using System;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            var splitTime = SplitTime(aTime);

            var berlinHours = ConverHours(splitTime[0]);
            var berlinMinutes = ConvertMinutes(splitTime[1]);
            var berlinSeconds = ConvertSeconds(splitTime[2]);

            return string.Format("{0}{1}{2}", berlinSeconds, berlinHours, berlinMinutes);
        }
        string[] SplitTime(string Time)
        {
            var time = Time.Split(':');

            if (time.Length != 3)
            {
                throw new ArgumentException("Input parameter is invalid. Please use format HH:MM:SS", "Time");
            }

            return time;
        }
        string ConvertSeconds(string second)
        {
            return Convert.ToInt32(second) % 2 == 0 ? "Y" : "O";
        }
        string ConverHours(string hour)
        {
            var hourInt = Convert.ToInt32(hour);

            var highHour = hourInt / 5;
            var lowHour = hourInt % 5;

            var highHourString = string.Concat(new string('R', highHour), new string('O', 4 - highHour));
            var lowHourString = string.Concat(new string('R', lowHour), new string('O', 4 - lowHour));

            return FormatValues(highHourString, lowHourString);
        }
        string ConvertMinutes(string minute)
        {
            var minuteInt = Convert.ToInt32(minute);

            var highMinute = minuteInt / 5;
            var lowMinute = minuteInt % 5;

            var highMinuteString = string.Concat(new string('Y', highMinute), new string('O', 11 - highMinute));
            var lowMinuteString = string.Concat(new string('Y', lowMinute), new string('O', 4 - lowMinute));

            StringBuilder stringBuilder = new StringBuilder(highMinuteString);

            for (int i = 0; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i] == 'Y' && (i + 1) % 3 == 0)
                    stringBuilder[i] = 'R';
            }

            return FormatValues(stringBuilder.ToString(), lowMinuteString);
        }
        string FormatValues(string highValue, string lowValue)
        {
            return string.Format("\r\n{0}\r\n{1}", highValue, lowValue);
        }
    }
}
