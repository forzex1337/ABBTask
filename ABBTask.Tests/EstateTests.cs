using ABBTask.Data.Data;
using ABBTask.Data.Schema.Entities;
using ABBTask.Infrastructure.Dto.Estate;
using ABBTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Globalization;

namespace ABBTask.Tests
{
    public class EstateTests
    {
        private readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();
        private readonly EstateValidatorService _uut;

        public EstateTests()
        {
            _uut = new EstateValidatorService(_loggerMock.Object);
        }

        [Fact]
        public void ValidateFilterEstate_ThrowsException_WhenAllPropertiesAreNotSet()
        {
            // Arrange
            var filter = new FilterEstate
            {
                ExpirationDateFrom = null,
                ExpirationDateTo = null,
                PriceFrom = 0,
                PriceTo = double.MaxValue
            };

            // Act
            var ex = Assert.Throws<Exception>(() => _uut.ValidateFilterEstate(filter));

            // Assert
            Assert.Equal("At least one property of FilterEstate must have a value", ex.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("not-a-date")]
        [InlineData("2022-15-15")]
        public void ValidateFilterEstate_ThrowsException_WhenExpirationDateFromIsInvalid(string date)
        {
            // Arrange
            var filter = new FilterEstate
            {
                ExpirationDateFrom = date,
                ExpirationDateTo = null,
                PriceFrom = 0,
                PriceTo = 100
            };

            // Act
            var ex = Assert.Throws<Exception>(() => _uut.ValidateFilterEstate(filter));

            // Assert
            Assert.Equal($"Incorrect date format ExpirationDateFrom : {date} ExpirationDateTo {filter.ExpirationDateTo}", ex.Message);
        }

        [Fact]
        public void FilterEstates_ReturnsCorrectResults()
        {
            // Arrange
            var estates = new List<Estate>() { };
            var filter = new FilterEstate
            {
                ExpirationDateFrom = "2022-01-01",
                ExpirationDateTo = "2022-12-31",
                PriceFrom = 1000,
                PriceTo = 2000
            };
            var expirationDateFrom = ParseToDateTime(filter.ExpirationDateFrom);
            var expirationDateTo = ParseToDateTime(filter.ExpirationDateTo);

            var options = new DbContextOptionsBuilder<ABBTaskDbContext>()
                .UseInMemoryDatabase(databaseName: "EstateListDatabase")
                .Options;

            using (var context = new ABBTaskDbContext(options))
            {
                context.Estates.Add(new Estate { Id = 1, Name = "Estate1", Description = "Estate1", ExpirationDate = DateTime.Parse("2022-01-01"), Price = 1500 });
                context.Estates.Add(new Estate { Id = 2, Name = "Estate2", Description = "Estate2", ExpirationDate = DateTime.Parse("2022-06-01"), Price = 1700 });
                context.Estates.Add(new Estate { Id = 3, Name = "Estate3", Description = "Estate3", ExpirationDate = DateTime.Parse("2022-12-31"), Price = 2000 });
                context.SaveChanges();

                estates = context.Estates.ToList();
            }

            // Assert

            Assert.True(estates.Count() > 0);
            Assert.True(estates.All(x => x.ExpirationDate >= expirationDateFrom && x.ExpirationDate <= expirationDateTo));
            Assert.True(estates.All(x => x.Price >= Convert.ToDecimal(filter.PriceFrom) && x.Price <= Convert.ToDecimal(filter.PriceTo)));
        }

        private DateTime ParseToDateTime(string filter)
        {
            return DateTime.ParseExact(filter, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}