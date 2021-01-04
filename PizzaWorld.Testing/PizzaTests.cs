using PizzaWorld.Domain.Abstracts;
using PizzaWorld.Domain.Models;
using System;
using Xunit;

namespace PizzaWorld.Testing
{
    public class PizzaTests
    {
        public PizzaTests()
        {
            Test_PizzaExists();
            Test_CheeseIsCorrect();
            Test_HawaiianIsCorrect();
            Test_MeatIsCorrect();
            Test_PestoIsCorrect();
            Test_CustomIsCorrect();
        }

        [Fact]
        public void Test_PizzaExists()
        {
            var sut = new PestoPizza();
            var actual = sut;
            Assert.IsType<PestoPizza>(actual);
            Assert.IsType<APizzaModel>(actual);
            Assert.NotNull(actual);
        }
        [Fact]
        public void Test_CheeseIsCorrect()
        {
            var sut = new CheesePizza();
            Console.WriteLine(sut.Name,sut.Size,sut.Crust,sut.Sauce,sut.ToppingsString(),sut.Price);
        }
        [Fact]
        public void Test_HawaiianIsCorrect()
        {
            var sut = new HawaiianPizza();
            Console.WriteLine(sut.Name,sut.Size,sut.Crust,sut.Sauce,sut.ToppingsString(),sut.Price);
        }
        [Fact]
        public void Test_MeatIsCorrect()
        {
            var sut = new MeatPizza();
            Console.WriteLine(sut.Name,sut.Size,sut.Crust,sut.Sauce,sut.ToppingsString(),sut.Price);
        }
        [Fact]
        public void Test_PestoIsCorrect()
        {
            var sut = new PestoPizza();
            Console.WriteLine(sut.Name,sut.Size,sut.Crust,sut.Sauce,sut.ToppingsString(),sut.Price);
        }
        [Fact]
        public void Test_CustomIsCorrect()
        {
            var sut = new CustomPizza(new CheesePizza());
            Console.WriteLine("Cheese base:",sut.Name,sut.Size,sut.Crust,sut.Sauce,sut.ToppingsString(),sut.Price);
            var sut1 = new CustomPizza(new PestoPizza());
            Console.WriteLine("Pesto base:",sut1.Name,sut1.Size,sut1.Crust,sut1.Sauce,sut1.ToppingsString(),sut1.Price);
        }
    }
}