namespace OrderPlacementService.Models
{
    public class Order
    {
        public Guid Id { get; set; }    
        public int CustomerId { get; }
        public int DesiredAmount { get; }
        public double TotalPrice { get; }
        public DateTime DeliveryDate { get; }
        public DNAKit Kit { get; }

        public Order(int customerId, int desiredAmount, double totalPrice, DateTime deliveryDate, DNAKit kit)
        {
            CustomerId = customerId;
            DesiredAmount = desiredAmount;
            TotalPrice = totalPrice;
            DeliveryDate = deliveryDate;
            Kit = kit;
        }
    }
}
