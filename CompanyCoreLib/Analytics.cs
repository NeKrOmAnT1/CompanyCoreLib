using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCoreLib
{
    public class Analytics
    {
        public List<DateTime> PopularMonths(List<DateTime> dates)
        {
            #region (из документа)Прогноз строится на основе предыдущего года, как реализовать это иначе без понятия 
            int firstYear = dates[0].Year;
            bool allYearsEqual = dates.All(date => date.Year == firstYear);
            if (!allYearsEqual)
            {
                throw new ArgumentException("Все года в списке должны быть равны.");
            }
            #endregion

            #region Создание списка с характеристиками популярности
            List<DateTime> numberDates = new List<DateTime>();

            foreach (DateTime date in dates)
            {
                DateTime monthDate = new DateTime(date.Year, date.Month, 1);
                bool monthFound = false;
                for (int i = 0; i < numberDates.Count; i++)
                {
                    if (numberDates[i].Year == monthDate.Year && numberDates[i].Month == monthDate.Month)
                    {
                        numberDates[i] = numberDates[i].AddDays(1);
                        monthFound = true;
                        break;
                    }
                }
                if (!monthFound)
                {
                    numberDates.Add(monthDate);
                }
            }
            #endregion

            #region  (из документа)В случае совпадения характеристик популярности сперва нужно вывести более ранние месяцы
            numberDates.Sort((x, y) =>
            {
                if (x.Day == y.Day)
                {
                    return x.CompareTo(y);
                }
                return y.Day.CompareTo(x.Day);
            });
            #endregion

            #region Формирование списка в нужном мне формате
            List<DateTime> popularMonths = new List<DateTime>();
            foreach (DateTime monthDate in numberDates)
            {
                DateTime firstDayOfMonth = new DateTime(monthDate.Year, monthDate.Month, 1);
                string correctFormat = firstDayOfMonth.ToString("yyyy-MM-dd HH:mm");
                var datatime = DateTime.Parse(correctFormat);
                popularMonths.Add(datatime);
            }
            #endregion
            return popularMonths;
        }
    }
}
