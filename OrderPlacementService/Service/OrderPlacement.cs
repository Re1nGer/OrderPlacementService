using OrderPlacementService.Interfaces;
using OrderPlacementService.Models;

namespace OrderPlacementService.Service
{
    public class OrderPlacement : IOrderPlacement
    {
        //since no db setup is requried, we can store those in List
        private List<Order> Orders = new();

        public OrderPlacement(List<Order> orders)
        {
            Orders = orders;
        }

        public List<Order> GetCustomerOrders()
        {
            return Orders;
        }

        //in the future, depending on requirements PlaceOrder method can be ascribed a new behaviour
        //with different validation rules by creating new service extending IOrderPlacement and providing
        //new Place Order method, so service is open for extension but closed for modification
        public void PlaceOrder(int customerId, int desiredAmount, DateTime deliveryDate, DNAKit kit)
        {
            bool deliveryInTheFuture = deliveryDate > DateTime.Now;

            //since desired amout is integer, there is no way to pass in decimal number, which covers third validation criteria
            bool desiredAmountInvalid = desiredAmount <= 0 || desiredAmount > 999; // || desiredAmount % 1 != 0;

            if (!deliveryInTheFuture)
            {
                throw new ArgumentException("Delivery date must be in the future");
            }

            if (desiredAmountInvalid)
            {
                throw new ArgumentException("Desired amount must be a positive round number less than or equal to 999");
            }

            double basePrice = kit.BasePrice;
            double discount = CalculateDiscount(desiredAmount);
            double totalPrice = desiredAmount * basePrice * (1 - discount);

            Order order = new(customerId, desiredAmount, totalPrice, deliveryDate, kit);
            Orders.Add(order);
        }

        //in the future, depending on requirements CalculateDiscount method can be ascribed a new behaviour
        //by creating new service extending IOrderPlacement and providing new CalculateDiscount method
        //so service is open for extension but closed for modification
        public double CalculateDiscount(int desiredAmount)
        {
            if (desiredAmount >= 50)
            {
                return 0.15;
            }
            else if (desiredAmount >= 10)
            {
                return 0.05;
            }
            else
            {
                return 0;
            }
        }
    }
}
