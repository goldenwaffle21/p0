namespace PizzaWorld.Domain.Models
{
    public class Store
    {
        public List<Order> Orders {get;set;}
        //CRUD methods for orders
        void CreateOrder()
        {
            Orders.add(new Order());

        }
        Order GetOrder()
        {
            //either find it by the index, or search for the index by some other trait.
        }
        try
        {
            void ChangeOrder(int i,Order o)
            {
                Orders(i) = o;
                return true;
            }
        }
        catch 
        {
            return false;
        }
        finally
        {
            GC.Collect();
        }
        try
        {
            void CancelOrder(int i)
            {
                Orders.remove(GetOrder(i));
                return true;
            }
        }
        catch
        {
            return false
        }
        finally
        {
            GC.Collect();
        }
    }
}