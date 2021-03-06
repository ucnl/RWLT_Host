﻿using RWLT_Host.RWLT;
using System;
using UCNLDrivers;

namespace RWLT_Host
{
    [Serializable]
    public class SettingsContainer : SimpleSettingsContainer
    {
        #region Properties

        public string InPortName;
        public BaudRate InPortBaudrate;

        public bool IsUseOutPort;
        public string OutPortName;
        public BaudRate OutPortBaudrate;

        public bool IsUseAUXGNSSPort;
        public string AUXGNSSPortName;
        public BaudRate AUXGNSSPortBaudrate;

        public bool IsSalinityAuto;
        public double SalinityPSU;

        public bool IsSoundSpeedAuto;
        public double SoundSpeedMPS;

        public double RadialErrorThrehsold;

        public int TrackPointsToShow;

        public bool IsEmulatorButtonVisible;

        public int CourseEstimatorFIFOSize;
        public int TrackFilterFIFOSize;

        public bool IsUseBuoyAsAUXGNSS;
        public BaseIDs AuxGNSSBuoyID;

        public string[] TileServers;

        #endregion

        #region Methods

        public override void SetDefaults()
        {
            InPortName = "COM1";
            InPortBaudrate = BaudRate.baudRate9600;
            
            IsUseOutPort = false;
            OutPortName = "COM1";
            OutPortBaudrate = BaudRate.baudRate9600;

            IsUseAUXGNSSPort = false;
            AUXGNSSPortName = "COM1";
            AUXGNSSPortBaudrate = BaudRate.baudRate9600;

            SalinityPSU = 0.0;

            IsSoundSpeedAuto = true;
            SoundSpeedMPS = 1500;

            RadialErrorThrehsold = 10;

            TrackPointsToShow = 100;

            IsEmulatorButtonVisible = false;

            CourseEstimatorFIFOSize = 8;
            TrackFilterFIFOSize = 4;

            IsUseBuoyAsAUXGNSS = false;
            AuxGNSSBuoyID = BaseIDs.BASE_1;

            TileServers = new string[]
            {
                "https://a.tile.openstreetmap.org",
                "https://b.tile.openstreetmap.org",
                "https://c.tile.openstreetmap.org"
            };
        }

        #endregion
    }
}
