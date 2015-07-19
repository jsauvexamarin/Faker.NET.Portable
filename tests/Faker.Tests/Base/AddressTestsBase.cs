using NUnit.Framework;

namespace Faker.Tests.Base
{
    public abstract class AddressTestsBase
    {
        private const string ADDRESS_REGEX1 = @"^[\d]{3,5} (\w+)+ \w+";
        private const string ADDRESS_REGEX2 = @"^[\d]{3,5} \w'\w+ \w+";

        [Test]
        [Repeat(1000)]
        public void Should_Get_Building_Number()
        {
            string buildingNum = Address.BuildingNumber();

            Assert.That(buildingNum, Has.Length.GreaterThanOrEqualTo(3)
                                        .Or.Length.LessThanOrEqualTo(5));
            //Assert.That(buildingNum, Is.StringMatching("^[0-9]+$"));
            buildingNum.AssertFormats(Resources.Address.BuildingNumber.ToFormat());
        }

        [Test]
        [Repeat(1000)]
        public virtual void Should_Get_City()
        {
            string cityPrefixFormat = Resources.Address.CityPrefix.ToFormat();
            string firstNameFormat = Resources.Name.First.ToFormat();
            string citySuffixFormat = Resources.Address.CitySuffix.ToFormat();
            string lastNameFormat = Resources.Name.Last.ToFormat();

            string city = Address.City();

            city.AssertFormats(cityPrefixFormat + " " + firstNameFormat + citySuffixFormat,
                               cityPrefixFormat + " " + firstNameFormat,
                               firstNameFormat + citySuffixFormat, lastNameFormat + citySuffixFormat);
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_City_Prefix()
        {
            string[] existingPrefixies = Resources.Address.CityPrefix.Split(Config.SEPARATOR);

            string prefix = Address.CityPrefix();

            Assert.That(new[] {prefix}, Is.SubsetOf(existingPrefixies));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_City_Suffix()
        {
            string[] existingSuffixes = Resources.Address.CitySuffix.Split(Config.SEPARATOR);
            string suffix = Address.CitySuffix();

            Assert.That(new[] {suffix}, Is.SubsetOf(existingSuffixes));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_Country()
        {
            string[] existingCountries = Resources.Address.Country.Split(Config.SEPARATOR);

            string country = Address.Country();

            Assert.That(new[] {country}, Is.SubsetOf(existingCountries));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_Country_Code()
        {
            string[] existingCountryCodes = Resources.Address.CountryCode.Split(Config.SEPARATOR);

            string countryCode = Address.CountryCode();

            Assert.That(new[] {countryCode}, Is.SubsetOf(existingCountryCodes));
        }

        [Test]
        public void Should_Get_Default_Country()
        {
            string[] possibleCountries = Resources.Address.DefaultCountry.Split(Config.SEPARATOR);

            string actualCountry = Address.DefaultCountry();

            Assert.That(new[] {actualCountry}, Is.SubsetOf(possibleCountries));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_Latitude()
        {
            double actual = Address.Latitude();

            Assert.That(actual, Is.GreaterThanOrEqualTo(-90)
                                  .And.LessThanOrEqualTo(90));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_Longitude()
        {
            double actual = Address.Longitude();

            Assert.That(actual, Is.GreaterThanOrEqualTo(-180)
                                  .And.LessThanOrEqualTo(180));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_State()
        {
            string[] expectedStates = Resources.Address.State.Split(Config.SEPARATOR);

            string state = Address.State();

            Assert.That(new[] {state}, Is.SubsetOf(expectedStates));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_State_Abbreviation()
        {
            string[] possibleAbberivations = Resources.Address.StateAbbr.Split(Config.SEPARATOR);

            string stateAbbr = Address.StateAbbreviation();

            Assert.That(new[] {stateAbbr}, Is.SubsetOf(possibleAbberivations));
        }

        [Test]
        [Repeat(10000)]
        public virtual void Should_Get_Street_Address()
        {
            string buildingNumberFormat = Resources.Address.BuildingNumber.ToFormat();
            string firstNameFormat = Resources.Name.First.ToFormat();
            string lastNameFormat = Resources.Name.Last.ToFormat();
            string addressStreetSuffixFormat = Resources.Address.StreetSuffix.ToFormat();

            string address = Address.StreetAddress();
        }

        [Test]
        [Repeat(10000)]
        public virtual void Should_Get_Street_Address_With_Secondary_Address()
        {
            string secondary = Resources.Address.SecondaryAddress.ToFormat(true);

            string address = Address.StreetAddress(true);

            Assert.That(address,
                        Is.StringMatching(ADDRESS_REGEX1 + " " + secondary + "$")
                          .Or.StringMatching(ADDRESS_REGEX2 + " " + secondary + "$"));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_TimeZone()
        {
            string[] possibleResults = Resources.Address.TimeZone.Split(Config.SEPARATOR);

            string result = Address.TimeZone();

            Assert.That(new[] {result}, Is.SubsetOf(possibleResults));
        }

        [Test]
        [Repeat(10000)]
        public void Should_Get_Zip_Code()
        {
            string zipcodeRegex = "^" + Resources.Address.ZipCode.ToFormat(true) + "$";

            string zipcode = Address.ZipCode();

            Assert.That(zipcode, Is.StringMatching(zipcodeRegex));
        }
    }
}
