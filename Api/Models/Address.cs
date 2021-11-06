namespace Api.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public Address(string country, string city, string houseNumber, string street, string zipCode)
        {
            Country = country;
            City = city;
            HouseNumber = houseNumber;
            Street = street;
            ZipCode = zipCode;
        }
    }
}
