using System;
using System.Collections.Generic;
using UCNLDrivers;
using UCNLNav;
using UCNLNMEA;
using UCNLPhysics;

namespace RWLT_Host.RWLT
{
    public class EmuStringEventArgs : EventArgs
    {
        #region Properties

        public string EmuString { get; private set; }

        #endregion

        #region Constructor

        public EmuStringEventArgs(string emuString)
        {
            EmuString = emuString;
        }

        #endregion
    }

    public class RWLT_Emulator
    {
        #region Properties

        PrecisionTimer timer;
        Random rnd = new Random(DateTime.Now.Millisecond);

        double targetDpt = 0.0;
        double targetTemp = 0.0;
        double targetBat = 0.0;
        double targetCourse = 0.0;
        double targetSpeed = 0.0;
        double auxCourse = 0.0;
        double auxSpeed = 0.0;

        GeoPoint targetLocation;
        GeoPoint auxLocation;

        Dictionary<BaseIDs, GeoPoint> baseLocations;
        Dictionary<BaseIDs, double> baseSpeeds;
        Dictionary<BaseIDs, double> baseCourses;
        Dictionary<BaseIDs, double> TOAs;
        Dictionary<BaseIDs, double> baseBats;

        Array baseIDs = Enum.GetValues(typeof(BaseIDs));

        int dataIDIdx = 0;
        PingerDataIDs[] dataIDs = new PingerDataIDs[] 
        { 
            PingerDataIDs.DID_PRS, PingerDataIDs.DID_TMP,
            PingerDataIDs.DID_PRS, PingerDataIDs.DID_PRS, 
            PingerDataIDs.DID_PRS, PingerDataIDs.DID_PRS, 
            PingerDataIDs.DID_PRS, PingerDataIDs.DID_PRS,
            PingerDataIDs.DID_TMP, PingerDataIDs.DID_BAT
        };
        //PingerDataIDs[] dataIDs = new PingerDataIDs[] 
        //{             
        //    PingerDataIDs.DID_TMP, PingerDataIDs.DID_BAT
        //};


        public double SoundSpeed = PHX.PHX_FWTR_SOUND_SPEED_MPS;

        public double TargetLatitude
        {
            get { return Algorithms.Rad2Deg(targetLocation.Latitude); }
        }

        public double TargetLongitude
        {
            get { return Algorithms.Rad2Deg(targetLocation.Longitude); }
        }

        double timeSlice = 1;
        int targetPeriod = 0;

        public bool IsRunning { get { return timer.IsRunning; } }

        #endregion

        #region Constructor

        public RWLT_Emulator(double origin_lat, double origin_lon, double dpt, double tmp, double bat)
        {
            GeoPoint origin = new GeoPoint(Algorithms.Deg2Rad(origin_lat), Algorithms.Deg2Rad(origin_lon));

            baseLocations = new Dictionary<BaseIDs, GeoPoint>();
            baseSpeeds = new Dictionary<BaseIDs, double>();
            baseCourses = new Dictionary<BaseIDs, double>();
            TOAs = new Dictionary<BaseIDs, double>();
            baseBats = new Dictionary<BaseIDs, double>();

            targetBat = bat;
            targetSpeed = rnd.NextDouble() * 2;
            targetCourse = rnd.NextDouble() * Math.PI * 2;
            targetDpt = dpt;
            targetTemp = tmp;
            targetLocation = MoveGeoPoint(origin, 10 + rnd.NextDouble() * 50, targetCourse);

            auxSpeed = rnd.NextDouble() * 2;
            auxCourse = rnd.NextDouble() * Math.PI * 2;
            auxLocation = MoveGeoPoint(origin, 100 + rnd.NextDouble() * 50, auxCourse);

            timer = new PrecisionTimer();
            timer.Period = 1000;
            timer.Tick += new EventHandler(timer_Tick);

            double az_step = Math.PI / 2.0;
            double az = rnd.NextDouble() * Math.PI * 2.0;

            foreach (BaseIDs baseID in baseIDs)
            {
                if (baseID != BaseIDs.BASE_INVALID)
                {
                    baseCourses.Add(baseID, rnd.NextDouble() * Math.PI * 2);
                    baseSpeeds.Add(baseID, rnd.NextDouble() * 0.5);
                    baseLocations.Add(baseID, MoveGeoPoint(origin, 100 + rnd.NextDouble() * 20, az));
                    baseBats.Add(baseID, 12.0);

                    az += az_step;
                }
            }

        }

        #endregion

        #region Methods

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }      

        private GeoPoint MoveGeoPoint(GeoPoint source, double dst, double course)
        {
            double epLat = source.Latitude;
            double epLon = source.Longitude;
            double rev_az_rad = 0;
            int its = 0;

            Algorithms.VincentyDirect(source.Latitude, source.Longitude, course, dst, Algorithms.WGS84Ellipsoid,
                Algorithms.VNC_DEF_EPSILON, Algorithms.VNC_DEF_IT_LIMIT,
                out epLat, out epLon, out rev_az_rad, out its);

            return new GeoPoint(epLat, epLon);
        }

        #endregion

        #region Handlers

        private void timer_Tick(object sender, EventArgs e)
        {
            #region randomize basepoints

            double dst = timeSlice * targetSpeed;
            targetLocation = MoveGeoPoint(targetLocation, dst, targetCourse);
            targetCourse = targetCourse * 0.99 + 0.01 * rnd.NextDouble() * Math.PI * 2.0;
            targetSpeed = Math.Abs(targetSpeed * 0.9 + 0.1 * rnd.NextDouble() * 2 - 1);

            targetDpt = Math.Abs(targetDpt + rnd.NextDouble() * 0.5 - 0.25);
            targetTemp = Math.Abs(targetTemp + rnd.NextDouble() * 0.1 - 0.05);
            targetBat = Math.Abs(targetBat + rnd.NextDouble() * 0.1 - 0.05);

            dst = timeSlice * auxSpeed;
            auxLocation = MoveGeoPoint(auxLocation, dst, auxCourse);
            auxCourse = auxCourse * 0.99 + 0.01 * rnd.NextDouble() * Math.PI * 2.0;
            auxSpeed = Math.Abs(auxSpeed * 0.9 + 0.1 * rnd.NextDouble() * 2 - 1);

            foreach (BaseIDs baseID in baseIDs)
            {
                if (baseID != BaseIDs.BASE_INVALID)
                {
                    dst = timeSlice * baseSpeeds[baseID];
                    baseLocations[baseID] = MoveGeoPoint(baseLocations[baseID], dst, baseCourses[baseID]);
                    baseCourses[baseID] = baseCourses[baseID] * 0.9 + 0.1 * rnd.NextDouble() * Math.PI * 2.0;
                    baseSpeeds[baseID] = Math.Abs(baseSpeeds[baseID] * 0.9 + 0.1 * rnd.NextDouble() * 0.5 - 0.25);

                    baseBats[baseID] = baseBats[baseID] + rnd.NextDouble() * 0.1;
                }
            }

            #endregion

            if (++targetPeriod == 2)
            {
                targetPeriod = 0;

                double dst_p = 0;
                double ddpt = 0;
                double fwd_az_rad = 0;
                double rev_az_rad = 0;
                int its = 0;
                foreach (BaseIDs baseID in baseIDs)
                {
                    if (baseID != BaseIDs.BASE_INVALID)
                    {
                        Algorithms.VincentyInverse(targetLocation.Latitude, targetLocation.Longitude,
                            baseLocations[baseID].Latitude, baseLocations[baseID].Longitude,
                            Algorithms.WGS84Ellipsoid,
                            Algorithms.VNC_DEF_EPSILON,
                            Algorithms.VNC_DEF_IT_LIMIT,
                            out dst_p,
                            out fwd_az_rad,
                            out rev_az_rad,
                            out its);

                        ddpt = RWLT.DEFAULT_BASE_DPT_M - targetDpt;
                        TOAs[baseID] = Math.Sqrt(ddpt * ddpt + dst_p * dst_p) / SoundSpeed;
                    }
                }

                #region emulate LBLAs

                PingerDataIDs dataID = dataIDs[dataIDIdx];
                dataIDIdx = (dataIDIdx + 1) % dataIDs.Length;
                double data = 0.0;

                if (dataID == PingerDataIDs.DID_BAT)
                    data = targetBat;
                else if (dataID == PingerDataIDs.DID_TMP)
                    data = targetTemp;
                else if (dataID == PingerDataIDs.DID_PRS)
                    data = PHX.Pressure_by_depth_calc(targetDpt, PHX.PHX_ATM_PRESSURE_MBAR, PHX.PHX_FWTR_DENSITY_KGM3, PHX.PHX_GRAVITY_ACC_MPS2);
                else
                    data = (int)dataID;

                // IC_D2H_LBLA $PRWLA,bID,baseLat,baseLon,[baseDpt],baseBat,pingerDataID,pingerData,TOAsecond,MSR_dB
                foreach (BaseIDs baseID in baseIDs)
                {
                    if ((baseID != BaseIDs.BASE_INVALID)) //&& (baseID != BaseIDs.BASE_2))
                    {
                        NewEmuStringEvent.Rise(this, new EmuStringEventArgs(
                            NMEAParser.BuildProprietarySentence(ManufacturerCodes.RWL, "A",
                            new object[]
                            {
                                (int)baseID,
                                Algorithms.Rad2Deg(baseLocations[baseID].Latitude),
                                Algorithms.Rad2Deg(baseLocations[baseID].Longitude),
                                RWLT.DEFAULT_BASE_DPT_M,
                                baseBats[baseID],
                                dataID,
                                data,
                                TOAs[baseID],
                                77.7777 // it's an emulation
                            })));
                    }
                }

                #endregion
            }

            #region emulate AUX GNSS

            string latCardinal, lonCardinal;

            double auxLat = Algorithms.Rad2Deg(auxLocation.Latitude);
            double auxLon = Algorithms.Rad2Deg(auxLocation.Longitude);

            if (auxLat > 0) latCardinal = "North";
            else latCardinal = "South";
            if (auxLon > 0) lonCardinal = "East";
            else lonCardinal = "West";

            NewEmuStringEvent.Rise(this,
                new EmuStringEventArgs(
                    NMEAParser.BuildSentence(TalkerIdentifiers.GN, SentenceIdentifiers.RMC, new object[] 
            {
                DateTime.Now, 
                "Valid", 
                Math.Abs(auxLat), latCardinal,
                Math.Abs(auxLon), lonCardinal,
                auxSpeed * 3.6, // speed
                Algorithms.Rad2Deg(auxCourse), // track true
                DateTime.Now,
                null, // magnetic variation
                null, // magnetic variation direction
                "A",                
            })));

            #endregion            
        }

        #endregion 

        #region Events

        public EventHandler<EmuStringEventArgs> NewEmuStringEvent;

        #endregion
    }
}
