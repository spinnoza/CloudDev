using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class DeviceAddress : ValueObject
    {
        public string Street { get; private set; } = null!;

        public string City { get; private set; } = null!;

        public string State { get; private set; } = null!;

        public string Country { get; private set; } = null!;

        public string ZipCode { get; private set; } = null!;

        public DeviceAddress() { }

        public DeviceAddress(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
