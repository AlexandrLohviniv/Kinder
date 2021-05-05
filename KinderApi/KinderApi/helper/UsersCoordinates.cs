using System;
using System.Globalization;

namespace KinderApi.helper
{
    public class UsersCoordinates
    {
        public double LatitudeUser1 { set; internal get; }
        public double LongtitudeUser1 { set; internal get; }
        public double LatitudeUser2 { set; internal get; }
        public double LongtitudeUser2 { set; internal get; }
        public double? Distance
        {
            get
            {
                if (LatitudeUser1 == 0d || LatitudeUser2 == 0d || LongtitudeUser1 == 0d || LongtitudeUser2 == 0d)
                {
                    return null;
                }

                return DistanceCalculator.CalculateDistance(this);
            }
        }

        public static UsersCoordinates Parse(string coords1, string coords2, char delimeter = ',')
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            string[] users1Coords = coords1.Split(delimeter);
            string[] users2Coords = coords2.Split(delimeter);

            double lat1 = Convert.ToDouble(users1Coords[0], nfi);
            double long1 = Convert.ToDouble(users1Coords[1], nfi);
            double lat2 = Convert.ToDouble(users2Coords[0], nfi);
            double long2 = Convert.ToDouble(users2Coords[1], nfi);

            return new UsersCoordinates
            {
                LatitudeUser1 = lat1,
                LongtitudeUser1 = long1,
                LatitudeUser2 = lat2,
                LongtitudeUser2 = long2
            };
        }

    }
}