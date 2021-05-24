using KinderApi.Models;
using System;
using System.Linq;
using System.Reflection;

namespace KinderApi.DTOs
{
    public class PreferenceDto
    {
        public Rate? SmokeRate { get; set; }
        public Rate? BabyRate { get; set; }
        public Rate? HeightRate { get; set; }
        public Rate? PetsRate { get; set; }
        public Rate? RelationshipRate { get; set; }
        public Sexuality? Sex { get; set; }
        public Rate? DrinkingRate { get; set; }

        public static int operator -(PreferenceDto a, PreferenceDto b)
        {
            var dtoType = typeof(PreferenceDto);
            PropertyInfo[] fields = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var sexVal = fields.FirstOrDefault(f => f.Name == "Sex");

            if (sexVal.GetValue(a) != null && sexVal.GetValue(b) != null)
            {
                if (a.Sex == b.Sex)
                    return 7;
            }

            int result = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Name == "Sex")
                    continue;

                if (fields[i].GetValue(a) != null &&
                    fields[i].GetValue(b) != null)
                {
                    Rate rateA = (Rate)fields[i].GetValue(a);
                    Rate rateB = (Rate)fields[i].GetValue(b);
                    result += Math.Abs(rateA - rateB);
                }
            }
            return result;
        }
    }
}