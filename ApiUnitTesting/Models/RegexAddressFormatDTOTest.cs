using Api.Models;
using Xunit;

namespace ApiUnitTesting.Models
{
    public class RegexAddressFormatDTOTest
    {
        private readonly RegexAddressFormat regexAddressFormatWithAllRequiredFields;
        private readonly RegexAddressFormat regexAddressFormatWithAllOptionalFields;

        public RegexAddressFormatDTOTest()
        {
            regexAddressFormatWithAllRequiredFields = new RegexAddressFormat("NL", "[\\p{L} ]+$", "^[0-9]", "[\\p{L} ]+$", "^([0-9]{3})$");
            regexAddressFormatWithAllOptionalFields = new RegexAddressFormat("NL", "[\\p{L} ]?$", "^[0-9]?", "[\\p{L} ]?$", "^([0-9]{3})?$");
        }

        [Fact]
        public void WhenCreate_ShouldSetFields()
        {
            var sut = new RegexAddressFormatDTO(regexAddressFormatWithAllRequiredFields);

            Assert.Equal(sut.Country, regexAddressFormatWithAllRequiredFields.Country);
            Assert.False(sut.IsCityOptional);
            Assert.False(sut.IsHouseNumberOptional);
            Assert.False(sut.IsStreetOptional);
            Assert.False(sut.IsZipcodeOptional);

            sut = new RegexAddressFormatDTO(regexAddressFormatWithAllOptionalFields);
            Assert.Equal(sut.Country, regexAddressFormatWithAllOptionalFields.Country);
            Assert.True(sut.IsCityOptional);
            Assert.True(sut.IsHouseNumberOptional);
            Assert.True(sut.IsStreetOptional);
            Assert.True(sut.IsZipcodeOptional);
        }
    }
}
