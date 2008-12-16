using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class MonthOfYear : IComparable
    {
        private int _year = 0;
        private int _month = 1;

        public static string getMonthName(int month)
        {
            switch (month)
            {
                case 1: return "Sausis";
                case 2: return "Vasaris";
                case 3: return "Kovas";
                case 4: return "Balandis";
                case 5: return "Gegužė";
                case 6: return "Birželis";
                case 7: return "Liepa";
                case 8: return "Rugpjūtis";
                case 9: return "Rugsėjis";
                case 10: return "Spalis";
                case 11: return "Lapkritis";
                case 12: return "Groudis";

            }
            return "";
        }

        public static string getMonth(int month)
        {
            string result = month.ToString();
            if (result.Length < 2)
            {
                result = "0" + result;
            }
            return result;
        }


        public string ShortName()
        {
            return getMonthName(this.Month);
        }

        public static SelectList monthsList(object selected)
        {
            System.Collections.Generic.Dictionary<string, int> list = new Dictionary<string, int>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(getMonthName(i), i);
            }
            return new SelectList(list, "Value", "Key", selected);
        }

        public static SelectList monthsListUnTitled(object selected)
        {
            System.Collections.Generic.Dictionary<string, int> list = new Dictionary<string, int>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(getMonth(i), i);
            }
            return new SelectList(list, "Value", "Key", selected);
        }


        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }
            set
            {
                if ((value < 13) && (value > 0))
                {
                    _month = value;
                }
                else
                {
                    //throw new FormatException("Month must be between 1 and 12");
                }
            }
        }


        public MonthOfYear()
        {
            Year = DateTime.Today.Year;
            Month = DateTime.Today.Month;
        }

        public MonthOfYear(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public MonthOfYear(int month)
        {
            Year = DateTime.Today.Year;
            Month = month;
        }

        public override string ToString()
        {
            string yearString = Year.ToString();
            string monthString = Month.ToString();
            if (monthString.Length < 2)
            {
                monthString = "0" + monthString;
            }
            while (yearString.Length < 4)
            {
                yearString = "0" + yearString;
            }
            return yearString + "-" + monthString;
        }

        public override bool Equals(object obj)
        {
            try
            {
                MonthOfYear toCompareWith = (MonthOfYear)obj;
                return ((this.Year == toCompareWith.Year) && (this.Month == toCompareWith.Month));
            }
            catch (Exception)
            {
                return base.Equals(obj);
            }
        }

        public int CompareTo(object o)
        {
            try
            {
                MonthOfYear toCompareWith = (MonthOfYear)o;
                if (this.Year < toCompareWith.Year)
                {
                    return -1;
                }
                else if (this.Year > toCompareWith.Year)
                {
                    return 1;
                }
                else if (this.Month < toCompareWith.Month)
                {
                    return -1;
                }
                else if (this.Month > toCompareWith.Month)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }
    }
}
