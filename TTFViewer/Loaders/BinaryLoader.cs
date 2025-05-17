using System;
using System.IO;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class BinaryLoader
    {
        BinaryReader Reader;

        public BinaryLoader(BinaryReader reader)
        {
            Reader = reader;
        }


        public PrimitiveReader GetPrimitiveReader()
        {
            return new PrimitiveReader(Reader);
        }

        public TableLoader GetTableLoader(TableModel tableModel)
        {
            return new TableLoader(Reader, tableModel);
        }                       

        public TableLoader GetTableLoader(TableModel tableModel, LoadItem2 loadItem)
        {
            return new TableLoader(Reader, tableModel, loadItem);
        }

        public TableLoader GetTableLoader(TableModel tableModel, string path)
        {
            return new TableLoader(Reader, tableModel, path);
        }

        public TableLoader GetTableLoader(TableModel tableModel, IElementList iel, Int32 index)
        {
            return new TableLoader(Reader, tableModel, iel, index);
        }

        public UInt32 GetStreamLength()
        {
            return (UInt32)Reader.BaseStream.Length;
        }
    }
}
