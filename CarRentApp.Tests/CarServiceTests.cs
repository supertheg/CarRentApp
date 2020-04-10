using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Unity;

namespace CarRentApp.Tests
{
    public class CarServiceTests
    {
        private UnityContainer Container { get; set; }
        private Mock<IDbContext> DbContextMock { get; set; }

        private DbSet<T> GetMockDbSet<T>(IEnumerable<T> dbSetData) where T : class
        {
            var queryable = dbSetData.AsQueryable();
            var mock = new Mock<DbSet<T>>();

            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mock.Object;
        }

        public class TestContext : CarRentDbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("CarRentDB");
            }
        }

        [SetUp]
        public void Setup()
        {
            DbContextMock = new Mock<IDbContext>();

            Container = new UnityContainer();
            Container.RegisterType<IDbContext, TestContext>();
            Container.RegisterType<ISearchCarService, SearchCarService>();
        }

        [TestCase(true, true, CarType.Sedan, 1)]
        [TestCase(true, true, CarType.Sport, 1)]
        [TestCase(true, true, CarType.Van, 1)]
        [TestCase(true, false, CarType.None, 6)]
        [TestCase(false, true, CarType.None, 6)]
        [TestCase(true, true, CarType.None, 3)]
        [TestCase(false, false, CarType.None, 12)]
        public async Task SearchCars_ShouldReturnCarsAccordingCriteria(bool radio, bool cond, CarType type, int expected)
        {
            // Arrange
            var ctx = Container.Resolve<IDbContext>();
            await ctx.Database.EnsureDeletedAsync();

            var cars = new List<Car> {
                new Sedan(true, true), new Sedan(true, false), new Sedan(false, true), new Sedan(false, false),
                new Sport(true, true), new Sport(true, false), new Sport(false, true), new Sport(false, false),
                new Van(true, true), new Van(true, false), new Van(false, true), new Van(false, false)
            };
            ctx.Cars.AddRange(cars);
            await ctx.SaveChangesAsync();

            var filter = new CriteriaViewModel { Radio = radio, Conditioning = cond, Type = type, PriceFrom = null, PriceTo = null };

            // Act
            var results = await Container.Resolve<ISearchCarService>().SearchCarsAsync(filter);

            // Assert
            Assert.AreEqual(expected, results.Count());
        }
    }
}