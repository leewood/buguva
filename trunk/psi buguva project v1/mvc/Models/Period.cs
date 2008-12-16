using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Common;
using mvc.Models;
using LinqToSqlExtensions;

namespace mvc.Models
{
    public class Period
    {
        private MonthOfYear _start;
        private MonthOfYear _end;
        List<string> errors = new List<string>();

        private MonthOfYear PeriodStart
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        private MonthOfYear PeriodEnd
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        private void validatePeriod()
        {
            bool used = false;
            if (PeriodStart.Year < 1970)
            {
                errors.Add("Metai negali būti mažesni nei 1970. Norėta pasirinkti " + PeriodStart.Year.ToString());
                PeriodStart.Year = DateTime.Today.Year;

            }
            if (PeriodEnd.Year < 1970)
            {
                errors.Add("Metai negali būti mažesni nei 1970. Norėta pasirinkti " + PeriodEnd.Year.ToString());
                PeriodEnd.Year = DateTime.Today.Year;
            }

            if ((PeriodStart.Month < 1) || (PeriodStart.Month > 12))
            {
                errors.Add("Mėnuo gali būti tik intervale 1..12. Pasirinkta " + PeriodStart.Month.ToString());
                PeriodStart.Month = DateTime.Today.Month;
            }
            if ((PeriodEnd.Month < 1) || (PeriodEnd.Month > 12))
            {
                errors.Add("Mėnuo gali būti tik intervale 1..12. Pasirinkta " + PeriodStart.Month.ToString());
                PeriodEnd.Month = DateTime.Today.Month;
            }
            if (PeriodStart.Year * 12 + PeriodStart.Month > PeriodEnd.Year * 12 + PeriodEnd.Month)
            {
                errors.Add("Pradžios data negali būti didesnė nei pabaigos data");
                used = true;
                PeriodEnd.Year = PeriodStart.Year;
                PeriodEnd.Month = PeriodStart.Month;
            }
            if (PeriodStart.Year * 12 + PeriodStart.Month > DateTime.Today.Year * 12 + DateTime.Today.Month)
            {
                if (!used)
                {
                    errors.Add("Data negali viršyti šio mėnesio datos");
                }
                PeriodStart.Year = DateTime.Today.Year;
                PeriodStart.Month = DateTime.Today.Month;
            }
            if (PeriodEnd.Year * 12 + PeriodEnd.Month > DateTime.Today.Year * 12 + DateTime.Today.Month)
            {
                errors.Add("Data negali viršyti šio mėnesio datos");
                PeriodEnd.Year = DateTime.Today.Year;
                PeriodEnd.Month = DateTime.Today.Month;
            }

        }

        public Period(MonthOfYear start, MonthOfYear end)
        {
            PeriodStart = start;
            PeriodEnd = end;
            validatePeriod();
        }

        public Period(int startYear, int startMonth, int endYear, int endMonth)
        {
            PeriodStart = new MonthOfYear(startYear, startMonth);
            PeriodEnd = new MonthOfYear(endYear, endMonth);
            validatePeriod();
        }


        public List<string> getErrors()
        {
            return errors;
        }

        private double DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate)
        {
            double diff = 0;
            System.TimeSpan TS = new System.TimeSpan(endDate.Ticks - startDate.Ticks);

            switch (howtocompare.ToLower())
            {
                case "year":
                    diff = Convert.ToDouble(TS.TotalDays / 365);
                    break;
                case "month":
                    diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                    break;
                case "day":
                    diff = Convert.ToDouble(TS.TotalDays);
                    break;
                case "hour":
                    diff = Convert.ToDouble(TS.TotalHours);
                    break;
                case "minute":
                    diff = Convert.ToDouble(TS.TotalMinutes);
                    break;
                case "second":
                    diff = Convert.ToDouble(TS.TotalSeconds);
                    break;
            }

            return diff;
        }

        public int TotalWorkHoursInPeriod
        {
            get
            {
                DateTime start = new DateTime(PeriodStart.Year, PeriodStart.Month, 1);
                int days = DateTime.DaysInMonth(PeriodEnd.Year, PeriodEnd.Month);
                DateTime end = new DateTime(PeriodEnd.Year, PeriodEnd.Month, days);
                int result = (int)DateDiff("month", start, end) + 1;
                return result * 160;
            }
        }

    }
}
