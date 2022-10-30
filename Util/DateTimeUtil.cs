using System.Globalization;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Util
{
    public static class DateTimeUtil
    {
        private const string _regularDateFormat = "hh:mm dd.MM.YYYY";
        public static DateTime ToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, _regularDateFormat, CultureInfo.InvariantCulture);
        }

        public static string FromDateTime(DateTime dateTime)
        {
            return dateTime.ToString();
        }

        public static List<DateTime> GenerateAppointmentTimeList(List<WorkingHours> workingHours)
        {
            List<DateTime> result = new();

            if (!workingHours.Any())
                return result;


            List<DateTime> busyDates =
                new AppointmentRepository()
                .FindAllByDoctorId(workingHours[0].DoctorId)
                .Select(item => DateTime.Parse(item.Time)).ToList();


            DateTime currentTime = DateTime.Now;

            while (!DateTime.Now.Day.Equals(currentTime.AddDays(14).Day))
            {
                WorkingHours wh = workingHours.Find(item => item.DayOfWeek.ToString().Equals(currentTime.DayOfWeek.ToString()));

                if (wh != null)
                {
                    DateTime dateTimeStart = new(currentTime.Year, currentTime.Month, currentTime.Day, wh.HourFrom, 0, 0);
                    DateTime dateTimeTill = dateTimeStart.AddHours(wh.HoursCount);
                    while (DateTime.Compare(dateTimeStart, dateTimeTill) != 0)
                    {
                        if (!busyDates.Contains(dateTimeStart))
                            result.Add(dateTimeStart);
                        dateTimeStart = dateTimeStart.AddMinutes(30);
                    }
                }
                currentTime = currentTime.AddDays(1);
            }

            return result;

        }
    }
}
