using PizzaWorld.Domain.Models;
using Xunit;

namespace PizzaWorld.Testing
{
    internal class UserTests
    {
        [Fact]
        public void Test_UserExists()
        {
        //Given
        User user = new User();
        
        //When
        User user1 = new User();
        
        //Then
        var actual = user;
        Assert.NotNull(actual);
        Assert.IsType<User>(actual);
        }
    }
}