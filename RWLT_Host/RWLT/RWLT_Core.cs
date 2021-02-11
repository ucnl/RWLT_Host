using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using UCNLDrivers;
using UCNLNav;
using UCNLNMEA;
using UCNLPhysics;

namespace RWLT_Host.RWLT
{    
    public class LocationUpdatedEventArgs : EventArgs
    {
        #region Properties

        public string ID { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double Depth { get; private set; }
        public bool IsValid { get; private set; }
        public DateTime TimeStamp { get; private set; }

        #endregion

        #region Constructor

        public LocationUpdatedEventArgs(string id, double lat, double lon, double dpt, bool isValid, DateTime timeStamp)
        {
            ID = id;
            Latitude = lat;
            Longitude = lon;
            Depth = dpt;
            IsValid = isValid;
            TimeStamp = timeStamp;
        }

        #endregion
    }    

    public class RWLT_Core : IDisposable
    {
        #region properties

        public bool IsOpen
        {
            get { return inPort.IsOpen; }
        }
        
        public bool AUXGNSSUsed { get; private set; }
        public bool OutPortUsed { get; private set; }               

        public bool IsAutoSoundSpeed = true;
        public double Soundspeed
        {
            get { return pCore.SoundSpeed; }
            set 
            { 
                pCore.SoundSpeed = value;
                IsAutoSoundSpeed = false;
            }
        }

        double salinity = 0.0;
        public double Salinity
        {
            get { return salinity; }
            set
            {
                if ((value >= 0.0) && (value <= 42.0))
                {
                    salinity = value;                    
                }
                else
                    throw new ArgumentOutOfRangeException("Salinity should be in a range 0..42 PSU");
            }
        }

        DateTime targetFixTimeStamp = DateTime.MinValue;
        public AgingValue<double> TargetLatitude;
        public AgingValue<double> TargetLongitude;
        public AgingValue<double> TargetLocationRadialError;
        public AgingValue<double> TargetPressure;
        public AgingValue<double> TargetTemperature;
        public AgingValue<double> TargetBatVoltage;
        public AgingValue<double> TargetDepth;
        public AgingValue<PingerCodeIDs> TargetAlarm;

        public AgingValue<DOPState> HDOPState;
        public AgingValue<TBAQuality> TBAState;

        public AgingValue<double> DistanceToTarget;
        public AgingValue<double> ForwardAzimuthToTarget;
        public AgingValue<double> ReverseAzimuthToTarget;

        public AgingValue<double> TargetCourse;

        public Dictionary<BaseIDs, AgingValue<double>> BaseBatVoltages = new Dictionary<BaseIDs, AgingValue<double>>();
        public Dictionary<BaseIDs, AgingValue<double>> BaseMSRs = new Dictionary<BaseIDs, AgingValue<double>>();

        public AgingValue<double> AUXLatitude;
        public AgingValue<double> AUXLongitude;
        public AgingValue<double> AUXTrack;
        public AgingValue<double> AUXSpeed;

        public AgingValue<double> CEP;
        public AgingValue<double> DRMS;
        public AgingValue<double> DRMS2;
        public AgingValue<double> DRMS3;

        public bool IsRadialErrorExeedsThreshold { get; private set; }

        Func<double, string> latlonFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F06}°", v));
        Func<double, string> svoltageFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F01} V", v));
        Func<double, string> msrFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F01} dB", v));
        Func<double, string> rerrFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", v));
        Func<double, string> prsFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F01} mBar", v));
        Func<double, string> tempFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F01} °C", v));
        Func<PingerCodeIDs, string> pAlmFormatter = new Func<PingerCodeIDs, string>((v) => v.ToString());
        Func<double, string> courseFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", v));
        Func<double, string> speedFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F01} km/h ({1:F01} m/s)", v, v / 3.6));
        Func<double, string> dptdstFormatter = new Func<double, string>((v) => string.Format(CultureInfo.InvariantCulture, "{0:F02} m", v));

        TrackFilter trkFilter;
        PCore2D<GeoPoint3DT> pCore;

        delegate T NullChecker<T>(object parameter);
        delegate object NullCheckerR<T>(T parameter);
        NullChecker<int> intNullChecker = (x => x == null ? -1 : (int)x);
        NullChecker<double> doubleNullChecker = (x => x == null ? double.NaN : (double)x);
        NullChecker<string> stringNullChecker = (x => x == null ? string.Empty : (string)x);
        NullCheckerR<double> doubleNullCheckerR = (x => double.IsNaN(x) ? null : (object)x);
        
        NMEAMultipleListener NMEAListener;
        SerialPort outPort;
        NMEASerialPort inPort;
        NMEASerialPort auxGNSSPort;
        RWLT_BaseProcessor baseProcessor;

        PrecisionTimer timer;

        bool gravityAccNeedsUpdate = true;
        double gravityAcc = UCNLPhysics.PHX.PHX_GRAVITY_ACC_MPS2;

        public bool InPortTimeout { get; private set; }
        public bool AUXGNSSTimeout { get; private set; }

        static bool nmeaSingleton = false;

        int systemUpdateTS = 0;
        readonly int systemUpdateLimit = 11;
        int inPortTimeoutTS = 0;
        readonly int inPortTimeoutLimit = 21;
        int AUXGNSSTimeoutTS = 0;
        readonly int AUXGNSSTimeoutLimit = 12;

        DateTime gnssTimeFix = DateTime.MinValue;
        DateTime gnssTimeFixLocalTS = DateTime.MinValue;

        BaseIDs auxGNSSBuoyID = BaseIDs.BASE_INVALID;
        public BaseIDs AuxGNSSBuoyID
        {
            get
            {
                return auxGNSSBuoyID;
            }
            set
            {
                if ((!AUXGNSSUsed) || (value == BaseIDs.BASE_INVALID))
                {
                    auxGNSSBuoyID = value;
                }
                else 
                {
                    throw new ArgumentException("Unable set a buoy as auxilary GNSS white AUX GNSS port is initialized");
                }
            }
        }

        bool isStatistics = false;
        public bool IsStatistics
        {
            get { return isStatistics; }
            set
            {
                statHelper.Clear();
                // TODO:
                isStatistics = value;
            }
        }

        public readonly int StatHelperRingSize = 512;
        List<GeoPoint> statHelper = new List<GeoPoint>();

        bool disposed = false;

        #endregion

        #region Constructor

        public RWLT_Core(SerialPortSettings rwltPortSettings, double radialErrorThreshold, double simplexSize, 
            int courseEstimatorFIFOSize, int trkFilterFIFOSize)
        {
            #region parameters

            var basesIDs = Enum.GetValues(typeof(BaseIDs));
            foreach (BaseIDs baseID in basesIDs)
            {
                if (baseID != BaseIDs.BASE_INVALID)
                {
                    BaseBatVoltages.Add(baseID, new AgingValue<double>(4, 10, svoltageFormatter));
                    BaseMSRs.Add(baseID, new AgingValue<double>(4, 10, msrFormatter));
                }
            }

            HDOPState = new AgingValue<DOPState>(4, 10, x => x.ToString().ToUpperInvariant());
            TBAState = new AgingValue<TBAQuality>(4, 10, x => x.ToString().Replace('_', ' ').ToUpperInvariant());

            TargetLatitude = new AgingValue<double>(4, 10, latlonFormatter);
            TargetLongitude = new AgingValue<double>(4, 10, latlonFormatter);
            TargetLocationRadialError = new AgingValue<double>(4, 10, rerrFormatter);
            TargetPressure = new AgingValue<double>(6, 10, prsFormatter);
            TargetTemperature = new AgingValue<double>(60, 120, tempFormatter);
            TargetBatVoltage = new AgingValue<double>(30, 120, svoltageFormatter);
            TargetDepth = new AgingValue<double>(6, 10, dptdstFormatter);
            TargetAlarm = new AgingValue<PingerCodeIDs>(600, 6000, pAlmFormatter);

            DistanceToTarget = new AgingValue<double>(4, 10, dptdstFormatter);
            ForwardAzimuthToTarget = new AgingValue<double>(4, 10, courseFormatter);
            ReverseAzimuthToTarget = new AgingValue<double>(4, 10, courseFormatter);

            TargetCourse = new AgingValue<double>(4, 10, courseFormatter);

            AUXLatitude = new AgingValue<double>(4, 10, latlonFormatter);
            AUXLongitude = new AgingValue<double>(4, 10, latlonFormatter);
            AUXTrack = new AgingValue<double>(4, 10, courseFormatter);
            AUXSpeed = new AgingValue<double>(4, 10, speedFormatter);

            CEP = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            DRMS = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            DRMS2 = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            DRMS3 = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            
            #endregion

            #region trkFilter

            trkFilter = new TrackFilter(trkFilterFIFOSize);

            #endregion

            #region pCore

            pCore = new PCore2D<GeoPoint3DT>(radialErrorThreshold, simplexSize, Algorithms.WGS84Ellipsoid, courseEstimatorFIFOSize);
            pCore.RadialErrorExeedsThrehsoldEventHandler += new EventHandler(pCore_RadialErrorExeedsThresholdEventHandler);
            pCore.TargetLocationUpdatedExHandler += new EventHandler<TargetLocationUpdatedExEventArgs>(pCore_TargetLocationUpdatedExEventHandler);
            pCore.BaseQualityUpdatedHandler += new EventHandler<BaseQualityUpdatedEventArgs>(pCore_BaseQualityUpdatedEventHandler);

            #endregion

            #region basesProcessor

            baseProcessor = new RWLT_BaseProcessor(4, 2.0);

            #endregion
            
            #region NMEA

            if (!nmeaSingleton)
            {
                NMEAParser.AddManufacturerToProprietarySentencesBase(ManufacturerCodes.RWL);                
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.RWL, "A", "x,x.x,x.x,x.x,x.x,x,x.x,x.x,x.x");
            }

            #endregion

            #region inPort

            inPort = new NMEASerialPort(rwltPortSettings);
            inPort.NewNMEAMessage += (o, e) =>
            {
                LogEvent.Rise(o, new LogEventArgs(LogLineType.INFO, string.Format("{0} (IN) >> {1}", inPort.PortName, e.Message)));
                NMEAListener.ProcessIncoming(0, e.Message);
            };

            inPort.PortError += (o, e) => LogEvent.Rise(o, new LogEventArgs(LogLineType.ERROR, string.Format("{0} (IN) >> {1}", inPort.PortName, e.EventType.ToString())));

            #endregion

            #region NMEAListener

            NMEAListener = new NMEAMultipleListener();
            NMEAListener.NMEAProprietaryUnsupportedSentenceParsed += new EventHandler<NMEAUnsupportedProprietaryEventArgs>(NMEAPSentenceReceived);
            NMEAListener.RMCSentenceReceived += new EventHandler<RMCMessageEventArgs>(GNSS_RMCSentenceReceived);

            #endregion

            #region timer

            timer = new PrecisionTimer();
            timer.Period = 100;
            timer.Tick += (o, e) =>
            {
                if (++systemUpdateTS > systemUpdateLimit)
                {
                    systemUpdateTS = 0;
                    SystemUpdateEvent.Rise(this, new EventArgs());
                }

                if (inPort.IsOpen && (++inPortTimeoutTS > inPortTimeoutLimit))
                {
                    inPortTimeoutTS = 0;
                    InPortTimeout = true;
                    LogEvent.Rise(this, new LogEventArgs(LogLineType.ERROR, string.Format("{0} (IN) >> TIMEOUT", inPort.PortName)));
                }

                if (AUXGNSSUsed && auxGNSSPort.IsOpen && (++AUXGNSSTimeoutTS > AUXGNSSTimeoutLimit))
                {
                    AUXGNSSTimeoutTS = 0;
                    AUXGNSSTimeout = true;
                    LogEvent.Rise(this, new LogEventArgs(LogLineType.ERROR, string.Format("{0} (AUX) >> TIMEOUT", auxGNSSPort.PortName)));
                }
            };

            timer.Start();

            #endregion

            statHelper = new List<GeoPoint>();
        }
        
        #endregion

        #region Methods

        public void InitAUXGNSSPort(SerialPortSettings auxPortSettings)
        {
            if (auxGNSSPort == null)
            {
                auxGNSSPort = new NMEASerialPort(auxPortSettings);
                auxGNSSPort.NewNMEAMessage += (o, e) =>
                {
                    LogEvent.Rise(o, new LogEventArgs(LogLineType.INFO, string.Format("{0} (AUX) >> {1}", auxGNSSPort.PortName, e.Message)));
                    NMEAListener.ProcessIncoming(2, e.Message);
                };
                auxGNSSPort.PortError += (o, e) => LogEvent.Rise(o, new LogEventArgs(LogLineType.ERROR, string.Format("{0} (AUX) >> {1}", auxGNSSPort.PortName, e.EventType.ToString())));
                AUXGNSSUsed = true;

                AuxGNSSBuoyID = BaseIDs.BASE_INVALID;
            }
            else
            {
                throw new InvalidOperationException("Auxilary GNSS port has been already initialized");
            }
        }

        public void InitOutputPort(SerialPortSettings outPortSettings)
        {
            if (outPort == null)
            {
                outPort = new SerialPort(outPortSettings.PortName, (int)outPortSettings.PortBaudRate);
                OutPortUsed = true;
            }
            else
            {
                throw new InvalidOperationException("Output port has been already initialized");
            }
        }
        
        public void Start()
        {
            if (!inPort.IsOpen)
            {
                if (AUXGNSSUsed)
                {
                    try
                    {
                        auxGNSSPort.Open();
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(auxGNSSPort, new LogEventArgs(LogLineType.ERROR, ex));
                    }
                }

                if (OutPortUsed)
                {
                    try
                    {
                        outPort.Open();
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(outPort, new LogEventArgs(LogLineType.ERROR, ex));
                    }
                }

                try
                {
                    inPort.Open();
                }
                catch (Exception ex)
                {
                    LogEvent.Rise(inPort, new LogEventArgs(LogLineType.CRITICAL, ex));
                    throw ex;
                }
            }
            else
            {
                throw new InvalidOperationException("Already running");
            }
        }

        public void Stop()
        {
            if (inPort.IsOpen)
            {
                try
                {
                    inPort.Close();
                }
                catch (Exception ex)
                {
                    LogEvent.Rise(inPort, new LogEventArgs(LogLineType.CRITICAL, ex));
                }

                if (AUXGNSSUsed)
                {
                    try
                    {
                        auxGNSSPort.Close();
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(auxGNSSPort, new LogEventArgs(LogLineType.ERROR, ex));
                    }
                }

                if (OutPortUsed)
                {
                    try
                    {
                        outPort.Close();
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(outPort, new LogEventArgs(LogLineType.ERROR, ex));
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot be stopped because has not been started");
            }
        }

        public void Emulate(string line)
        {
            NMEAListener.ProcessIncoming(99, line);
            LogEvent.Rise(this, new LogEventArgs(LogLineType.INFO, line));
        }

        public void MarkCurrentLocation()
        {
            if (TargetLatitude.IsInitialized && TargetLongitude.IsInitialized)
            {
                LocationUpdatedEvent.Rise(this,
                    new LocationUpdatedEventArgs("Marked", TargetLatitude.Value, TargetLongitude.Value,
                        TargetDepth.IsInitialized ? TargetDepth.Value : 0.0, true, targetFixTimeStamp));                        
            }
        }

        #region Private

        public void UpdateStat(double lat, double lon)
        {
            statHelper.Add(new GeoPoint(lat, lon));
            if (statHelper.Count > StatHelperRingSize)
                statHelper.RemoveAt(0);

            var mPoints = Navigation.GCSToLCS(statHelper, Algorithms.WGS84Ellipsoid);

            double sigmax = 0, sigmay = 0;
            Navigation.GetPointsSTD2D(mPoints, out sigmax, out sigmay);

            CEP.Value = Navigation.CEP(sigmax, sigmay);
            var drms = Navigation.DRMS(sigmax, sigmay);
            DRMS.Value = drms;
            DRMS2.Value = drms * 2;
            DRMS3.Value = drms * 3;
        }

        public DateTime GetTimeStamp()
        {
            if (AUXGNSSUsed && !AUXGNSSTimeout)
            {
                return gnssTimeFix.Add(DateTime.Now.Subtract(gnssTimeFixLocalTS));
            }
            else
                return DateTime.Now;
        }

        private void UpdateDistanceToTarget()
        {
            if (AUXLatitude.IsInitializedAndNotObsolete && AUXLongitude.IsInitializedAndNotObsolete &&
                TargetLatitude.IsInitializedAndNotObsolete && TargetLongitude.IsInitializedAndNotObsolete)
            {
                double sp_lat_rad = Algorithms.Deg2Rad(AUXLatitude.Value);
                double sp_lon_rad = Algorithms.Deg2Rad(AUXLongitude.Value);
                double ep_lat_rad = Algorithms.Deg2Rad(TargetLatitude.Value);
                double ep_lon_rad = Algorithms.Deg2Rad(TargetLongitude.Value);

                double dst_m = double.NaN;
                double fwd_az_rad = double.NaN;
                double rev_az_rad = double.NaN;
                int its = -1;

                Algorithms.VincentyInverse(sp_lat_rad, sp_lon_rad,
                    ep_lat_rad, ep_lon_rad, Algorithms.WGS84Ellipsoid,
                    Algorithms.VNC_DEF_EPSILON, Algorithms.VNC_DEF_IT_LIMIT,
                    out dst_m, out fwd_az_rad, out rev_az_rad, out its);

                rev_az_rad += Math.PI;
                if (rev_az_rad > Math.PI * 2)
                    rev_az_rad -= Math.PI * 2;

                if (!double.IsNaN(dst_m))
                    DistanceToTarget.Value = dst_m;

                if (!double.IsNaN(fwd_az_rad))
                    ForwardAzimuthToTarget.Value = Algorithms.Rad2Deg(fwd_az_rad);

                if (!double.IsNaN(rev_az_rad))
                    ReverseAzimuthToTarget.Value = Algorithms.Rad2Deg(rev_az_rad);
            }
        }

        private void WriteOutData(double bLat, double bLon, double bDpt, double rErr, double cource, bool isValid, double wTemp)
        {
            string latCardinal, lonCardinal;

            string RMCvString;
            if (isValid) RMCvString = "Valid";
            else RMCvString = "Invalid";

            if (bLat > 0) latCardinal = "North";
            else latCardinal = "South";

            if (bLon > 0) lonCardinal = "East";
            else lonCardinal = "West";

            StringBuilder emuString = new StringBuilder();            

            #region RMC

            emuString.Append(NMEAParser.BuildSentence(TalkerIdentifiers.GP, 
                SentenceIdentifiers.RMC, 
                new object[] 
                {
                    GetTimeStamp(), 
                    RMCvString, 
                    doubleNullCheckerR(Math.Abs(bLat)), latCardinal,
                    doubleNullCheckerR(Math.Abs(bLon)), lonCardinal,
                    null, // speed
                    doubleNullCheckerR(cource), // track true
                    GetTimeStamp(),
                    null, // magnetic variation
                    null, // magnetic variation direction
                    "A",
                }));

            #endregion

            #region GGA

            if (bLat > 0) latCardinal = "N";
            else latCardinal = "S";

            if (bLon > 0) lonCardinal = "E";
            else lonCardinal = "W";

            emuString.Append(NMEAParser.BuildSentence(TalkerIdentifiers.GP, 
                SentenceIdentifiers.GGA, 
                new object[]
                {
                    GetTimeStamp(),
                    doubleNullCheckerR(Math.Abs(bLat)), latCardinal,
                    doubleNullCheckerR(Math.Abs(bLon)), lonCardinal,
                    "GPS fix",
                    4,
                    doubleNullCheckerR(rErr),
                    doubleNullCheckerR(-bDpt),
                    "M",
                    null,
                    "M",
                    null,
                    null
                }));

            #endregion

            #region MTW

            if (!double.IsNaN(wTemp))
            {
                emuString.Append(NMEAParser.BuildSentence(TalkerIdentifiers.GP, SentenceIdentifiers.MTW, new object[]
                {
                    doubleNullCheckerR(wTemp),
                    "C"
                }));
            }

            #endregion

            var emuStr = emuString.ToString();

            try
            {
                var bytes = Encoding.ASCII.GetBytes(emuStr);
                outPort.Write(bytes, 0, bytes.Length);

                LogEvent.Rise(this, new LogEventArgs(LogLineType.INFO, string.Format("{0} (OUT) << {1}", outPort.PortName, emuStr)));
            }
            catch (Exception ex)
            {
                LogEvent.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void TryLocate(IEnumerable<GeoPoint3DT> basePoints)
        {
            double baseMeanDepth = 0.0;
            int bCount = 0;
            foreach (var item in basePoints)
            {
                baseMeanDepth += item.Depth;
                bCount++;
            }

            if (bCount >= 3)
            {
                baseMeanDepth /= bCount;

                if (TargetDepth.IsInitialized)
                {
                    pCore.TargetDepth = TargetDepth.Value;
                    pCore.ProcessBasePoints(basePoints, GetTimeStamp());
                }
                else
                {
                    pCore.ProcessBasePoints(basePoints, baseMeanDepth, GetTimeStamp());
                }                
            }            
        }

        private void OnLBLA(object[] parameters)
        {
            // IC_D2H_LBLA $PRWLA,bID,baseLat,baseLon,[baseDpt],baseBat,pingerDataID,pingerData,TOAsecond,MSR_dB
            BaseIDs baseID = (parameters[0] == null) ? BaseIDs.BASE_INVALID : (BaseIDs)(int)parameters[0];

            double baseLat = doubleNullChecker(parameters[1]);
            double baseLon = doubleNullChecker(parameters[2]);
            double baseDepth = (parameters[3] == null) ? RWLT.DEFAULT_BASE_DPT_M : (double)parameters[3];

            double baseBat = doubleNullChecker(parameters[4]);
            PingerDataIDs pDataID = (parameters[5] == null) ? PingerDataIDs.DID_INVALID : (PingerDataIDs)(int)parameters[5];
            double pData = doubleNullChecker(parameters[6]);
            double TOAs = doubleNullChecker(parameters[7]);
            double MSR = doubleNullChecker(parameters[8]);

            if ((baseID != BaseIDs.BASE_INVALID) &&
                (!double.IsNaN(baseLat)) &&
                (!double.IsNaN(baseLon)) &&
                (!double.IsNaN(baseDepth)))
            {

                if ((baseID == auxGNSSBuoyID) && (!AUXGNSSUsed))
                {
                    GNSS_RMCSentenceReceived(this,
                        new RMCMessageEventArgs(0xFF,
                            TalkerIdentifiers.GN,
                            GetTimeStamp(),
                            baseLat,
                            baseLon,
                            double.NaN,
                            double.NaN,
                            double.NaN,
                            true));
                }
                
                LocationUpdatedEvent.Rise(this, new LocationUpdatedEventArgs(baseID.ToString().Replace('_', ' '),
                    baseLat, baseLon, baseDepth,
                    true, 
                    GetTimeStamp()));      

                if (!double.IsNaN(MSR))
                    BaseMSRs[baseID].Value = MSR;                

                if (!double.IsNaN(baseBat))
                    BaseBatVoltages[baseID].Value = baseBat;

                if (gravityAccNeedsUpdate)
                {
                    gravityAcc = PHX.Gravity_constant_wgs84_calc(baseLat);
                    gravityAccNeedsUpdate = false;
                }

                if (pDataID == PingerDataIDs.DID_BAT)
                {
                    TargetBatVoltage.Value = pData;
                }
                else if (pDataID == PingerDataIDs.DID_TMP)
                {
                    TargetTemperature.Value = pData;
                    if (IsAutoSoundSpeed && TargetPressure.IsInitialized)
                        Soundspeed = PHX.Speed_of_sound_UNESCO_calc(TargetTemperature.Value, TargetPressure.Value / 2.0, salinity);
                }
                else if (pDataID == PingerDataIDs.DID_PRS)
                {
                    TargetPressure.Value = pData;
                    if (TargetTemperature.IsInitialized)
                    {
                        if (IsAutoSoundSpeed)
                            Soundspeed = PHX.Speed_of_sound_UNESCO_calc(TargetTemperature.Value, TargetPressure.Value / 2.0, salinity);

                        double rho = PHX.Water_density_calc(TargetTemperature.Value, TargetPressure.Value / 2.0, salinity);
                        // p0 is zero, because pinger calibrates surface pressure on start and transmits pressure relative to the surface
                        TargetDepth.Value = PHX.Depth_by_pressure_calc(TargetPressure.Value, 0, rho, gravityAcc);
                    }
                    else if (TargetPressure.IsInitialized)
                    {
                        TargetDepth.Value = PHX.Depth_by_pressure_calc(TargetPressure.Value, 0, PHX.PHX_FWTR_DENSITY_KGM3, gravityAcc);
                    }
                }
                else if (pDataID == PingerDataIDs.DID_CODE)
                {
                    TargetAlarm.Value = (PingerCodeIDs)Enum.ToObject(typeof(PingerCodeIDs), Convert.ToInt32(pData));
                }

                var bases = baseProcessor.ProcessBase(baseID, baseLat, baseLon, baseDepth, TOAs);
                if (bases != null)
                    TryLocate(bases);
            }

            OnSystemUpdate();
        }

        private void OnSystemUpdate()
        {
            systemUpdateTS = 0;
            SystemUpdateEvent.Rise(this, new EventArgs());
        }

        #endregion

        #endregion

        #region Handlers

        #region NMEAListener

        private void NMEAPSentenceReceived(object sender, NMEAUnsupportedProprietaryEventArgs e)
        {
            if ((e.Sentence.Manufacturer == ManufacturerCodes.RWL) && (e.Sentence.SentenceIDString == "A"))
            {
                inPortTimeoutTS = 0;
                InPortTimeout = false;
                OnLBLA(e.Sentence.parameters);                    
            }            
        }

        private void GNSS_RMCSentenceReceived(object sender, RMCMessageEventArgs e)
        {
            AUXGNSSTimeoutTS = 0;
            AUXGNSSTimeout = false;

            if (e.IsValid)
            {
                AUXLatitude.Value = e.Latitude;
                AUXLongitude.Value = e.Longitude;
                if (!double.IsNaN(e.TrackTrue)) 
                    AUXTrack.Value = e.TrackTrue;
                if (!double.IsNaN(e.SpeedKmh))
                    AUXSpeed.Value = e.SpeedKmh;

                gnssTimeFix = e.TimeFix;
                gnssTimeFixLocalTS = DateTime.Now;

                UpdateDistanceToTarget();

                LocationUpdatedEvent.Rise(this, new LocationUpdatedEventArgs("AUX GNSS", e.Latitude, e.Longitude, 0.0, true, e.TimeFix));
                OnSystemUpdate();
            }
        }

        #endregion

        #region pCore

        private void pCore_RadialErrorExeedsThresholdEventHandler(object sender, EventArgs e)
        {
            IsRadialErrorExeedsThreshold = true;
            SystemUpdateEvent.Rise(this, new EventArgs());
        }

        private void pCore_TargetLocationUpdatedExEventHandler(object sender, TargetLocationUpdatedExEventArgs e)
        {
            targetFixTimeStamp = e.TimeStamp;
            TargetCourse.Value = e.Course;
            IsRadialErrorExeedsThreshold = false;

            LocationUpdatedEvent.Rise(this,
                new LocationUpdatedEventArgs("RWLT (RAW)",
                    e.Location.Latitude, e.Location.Longitude, e.Location.Depth,
                    e.Location.RadialError <= pCore.RadialErrorThreshold,
                    e.TimeStamp));


            GeoPoint locationFlt = new GeoPoint();

            if (TargetDepth.IsInitialized)
                locationFlt = trkFilter.Filter(e.Location.Latitude, e.Location.Longitude);
            else
                locationFlt = new GeoPoint(e.Location.Latitude, e.Location.Longitude);

            LocationUpdatedEvent.Rise(this,
                new LocationUpdatedEventArgs("RWLT (FLT)",
                    locationFlt.Latitude, locationFlt.Longitude, e.Location.Depth,
                    e.Location.RadialError <= pCore.RadialErrorThreshold,
                    e.TimeStamp));

            if (isStatistics)
                UpdateStat(locationFlt.Latitude, locationFlt.Longitude);


            if (OutPortUsed)
            {
                double wTemp = double.NaN;
                if (TargetTemperature.IsInitialized)
                    wTemp = TargetTemperature.Value;

                WriteOutData(locationFlt.Latitude,
                    locationFlt.Longitude,
                    e.Location.Depth,
                    e.Location.RadialError,
                    e.Course,
                    e.Location.RadialError <= pCore.RadialErrorThreshold,
                    wTemp);
            }

            TargetLocationRadialError.Value = e.Location.RadialError;
            TargetLatitude.Value = locationFlt.Latitude;
            TargetLongitude.Value = locationFlt.Longitude;

            UpdateDistanceToTarget();
        }

        private void pCore_BaseQualityUpdatedEventHandler(object sender, BaseQualityUpdatedEventArgs e)
        {
            HDOPState.Value = e.DopState;
            TBAState.Value = e.TBAState;
        }
        
        #endregion

        #endregion

        #region Events

        public EventHandler<LogEventArgs> LogEvent;
        public EventHandler SystemUpdateEvent;
        public EventHandler<LocationUpdatedEventArgs> LocationUpdatedEvent;

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (timer.IsRunning)
                        timer.Stop();
                    timer.Dispose();

                    if (inPort.IsOpen)
                    {
                        try
                        {
                            inPort.Close();
                        }
                        catch { }
                    }

                    inPort.Dispose();

                    if (AUXGNSSUsed)
                    {
                        try
                        {
                            auxGNSSPort.Close();                            
                        }
                        catch { }

                        auxGNSSPort.Dispose();
                    }

                    if (OutPortUsed)
                    {
                        if (outPort.IsOpen)
                        {
                            try
                            {
                                outPort.Close();
                            }
                            catch { }
                        }

                        outPort.Dispose();
                    }
                }

                disposed = true;
            }
        }

        #endregion
    }
}
