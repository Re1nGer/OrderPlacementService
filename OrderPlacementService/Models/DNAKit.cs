namespace OrderPlacementService.Models
{
    public class DNAKit
    {
        public Guid Id { get; }
        //base price for standard DNA kit
        public double BasePrice { get; } = 98.99;
        public DNAKit(Guid id, double basePrice)
        {
            Id = id;
            BasePrice = basePrice;
        }
    }
}