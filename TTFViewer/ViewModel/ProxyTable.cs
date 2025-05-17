using System;
using System.Collections.Generic;
using TTFViewer.DataTypes;
using TTFViewer.Tables;

namespace TTFViewer.ViewModel
{
#pragma warning disable IDE1006

    class ProxyTable
    {
#pragma warning disable CS0649
        [Proxy(typeof(NameRecord), "stringOffset")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "nameBytesDescription")]
        public uint8[] Name;

        [Proxy(typeof(NameRecord), "stringOffset")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "nameBytesDescription")]
        public IList<uint8> Name2;

        [Proxy(typeof(LangTagRecord), "langTagOffset")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "nameBytesDescription")]
        public uint8[] Name3;

        [Proxy(typeof(LangTagRecord), "langTagOffset")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "nameBytesDescription")]
        public IList<uint8> Name4;

        [Proxy(typeof(GlyphVariationDataHeader), "dataOffset")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "serialized data")]
        public uint8[] Name5;

        [Proxy(typeof(GlyphVariationDataHeader), "dataOffset")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "serialized data")]
        public IList<uint8> Name6;


#pragma warning restore CS0649

        static string nameBytesDescription(IItemValueService ivs)
        {
            string result = null;
            if (ivs.Value is IList<uint8> bytes)
            {
                var recordIVS = ivs.Parent?.Parent;
                if(recordIVS != null)
                {
                    if (recordIVS.ValueType == typeof(Tables.LangTagRecord))
                    {
                        result = ItemValueHelper.UnicodeDescription(ivs);
                    }
                    else if (recordIVS.Value is Tables.NameRecord nr)
                    {
                        bool isUnicode =
                                nr.platformID == (Int32)name_PlatformIDs.Windows
                                    && (nr.encodingID == (Int32)name_WindowsEncodingIDs.Symbol
                                        || nr.encodingID == (Int32)name_WindowsEncodingIDs.Unicode_BMP
                                        || nr.encodingID == (Int32)name_WindowsEncodingIDs.Unicode_full_repertoire
                                        )
                                || nr.platformID == (Int32)name_PlatformIDs.Unicode;

                        if (isUnicode)
                            result = ItemValueHelper.UnicodeDescription(ivs);
                        else
                            result = ItemValueHelper.AsciiDescription(ivs);
                    }
                }
            }
            return result;
        }
    }

#pragma warning restore IDE1006
}
