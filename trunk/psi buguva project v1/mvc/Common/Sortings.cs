using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Common
{
    public class Sortings
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();

        public string toParam()
        {
            string result = "";
            string next = "";
            foreach (KeyValuePair<string, int> pair in dict)
            {
                result += next + pair.Key + ":" + pair.Value.ToString();
                next = ";";
            }
            result = HttpUtility.UrlEncode(result);
            return result;
        }

        public Sortings()
        {
        }

        public Sortings(string param)
        {
            fromParam(param);
        }

        public void fromParam(string param)
        {
            param = HttpUtility.UrlDecode(param);
            dict = new Dictionary<string, int>();
            if ((param != null) && (param != ""))
            {
                char[] separs = new char[1];
                separs[0] = ';';
                string[] pairs = param.Split(separs);

                separs[0] = ':';
                foreach (string s in pairs)
                {
                    string[] p2 = s.Split(separs);
                    int value = 0;
                    try
                    {
                        value = int.Parse(p2[1]);
                    }
                    catch (Exception)
                    {
                    }

                    dict.Add(p2[0], value);
                }
            }
        }

        public void setSorting(string name, int sort)
        {
            if (dict.ContainsKey(name))
            {
                if (sort == 0)
                {
                    sort = dict[name] + 1;
                    if (sort > 2)
                    {
                        sort = 0;
                    }
                    dict[name] = sort;
                }
                if (dict[name] == sort)
                {
                    dict[name] = 0;
                }
                else
                {
                    dict[name] = sort;
                }
            }
            else
            {
                dict.Add(name, sort);
            }
        }

        public int getSorting(string name)
        {
            if (dict.ContainsKey(name))
            {
                return dict[name];
            }
            else
            {
                return 0;
            }
        }

        public string getSortString()
        {

            string result = "";
            string next = "";
            foreach (KeyValuePair<string, int> pair in dict)
            {
                if (pair.Value > 0)
                {
                    result += next + pair.Key + ((pair.Value > 1) ? " descending" : "");
                    next = ",";
                }
            }
            return result;
        }

    }
}
