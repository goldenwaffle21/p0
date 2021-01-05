using PizzaWorld.Domain.Abstracts;
using PizzaWorld.Domain.Factories;
using PizzaWorld.Domain.Models;
using System;
using System.Collections.Generic;
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
        /*[Fact]
        public void Test_FactoryWorks()
        {
        //Given
        private GenericPizzaFactory _pizzaFactory = new GenericPizzaFactory();
        
        //When
        public List<APizzaModel> Pizzas {get;set;}
        Pizzas.Add(_pizzaFactory.Make<HawaiianPizza>());

        //Then
        }*/

    internal struct NewStruct
    {
        public object Item1;
        public object Item2;

        public NewStruct(object item1, object item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(object obj)
        {
            return obj is NewStruct other &&
                   EqualityComparer<object>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<object>.Default.Equals(Item2, other.Item2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2);
        }

        public void Deconstruct(out object item1, out object item2)
        {
            item1 = Item1;
            item2 = Item2;
        }

        public static implicit operator (object, object)(NewStruct value)
        {
            return (value.Item1, value.Item2);
        }

        public static implicit operator NewStruct((object, object) value)
        {
            return new NewStruct(value.Item1, value.Item2);
        }
    }
}
}