using System;
using System.Reflection;
using KinderApi.DTOs;

namespace KinderApi.helper
{
    public static class ExstensionMethods
    {
        public static int GetYears(this DateTime birthdate)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            TimeSpan span = DateTime.Now - birthdate;
            int years = (zeroTime + span).Year - 1;

            return years;
        }

        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }

        public static int CountNotNullField<T>(this T t) where T : PreferenceDto
        {
            var userPrefType = t.GetType();
            PropertyInfo[] fields = userPrefType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            int count = 0;

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].GetValue(t) != null)
                {
                    count++;
                }
            }
            return count;
        }
    }
}