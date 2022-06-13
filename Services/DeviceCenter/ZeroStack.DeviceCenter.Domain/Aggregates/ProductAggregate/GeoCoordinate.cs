using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public record GeoCoordinate
    {
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public override string ToString() => $"{Latitude},{Longitude}";

        public static implicit operator string(GeoCoordinate geo) => geo.ToString();

        public static explicit operator GeoCoordinate(string str)
        {
            GeoCoordinate geoCoordinate = new();

            if (!string.IsNullOrWhiteSpace(str))
            {
                double[] arr = Array.ConvertAll(str.Split(','), i => Convert.ToDouble(i));

                geoCoordinate.Latitude = arr.First();
                geoCoordinate.Longitude = arr.Last();
            }

            return geoCoordinate;
        }
    }
}
