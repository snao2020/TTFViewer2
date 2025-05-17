using System;
using System.Collections.Generic;
using System.IO;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.Tables;

namespace TTFViewer.Model
{
#pragma warning disable IDE1006

    class TableModel
    {
        public TableModel Parent { get; }
        public BinaryLoader BinaryLoader { get; }
        public CreateModelFlags CreateModelFlags { get; }
        public string SourcePath { get; }
        public UInt32 FilePosition { get; }
        public UInt32? FileLength { get; }
        public Int32? ElementCount { get; }

        public Type ValueType { get; } 

        object _Value;
        public object GetValue()
        {
            object result = null;
            if (ValueType != null)
            {
                if (_Value == null)
                {
                    var tableLoader = BinaryLoader.GetTableLoader(this);
                    result = tableLoader.GetValue("");
                    if (CreateModelFlags.HasFlag(CreateModelFlags.Buffered))
                        _Value = result;
                }
                else
                    result = _Value;
            }
            return result;
        }

        public TableModel(TableModel parent, ChildParam cp) //, CreateModelFlags createModelFlags)
        {
            Parent = parent;
            BinaryLoader = parent.BinaryLoader;

            FilePosition = cp.FilePosition;
            FileLength = cp.FileLength;
            ElementCount = cp.ElementCount;
            CreateModelFlags = cp.CreateModelFlags;
            SourcePath = cp.Path;
            ValueType = cp.BaseType;
        }


        public TableModel(BinaryReader reader)
        {
            Parent = null;
            BinaryLoader = new BinaryLoader(reader);
            FilePosition = 0;
            SourcePath = null;
            ValueType = typeof(RootTable);
        }
    }

#pragma warning restore IDE1006
}
