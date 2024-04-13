using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var sr = new StreamReader("input.txt");
            var sw = new StreamWriter("output.txt");

            var str = sr.ReadLine();

            sw.WriteLine(OptimizeString(str));

            sr.Close();
            sw.Close();
        }

        public static string OptimizeString(string str, bool isSkipSingle = true)
        {
            var list = new List<Pair>();

            #region Optimize_string
            for (int i = 0; i < str.Length; ++i)
            {
                var substr = str.Substring(i);
                var z = Z_Function(substr);


                int maxPos = GetMax_Z(z, isSkipSingle);



                if (z[maxPos] <= 1 || z[maxPos] < maxPos)
                {
                    list.Add(new Pair(str[i].ToString(), 1));
                    continue;
                }

                bool isValid = true;


                for (int j = 0; j < z[maxPos] / maxPos; ++j)
                {

                    if ((j + 1) * maxPos + maxPos > substr.Length)
                    {
                        isValid = false;
                        break;
                    }


                    if (Hash(substr, 0, maxPos) != Hash(substr, (j + 1) * maxPos, maxPos))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {

                    list.Add(new Pair(str[i].ToString(), 1));
                }
                else
                {
                    list.Add(new Pair(substr.Substring(0, maxPos), z[maxPos] / maxPos + 1));
                    i += maxPos + z[maxPos] / maxPos * maxPos - 1;
                }
            }
            #endregion

            for (int i = 0; i < list.Count - 1; ++i)
            {
                if (list[i].Value == 1 && list[i + 1].Value == 1)
                {
                    list[i].Key = list[i].Key + list[i + 1].Key;
                    list.RemoveAt(i + 1);
                    i--;
                }
            }

            for (int i = 1; i < list.Count - 1; ++i)
            {
                if (list[i + 1].Value == 1 && list[i - 1].Value == 1 && Find(list[i].Key, list[i + 1].Key) == 0)
                {
                    list[i - 1].Key = list[i - 1].Key + list[i + 1].Key;
                    list[i].Key = list[i].Key.Substring(list[i + 1].Key.Length) + list[i + 1].Key;
                    list.RemoveAt(i + 1);
                }
            }

            for (int i = 1; i < list.Count - 1; ++i)
            {
                if (list[i + 1].Value == 1 && list[i - 1].Value == 1 &&
                    Find(list[i].Key, list[i - 1].Key) == list[i].Key.Length - list[i - 1].Key.Length)
                {
                    list[i].Key = list[i - 1].Key + list[i].Key.Substring(0, list[i].Key.Length - list[i - 1].Key.Length);
                    list[i + 1].Key = list[i - 1].Key + list[i + 1].Key;
                    list.RemoveAt(i - 1);
                }
            }

            if (!isSkipSingle)
            {
                for (int i = 1; i < list.Count - 1; ++i)
                {
                    if (list[i].Value == 4 && list[i].Key.Length == 1 &&
                        (list[i - 1].Value == 1 || list[i + 1].Value == 1))
                        list[i] = new Pair(list[i].Key + list[i].Key + list[i].Key + list[i].Key, 1);
                }
            }
            else
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    if (list[i].Value == 1)
                        list[i].Key = OptimizeString(list[i].Key, false);
                }
            }

            string result = "";

            for (int i = 0; i < list.Count; ++i)
            {
                var substr = list[i].Key;
                var num = list[i].Value;

                if (num == 1)
                {
                    if (i > 0 && list[i - 1].Value == 1)
                        result += substr;
                    else if (i > 0 && list[i - 1].Key.Length <= 2 && list[i - 1].Value <= 2)
                        result += substr;
                    else
                    {
                        if (result != "")
                            result += "+";

                        result += substr;
                    }
                }
                else if (num == 2)
                {
                    if (substr.Length == 1)
                    {
                        if (i > 0 && list[i - 1].Value == 1)
                            result += substr + substr;
                        else
                        {
                            if (result != "")
                                result += "+";

                            result += substr + substr;
                        }
                    }
                    else
                    {
                        if (result != "")
                            result += "+";

                        result += substr + "*2";
                    }
                }
                else
                {
                    if (result != "")
                        result += "+";

                    result += substr + "*" + $"{num}";
                }
            }

            if (result.Length < str.Length)
                return result;

            return str;
        }

        public static int GetMax_Z(int[] z, bool isSkipSingle)
        {
            int max = 0;
            int maxPos = 0;

            for (int i = 0; i < z.Length; ++i)
            {
                if (z[i] == 0)
                    continue;

                if (i == 1 && isSkipSingle)
                {
                    if (z[i] > 0)
                        i += z[i] - 1;

                    continue;
                }

                if (z[i] > max)
                {
                    max = z[i];
                    maxPos = i;
                }
                else if (z[i] < max)
                    break;
            }

            return maxPos;
        }

        static int Find(string str, string substr)
        {
            var z = Z_Function(substr + "#" + str);

            for (int i = 0; i < z.Length; ++i)
            {
                if (z[i] == substr.Length)
                    return i - substr.Length - 1;
            }

            return -1;
        }

        static long Hash(string str, int startIndex, int length)
        {
            int hash = 0;

            for (int i = startIndex; i < startIndex + length; ++i)
            {
                hash += hash << 5 + str[i];
            }

            return hash;
        }

        private static int[] Z_Function(string s)
        {
            int n = s.Length;
            int[] z = new int[n];
            int l = 0, r = 0;

            for (int i = 1; i < n; ++i)
            {
                if (i <= r)
                    z[i] = Math.Min(r - i + 1, z[i - l]);

                while (i + z[i] < n && s[z[i]] == s[i + z[i]])
                    ++z[i];

                if (i + z[i] - 1 > r)
                {
                    l = i;
                    r = i + z[i] - 1;
                }
            }

            return z;
        }
    }
    public class Pair
    {
        public string Key { get; set; }
        public int Value { get; set; }

        public Pair() { }

        public Pair(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
