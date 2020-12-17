namespace PizzaWorld.Testing
{
    public class OrderTests
    {
        [Fact]
        private void Test_OrderExists()
        {
            var sut = new Order();
            sut.Exist();

            var actual = sut;

            Assert.IsType<Order>(actual);
            Assert.NotNull(actual);
        }
    }
}