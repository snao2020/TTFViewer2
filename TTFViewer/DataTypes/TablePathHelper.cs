using System;
using System.Collections.Generic;
using System.Linq;

namespace TTFViewer.DataTypes
{
    static class TablePathHelper
    {
        public static Int32 GetFirstIndex(string path)
        {
            Int32 start = path.IndexOf('[');
            Int32 end = path.IndexOf(']');
            if(start >= 0 && end >= 0)
                return Int32.Parse(path.Substring(start + 1, end - start - 1));
            return -1;
        }


        public static Int32 GetLastIndex(string path)
        {
            Int32 start = path.LastIndexOf('[');
            Int32 end = path.LastIndexOf(']');
            if (start >= 0 && end >= 0)
                return Int32.Parse(path.Substring(start + 1, end - start - 1));
            return -1;
        }


        public static string GetLastName(string path)
        {
            Int32 start = path.LastIndexOf('\\');
            return path.Substring(start + 1);
        }

        public static List<Int32> GetIndexes(string path)
        {
            var result = new List<Int32>();
            var paths = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var s in paths)
            {
                if(s.Length > 0 && s[0] == '[')
                {
                    var subStr = s.Substring(1, s.Length - 2);
                    result.Add(Int32.Parse(subStr)); ;
                }
            }
            return result;
        }


        // bug fixed 
        public static string GetFieldPath2(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            if (path[0] == '\\')
                path = path.Substring(1);
            if (path.Last() == ']')
            {
                var lastIndex = path.LastIndexOf('\\');
                if (lastIndex >= 0)
                {
                    path = path.Substring(0, lastIndex);
                    return GetFieldPath2(path);
                }
                else
                    path = null;
            }
            return path;
        }

        // remove last []s
        // bug: if path=="[0]", return "[0]"
        public static string GetFieldPath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            if (path[0] == '\\')
                path = path.Substring(1);
            var lastIndex = path.LastIndexOf('\\');
            if(lastIndex >= 0)
            {
                if(path[lastIndex + 1] == '[')
                {
                    return GetFieldPath(path.Substring(0, lastIndex));
                }
            }
            return path;
        }

        public static string MergePathIndexes(string path, List<Int32> indexes)
        {
            foreach (var i in indexes)
            {
                int pos = path.IndexOf("[]");
                if (pos >= 0)
                    path = path.Insert(pos + 1, i.ToString());
            }
            return path;
        }


        public static Int32? GetIndex(string name)
        {           
            if (!string.IsNullOrEmpty(name)
                && name[0] == '[' && name.IndexOf(']') == name.Length - 1)
            {
                return Int32.Parse(name.Substring(1, name.Length - 2));
            }
            return null;
        }

        public static Int32 GetAxis(string path)
        {
            Int32 result = 0;
            while (path.Length > 0 && path.Last() == ']')
            {
                result++;
                var pos = path.LastIndexOf('\\');
                if (pos >= 0)
                {
                    path = path.Substring(0, pos);
                }
                else
                    break;
            }
            return result;
        }


        public static string RemoveAllElements(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            if (path.Last() == ']')
            {
                var pos = path.LastIndexOf('\\');
                if (pos >= 0)
                {
                    if (path[pos + 1] == '[')
                    {
                        path = path.Substring(0, pos);
                        return RemoveAllElements(path);
                    }
                    else
                        throw new ArgumentException();
                }
                else
                {
                    if (path[0] == '[')
                        return string.Empty;
                    else
                        throw new ArgumentException();
                }
            }
            return path;
        }


        public static string RemoveElement(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            if (path.Last() == ']')
            {
                var pos = path.LastIndexOf('\\');
                if (pos >= 0)
                {
                    if(path[pos + 1] == '[')
                    {
                        path = path.Substring(0, pos);
                        return path;
                    }
                }
                else
                {
                    if (path[0] == '[')
                        return string.Empty;
                }
            }
            return null;
        }

        public static void RemoveLast(ref string path)
        {
            if(path != null)
            {
                var pos = path.LastIndexOf('\\');
                if (pos == 0)
                {
                    path = "\\";
                }
                else if (pos > 0)
                {
                    path = path.Substring(0, pos);
                }
                else
                    path = null;
            }
        }
    }
}
