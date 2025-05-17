using System;

namespace Win32
{
    public static partial class User32
    {
        public const UInt32 DBT_DEVICEARRIVAL           = 0x8000;  // system detected a new device
        public const UInt32 DBT_DEVICEQUERYREMOVE       = 0x8001;  // wants to remove, may fail
        public const UInt32 DBT_DEVICEQUERYREMOVEFAILED = 0x8002;  // removal aborted
        public const UInt32 DBT_DEVICEREMOVEPENDING     = 0x8003;  // about to remove, still avail.
        public const UInt32 DBT_DEVICEREMOVECOMPLETE    = 0x8004;  // device is gone
        public const UInt32 DBT_DEVICETYPESPECIFIC      = 0x8005; // type specific event

        public const UInt32 DBT_DEVTYP_OEM              = 0x00000000;   // oem-defined device type
        public const UInt32 DBT_DEVTYP_DEVNODE          = 0x00000001;  // devnode number
        public const UInt32 DBT_DEVTYP_VOLUME           = 0x00000002;  // logical volume
        public const UInt32 DBT_DEVTYP_PORT             = 0x00000003; // serial, parallel
        public const UInt32 DBT_DEVTYP_NET              = 0x00000004;  // network resource

        public struct DEV_BROADCAST_HDR
        {
            public UInt32 dbch_size;
            public UInt32 dbch_devicetype;
            public UInt32 dbch_reserved;
        };
    }
}
