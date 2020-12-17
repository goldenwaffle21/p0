namespace PizzaWorld.Domain.Singletons
{
    internal class ClientSingleton
    {
        private static ClientSingleton _instance;

        public static ClientSingleton Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance == new ClientSingleton;
                }
                return _instance;
            }
        }
        
        public List<Store> Stores {get;set;}
        
        private ClientSingleton()
        {
            Stores = new List<Store>();
        }
        
        public void GetAllStores()
        {

        }

        public void MakeAStore()
        {
            var s = new Store();
            Stores.add(s);
            Save();
        }

        private void Save()
        {
            string path = @"./pizzaworld.xml"
            var file = new StreamWriter(path);
            var xml = new XmlSerializer(typof(List<Store>));
            xml.Serialize(file,Stores);
        }
    }
}
