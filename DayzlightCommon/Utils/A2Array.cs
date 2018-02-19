using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace DayzlightCommon.Utils
{
    public class A2Array : IEnumerable<A2Array>
    {
        private List<A2Array> childs = new List<A2Array>();
        private Object value = null;

        public bool IsNull()
        {
            return value == null;
        }

        public int Length()
        {
            return childs.Count;
        }

        public A2Array this[int index]
        {
            get { return childs[index]; }
            set { childs[index] = value; }
        }

        public static implicit operator String(A2Array ar) => ar.value?.ToString();
        public static implicit operator long(A2Array ar) => Convert.ToInt64(ar.value);
        public static implicit operator double(A2Array ar) => Convert.ToDouble(ar.value);
        public static implicit operator String[](A2Array ar)
        {
            String[] result = new String[ar.Length()];
            for(int i = 0; i < ar.Length(); ++i) result[i] = ar[i];
            return result;
        }

        public static implicit operator long[] (A2Array ar)
        {
            long[] result = new long[ar.Length()];
            for (int i = 0; i < ar.Length(); ++i) result[i] = ar[i];
            return result;
        }

        public static implicit operator double[] (A2Array ar)
        {
            double[] result = new double[ar.Length()];
            for (int i = 0; i < ar.Length(); ++i) result[i] = ar[i];
            return result;
        }

        public static A2Array Deserialize(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                return null;

            A2Array root = new A2Array();
            data = data.TrimStart(' ').TrimEnd(' ');
            if (data.StartsWith("["))
            {
                int counter = 1, last = 1;
                for (int i = 1; i < data.Length; ++i)
                {
                    if (data[i] == '[') counter++;
                    else if (data[i] == ']') counter--;

                    if (counter == 1 && data[i] == ',')
                    {
                        var child = A2Array.Deserialize(data.Substring(last, i - last));
                        if (child != null) root.childs.Add(child);
                        last = i + 1;
                    }
                    else if (counter == 0)
                    {
                        var child = A2Array.Deserialize(data.Substring(last, i - last));
                        if (child != null) root.childs.Add(child);
                        break;
                    }
                }
            }
            else if (data.StartsWith("\"") || data.StartsWith("\'"))
            {
                root.value = data.TrimStart('\"', '\'').TrimEnd('\"', '\'');
            }
            else if (data.Equals("<null>", StringComparison.CurrentCultureIgnoreCase))
            {
                root.value = null;
            }
            else if (data.Contains('.'))
            {
                root.value = double.Parse(data, CultureInfo.InvariantCulture);
            }
            else
            {
                root.value = long.Parse(data, CultureInfo.InvariantCulture);
            }
            return root;
        }

        public IEnumerator<A2Array> GetEnumerator()
        {
            return childs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return childs.GetEnumerator();
        }
    }
}
