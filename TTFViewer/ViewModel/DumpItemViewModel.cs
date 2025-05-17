using System;
using TTFViewer.Model;

namespace TTFViewer.ViewModel
{
    public class Bytes
    {
        public Byte? this[int index]
        {
            get
            {
                if (index < Start)
                    return null;
                else if (index < End)
                    return Array[index - Start];
                else
                    return null;
            }
        }

        int Start;
        int End;
        Byte[] Array;

        public Bytes(int start, int end, Byte[] array)
        {
            Start = start;
            End = end;
            Array = array;
        }
    }


    public class DumpItemViewModel
    {
        public const UInt32 Length = 16;
        public UInt32 FilePosition { get; }

        public Bytes Bytes { get; }

        public DumpViewModel Parent;

        public DumpItemViewModel(DumpViewModel parent, UInt32 start, UInt32 end, DumpModel model)//, Range selection)
        {
            Parent = parent;
            FilePosition = start / Length * Length;
            UInt32 s = start;
            UInt32 e = end;
            Byte[] bytes = model.Read(start, end - start);
            Bytes = new Bytes((int)(start - FilePosition), (int)(end - FilePosition), bytes);
        }
    }
}
