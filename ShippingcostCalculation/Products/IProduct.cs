namespace ShippingcostCalculation.Products
{
    public interface IProduct
    {
        int Amount { get; set; }
        int TransportDistance { get; set; }
        
        decimal TotalWeight { get; }
        decimal InvoiceWeight { get; }
        
        decimal TransportCost { get; }
        decimal ProductPrice { get; }
        decimal TotalPrice { get; }
        decimal Discount { get; }
        decimal TotalDiscountPrice { get; }
    }
}