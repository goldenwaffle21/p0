using PizzaWorld.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace PizzaWorld.Domain.Singletons
{
    public class ClientSingleton
    {
        private static ClientSingleton _instance;

        public static ClientSingleton Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientSingleton();
                }
                return _instance;
            }
        }
        
        public List<Store> Stores {get;set;}
        
        private ClientSingleton()
        {
            Read();
        }
        
        public void MakeStore()
        {
            var s = new Store();
            Stores.Add(s);
            Save();
        }

        public Store SelectStore()
        {
            int.TryParse(Console.ReadLine(), out int input);
            //converts user input to an int, automatically catching exceptions.

            if (Stores.ElementAtOrDefault(input) == null)   //if the input is invalid
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                return SelectStore();
            }

            //return Stores.FirstOrDefault(s => s == input);  //property must be unique, and input must be correct
            else {return Stores.ElementAtOrDefault(input);}  //returns null if input is invalid
            //return Stores[input];  //returns exception if input is invalid
        }

        private const string _path = @"./pizzaworld.xml";

        private void Save()
        {
            var file = new StreamWriter(_path);
            var xml = new XmlSerializer(typeof(List<Store>));
            xml.Serialize(file,Stores);
        }
        public void Read()
        {
            if (!File.Exists(_path))
            {
                Stores = new List<Store>();
                return;
            }

            var file = new StreamReader(_path);
            var xml = new XmlSerializer(typeof(List<Store>));
            Stores = xml.Deserialize(file) as List<Store>; //null if cannot convert
            //Stores = (List<Store.) xml.Deserialize(file); //exception if cannot convert
            
        }
    }
}
