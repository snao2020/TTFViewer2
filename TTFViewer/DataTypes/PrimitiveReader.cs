using System;
using System.IO;
using System.Linq;

namespace TTFViewer.DataTypes
{
    class PrimitiveReader
    {
        BinaryReader Reader;

        public PrimitiveReader(BinaryReader reader)
        {
            Reader = reader;
        }


        public object Read(Type t, UInt32 filePosition)
        {
            var mi = typeof(PrimitiveReader).GetMethods()
                .FirstOrDefault(i => i.IsGenericMethod && i.Name == "Read");
            if (mi != null)
            {
                mi = mi.MakeGenericMethod(new[] { t });
                var result  = mi.Invoke(this, new object[] { filePosition });
                return result;
            }
            return null;
        }

        public T Read<T>(UInt32 filePosition) where T : ITTFPrimitive
        {
            T p = default(T);
            var fp = Reader.BaseStream.Position;
            Reader.BaseStream.Position = filePosition;
            p.Load(Reader);
            Reader.BaseStream.Position = fp;

            return p;
        }

        public Array CreatePrimitiveArray(UInt32 filePosition, Type elementType, Int32 count)
        {
            Array result = null;
            if (elementType != null)
            {
                result = Array.CreateInstance(elementType, count);
                var value = Activator.CreateInstance(elementType);
                var primitive = (ITTFPrimitive)value;
                var fp = Reader.BaseStream.Position;
                Reader.BaseStream.Position = filePosition;
                for (int i = 0; i < count; i++)
                {
                    primitive.Load(Reader);
                    result.SetValue(value, i);
                }
                Reader.BaseStream.Position = fp;
            }
            return result;
        }


        public Byte[] DirectRead(UInt32 filePosition, Int32 length)
        {
            UInt32 fp = (UInt32)Reader.BaseStream.Position;
            Reader.BaseStream.Position = filePosition;
            var result = Reader.ReadBytes(length);
            Reader.BaseStream.Position = fp;
            return result;
        }


        public UInt32 GetStreamLength()
        {
            return (UInt32)Reader.BaseStream.Length;
        }
    }
}
