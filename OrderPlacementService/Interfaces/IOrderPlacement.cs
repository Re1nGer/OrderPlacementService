using OrderPlacementService.Models;

namespace OrderPlacementService.Interfaces
{
    public interface IOrderPlacement
    {
        //we may substitute DNAKit class to interface in the future in case there is specific logic relevant to DNA kits that might exibit different behaviour
        //depending on its type or other properties, but for now since only price changes, one concrete class would suffice
        void PlaceOrder(int customerId, int desiredAmount, DateTime deliveryDate, DNAKit kit);
        List<Order> GetCustomerOrders();
        double CalculateDiscount(int desiredAmount);
    }
}
