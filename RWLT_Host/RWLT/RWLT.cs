using System.Collections.Generic;

namespace RWLT_Host.RWLT
{

    public enum ICs
    {
        IC_D2H_ACK,
        IC_H2D_SETTINGS_WRITE,
        IC_D2H_LBLA,
        IC_H2D_DINFO_GET,
        IC_D2H_DINFO,
        IC_D2H_UNKNOWN,
        IC_INVALID
    }

    public enum BaseIDs
    {
        BASE_1 = 0,
        BASE_2 = 1,
        BASE_3 = 2,
        BASE_4 = 3,
        BASE_INVALID
    }

    public enum PingerDataIDs
    {
        DID_PRS = 0,
        DID_TMP = 1,
        DID_BAT = 2,
        DID_CODE = 3,
        DID_INVALID
    }

    public enum PingerCodeIDs
    {
        RE_RESERVED_16 = 1000,
        RE_RESERVED_15 = 1001,
        RE_RESERVED_14 = 1002,
        RE_RESERVED_13 = 1003,
        RE_RESERVED_12 = 1004,
        RE_RESERVED_11 = 1005,
        RE_RESERVED_10 = 1006,
        RE_RESERVED_9  = 1007,
        RE_RESERVED_8  = 1008,
        RE_RESERVED_7  = 1009,

        RE_RESERVED_6 = 1010,
        RE_RESERVED_5 = 1011,
        RE_RESERVED_4 = 1012,
        RE_RESERVED_3 = 1013,
        RE_RESERVED_2 = 1014,
        RE_RESERVED_1 = 1015,

        RE_PTS_FAILURE = 1016,
        RE_NOT_AVAILABLE = 1017,
        RE_PRS_OVRF = 1018,
        RE_BAT_LOW = 1019,
        RE_TMP_LOW = 1020,
        RE_TMP_HIGH = 1021,
        RE_INVALID_CODE
    }


    public static class RWLT
    {
        public readonly static int BASES_NUMBER = 4;
        public readonly static double DEFAULT_BASE_DPT_M = 1.5;

        static Dictionary<string, ICs> ICsIdxArray = new Dictionary<string, ICs>()
        {           
            { "0", ICs.IC_D2H_ACK },
            { "1", ICs.IC_H2D_SETTINGS_WRITE },           
            { "A", ICs.IC_D2H_LBLA },
            { "?", ICs.IC_H2D_DINFO_GET },
            { "!", ICs.IC_D2H_DINFO },           
            { "-", ICs.IC_D2H_UNKNOWN },         
        };

        public static ICs ICsByMessageID(string msgID)
        {
            if (ICsIdxArray.ContainsKey(msgID))
                return ICsIdxArray[msgID];
            else
                return ICs.IC_INVALID;
        }
    }
}
