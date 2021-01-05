using PizzaWorld.Domain.Models;
using Xunit;

namespace PizzaWorld.Testing
{
    public class OrderTests
    {
        [Fact]
        public void Test_OrderExists()
        {
            //Given
            var sut = new Order();

            //When
            Order sut1 = new Order();    //memory allocation
            
            //Then
            var actual = sut;
            Assert.IsType<Order>(actual);
            Assert.NotNull(actual);
        }
    }
}