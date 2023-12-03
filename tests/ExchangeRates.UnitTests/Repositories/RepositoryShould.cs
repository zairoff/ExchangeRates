using ExchangeRates.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace ExchangeRates.UnitTests.Repositories
{
    public class RepositoryShould
    {
        public RepositoryShould()
        {

        }

        [Fact]
        public async Task AddAsync()
        {
            // Arrange
            var testObject = new TestClass();

            var context = Substitute.For<AppDbContext>();
            var dbSetMock = Substitute.For<DbSet<TestClass>>();
            context.Set<TestClass>().Returns(dbSetMock);

            // Act
            var repository = new Repository<TestClass>(context);
            await repository.AddAsync(testObject);

            //Assert
            context.Received(1).Set<TestClass>();
            await dbSetMock.Received(1).AddAsync(testObject);
        }
    }
}
