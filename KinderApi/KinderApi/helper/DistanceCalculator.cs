using System;

namespace KinderApi.helper
{
    public class DistanceCalculator
    {
        public static double CalculateDistance(UsersCoordinates coords)
        {
            double deltaLatitude = (coords.LatitudeUser1 - coords.LatitudeUser2).ToRadians();
            double deltaLongtitude = (coords.LongtitudeUser1 - coords.LongtitudeUser2).ToRadians();

            double a = Math.Pow(Math.Sin(deltaLatitude/2.0), 2) + Math.Cos(coords.LatitudeUser1.ToRadians()) * Math.Cos(coords.LatitudeUser2.ToRadians()) * Math.Pow(Math.Sin(deltaLongtitude/2.0), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double d = 6371000 * c;

            return d;
        }
    }

}