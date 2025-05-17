using System;
using System.IO;

namespace TTFViewer.Model
{
    public class DumpModel
    {
        BinaryReader Reader;

        public DumpModel(BinaryReader reader)
            => Reader = reader;


        public Byte[] Read(UInt32 filePosition, UInt32 length)
        {
            Byte[] result = null;
            if (Reader != null)
            {
                Reader.BaseStream.Position = filePosition;
                result = Reader.ReadBytes((int)length);
            }
            return result;
        }

        public UInt32 GetFileSize()
        {
            return Reader != null ? (UInt32)Reader.BaseStream.Length : 0;
        }
    }
}
