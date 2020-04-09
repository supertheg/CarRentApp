using System.Collections.Generic;
using System.Linq;
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

        private DbSet<T> GetMockDbSet<T>(IEnumerable<T> dbSetData) where T:class 
        {
            var queryable = dbSetData.AsQueryable();
            var mock = new Mock<DbSet<T>>();

            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            
            return mock.Object;
        }

        [SetUp]
        public void Setup()
        {
            DbContextMock = new Mock<IDbContext>();

            Container = new UnityContainer(); 
            Container.RegisterInstance(DbContextMock.Object);
            Container.RegisterType<ICarService, CarService>();
        }

        [Test]
        public void GetCarTypes_ShouldReturnRightTypes()
        {
            // Arrange
            var types = new[] { new CarType { Name = "sport" }, new CarType { Name = "sedan" }, new CarType { Name = "van" } };
            DbContextMock.Setup(m => m.CarTypes).Returns(GetMockDbSet(types));

            // Act
            var results = Container.Resolve<ICarService>().GetCarTypes().ToList();

            // Assert
            Assert.NotNull(results);
            Assert.IsNotEmpty(results);
            CollectionAssert.AreEqual(results, new[] { "sport", "sedan", "van" });
        }

        [Test]
        public void SearchCars_ShouldReturnCarsAccordingCriteria()
        {
            //TODO
        }
    }
}