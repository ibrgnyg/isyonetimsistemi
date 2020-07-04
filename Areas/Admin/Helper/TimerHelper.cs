using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Areas.Admin.Helper
{
    public static class Helper
    {
        public static string TimeHelper(this DateTime date)
        {

            var timeSpan = DateTime.Now - date;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("şimdi", timeSpan.Seconds);

            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? string.Format("{0} dakika önce", timeSpan.Minutes) : "bir dakika önce";

            else if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? String.Format("{0} saat önce", timeSpan.Hours) : " bir saat önce";

            else if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ? String.Format("{0} gün önce", timeSpan.Days) : "dün";

            else if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? String.Format("{0} ay önce", timeSpan.Days / 30) : " bir ay önce";

            return timeSpan.Days > 365 ? String.Format("{0} yıl önce", timeSpan.Days / 365) : " bir yıl önce";
        }

    }
}
