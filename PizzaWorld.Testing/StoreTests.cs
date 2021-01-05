using PizzaWorld.Domain.Models;
using Xunit;

namespace PizzaWorld.Testing
{
    internal class StoreTests
    {
        [Fact]
        public void Test_StoreExists()
        {
        //Given
        Store store = new Store();
        
        //When
        Store store2 = new Store();

        //Then
        var actual = store;
        Assert.IsType<Store>(actual);
        Assert.NotNull(actual);
        }
    }
}