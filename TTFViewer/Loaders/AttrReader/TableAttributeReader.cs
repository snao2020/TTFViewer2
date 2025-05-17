using System;
using System.Collections.Generic;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Tables;

namespace TTFViewer.Loaders
{
    class TableAttributeReader
    {
        TableLoader TableLoader;
        FieldInfo OwnerFieldInfo;

        public TableAttributeReader(TableLoader tableLoader)
        {
            TableLoader = tableLoader;
            OwnerFieldInfo = tableLoader.GetFieldInfo();
        }

        public (Type, CreateModelFlags)? GetTypeFlags()
        {
            (Type, CreateModelFlags)? result = null;

            var ownerModel = TableLoader.TableModel;
            var tableTypeAttr = AttributeHelper.GetAttribute<TableTypeAttribute>(OwnerFieldInfo);
            if (tableTypeAttr != null)
            {
                if (tableTypeAttr.TableType != null)
                {
                    result = (tableTypeAttr.TableType, tableTypeAttr.CreateModelFlags);
                }
                else //if (tableTypeAttr.FieldValue != null)
                {
                    var reader = new FieldValueReader(TableLoader, OwnerFieldInfo.DeclaringType);

                    if (tableTypeAttr.TableKey == TableKey.Startup)
                    {
                        var offset = AttributeHelper2.GetValueT2<Offset32>(reader, OwnerFieldInfo.DeclaringType, tableTypeAttr);
                        var ret = AttributeHelper2.SearchTagTable<StartupTableAttribute>(TableKey.Startup, offset);
                        if (ret is ValueTuple<Type, StartupTableAttribute> vt)
                            result = (vt.Item1, CreateModelFlags.None);
                    }

                    else if (tableTypeAttr.TableKey == TableKey.FontTable)
                    {
                        var tag = AttributeHelper2.GetValueT2<Tag>(reader, OwnerFieldInfo.DeclaringType, tableTypeAttr);
                        var ret = AttributeHelper2.SearchTagTable<FontTableAttribute>(TableKey.FontTable, tag);
                        if (ret is ValueTuple<Type, FontTableAttribute> vt)
                            result = (vt.Item1, vt.Item2.Flags);
                    }
                }
            }
            else
            {
                var tableSelectAttr = AttributeHelper.GetAttribute<TableSelectAttribute>(OwnerFieldInfo);
                var reader = new FieldValueReader(TableLoader, OwnerFieldInfo.DeclaringType);
                var ret = AttributeHelper2.SelectAttribute4<TableSelectAttribute, TableConditionAttribute>(reader, tableSelectAttr, OwnerFieldInfo);
                if(ret != null)
                    result = (ret.Type, tableSelectAttr.CreateModelFlags);
            }
            return result;
        }


        public UInt32? GetPosition(UInt32 offset)
        {
            var ownerModel = TableLoader.TableModel;
            if (ownerModel.ValueType == typeof(RootTable))
                return 0;

            String basePath = "\\";
            var tablePositionAttr = AttributeHelper.GetAttribute<TablePositionAttribute>(OwnerFieldInfo);
            if (tablePositionAttr != null)
            {
                if (tablePositionAttr.MethodName != null)
                {
                    var reader2 = new FieldValueReader(TableLoader, OwnerFieldInfo.DeclaringType);
                    var ret = AttributeHelper2.GetValueT2<UInt32?>(reader2, OwnerFieldInfo.DeclaringType, tablePositionAttr);
                    return ret;
                }
                basePath = tablePositionAttr.BasePosition;
                if (tablePositionAttr.Flags.HasFlag(TablePositionFlag.MayBeNULL) && offset == 0)
                    return null;
                //if (tablePositionAttr.Flags.HasFlag(TablePositionFlag.OffsetMul2))
                //    offset *= 2;
            }

            UInt32 filePosition;
            if (basePath == null)
                filePosition = offset;
            else
            {
                basePath = TableLoader.ResolvePathIndex(basePath);
                if (TableLoader.GetFilePositionNull(basePath) is UInt32 u32)
                    filePosition = u32 + offset;
                else
                    return null;
            }

            var reader = new FieldValueReader(TableLoader, OwnerFieldInfo.DeclaringType);
            UInt32 offsetDelta = AttributeHelper2.GetValueT2<UInt32>(reader, OwnerFieldInfo.DeclaringType, tablePositionAttr);
            return filePosition + offsetDelta;
        }


        public (UInt32?, Int32?) GetLengthOrCount()
        {
            var ownerModel = TableLoader.TableModel;
            if (ownerModel.ValueType == typeof(RootTable))
                return (ownerModel.BinaryLoader.GetStreamLength(), null);

            var lengthAttr = AttributeHelper.GetAttribute<TableLengthAttribute>(OwnerFieldInfo);
            if (lengthAttr != null)
            {
                var reader = new FieldValueReader(TableLoader, OwnerFieldInfo.DeclaringType);
                switch (lengthAttr.LengthKind)
                {
                    case TableLengthKind.FileLength:
                        var length = AttributeHelper2.GetValueT2<UInt32>(reader, OwnerFieldInfo.DeclaringType, lengthAttr);
                        return (length, null);
                    case TableLengthKind.ElementCount:
                        var count = AttributeHelper2.GetValueT2<Int32>(reader, OwnerFieldInfo.DeclaringType, lengthAttr);
                        return (null, count);
                }
            }
            return (null, null);
        }
    }
}
