using System;

namespace ShippingcostCalculation.Products
{
    public class Banana : IProduct
    {
        private static Banana _instance;
        private static readonly object _padlock = new object();
        
        public int Amount { get; set; }
        public int TransportDistance { get; set; }
        
        public decimal TotalWeight => this.Amount * this.BoxWeight;
        public decimal InvoiceWeight
        {
            get
            {
                decimal totalWeight = this.TotalWeight;
                decimal modulo = totalWeight % 100;

                if (modulo > 0)
                {
                    return totalWeight + (100 - modulo);
                }
                else
                {
                    return totalWeight;
                }
            }
        }

        public decimal TransportCost => this.TransportDistance * (this.InvoiceWeight / 100) * this.BoxTransportCost;
        public decimal ProductPrice => this.ItemsPerBox * this.Price * this.Amount;
        public decimal TotalPrice => this.ProductPrice + this.TransportCost;

        public decimal DiscountPercentage
        {
            get
            {
                decimal price = this.ProductPrice;
                
                if (price <= 10000m)
                    return 3m;
                else if (price > 10000m && price <= 50000m)
                    return 5m;
                else
                    return 7m;
            }
        }

        public decimal Discount => this.ProductPrice * (this.DiscountPercentage / 100);
        public decimal TotalDiscountPrice => this.TotalPrice - this.Discount;
        
        public decimal Price => 7.85m;
        public int ItemsPerBox => 12;
        public decimal BoxWeight => 24m;
        public decimal BoxTransportCost => 0.06m;

        private Banana()
        {
            
        }
        
        public static Banana Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Banana();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
