using System;
using ShippingcostCalculation.Products;

namespace ShippingcostCalculation
{
    public class Program
    {
        #region Functional Fields

        // Initializing Variables as Zero (for Functional-Part)
        private static decimal _netPrice = 0;
        private static int _totalWeight = 0;

        private static int _numberOfCardboards = 0;
        private static int _numberTransportKilometers = 0;

        private static int _calculatedWeight = 0;
        private static decimal _calculatedDiscount = 0;
        private static decimal _calculatedShipmentCost = 0;

        // Initializing set Variables by Design
        private static readonly decimal _netSalePrice = 7.85m; // Net price per Unit in Euros
        private static readonly int _itemsPerCardboard = 12; // Items per Cardboard Package
        private static readonly int _weightPerCardboard = 24; // Weight per Cardboard in Kilograms

        #endregion

        public static void Main(string[] args)
        {
#if DEBUG
            // OOP
            NewCalculation();
#else
            // Functional
            GetUserInput();
            CalculateWeight();
            CalculateShipment(_numberTransportKilometers, _calculatedWeight);
            CalculateNetPrice();
            CalculateDiscount();
            FinalOutput();
            Console.WriteLine("Drücke beliebige Taste zum beenden...");
            Console.ReadKey();
#endif
        }

        #region OOP-Part

        private static void NewCalculation()
        {
            string boxAmountString;
            string transportDistanceString;

            int boxAmount;
            int transportDistance;

            do
            {
                Console.Write("{0,-44}", "Anzahl der Kartons: ");
                boxAmountString = Console.ReadLine();
            } while (!Int32.TryParse(boxAmountString, out boxAmount));

            do
            {
                Console.Write("{0,-44}", "Kilometer: ");
                transportDistanceString = Console.ReadLine();
            } while (!Int32.TryParse(transportDistanceString, out transportDistance));

            IProduct product = Banana.Instance;
            product.Amount = boxAmount;
            product.TransportDistance = transportDistance;

            Program.OutOop(product);

            Console.Write("Neue Kalkulation? j / N ");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            

            if (keyPressed.KeyChar.ToString() == "j")
            {
                Console.WriteLine();
                NewCalculation();
            }
        }

        private static void OutOop(IProduct product)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("{0,-36} {1,13}", "Gewicht: ", product.TotalWeight + " kg");
            Console.WriteLine("{0,-36} {1,13}", "berechnetes Gewicht: ", product.InvoiceWeight + " kg");
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Fracht: ", product.TransportCost);
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Nettoumsatz (Umsatz ohne Fracht): ", product.ProductPrice);
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Gesamtumsatz (Umsatz mit Fracht): ", product.TotalPrice);
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Rabatt: ", product.Discount);
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Zielpreis: ", product.TotalDiscountPrice);
        }

        #endregion OOP-Part

        #region Functional-Part

        private static void GetUserInput()
        {
            string cardboardCountUserInput;
            string transportKilometersUserInput;

            do
            {
                Console.Write("{0,-44}", "Anzahl der Kartons: ");
                cardboardCountUserInput = Console.ReadLine();
            } while (!Int32.TryParse(cardboardCountUserInput, out Program._numberOfCardboards) ||
                     _numberOfCardboards <= 0);

            do
            {
                Console.Write("{0,-44}", "Kilometer: ");
                transportKilometersUserInput = Console.ReadLine();
            } while (!Int32.TryParse(transportKilometersUserInput, out Program._numberTransportKilometers) ||
                     _numberTransportKilometers <= 0);
        }

        private static void CalculateWeight()
        {
            Program._totalWeight = Program._numberOfCardboards * Program._weightPerCardboard;
            Program._calculatedWeight = ((Program._totalWeight + 99) / 100) * 100;
        }

        private static void CalculateShipment(int kilometers, int calculatedWeight)
        {
            Program._calculatedShipmentCost = kilometers * calculatedWeight * 0.06m / 100;
        }

        // Calculating Net Price
        private static void CalculateNetPrice()
        {
            Program._netPrice = Program._numberOfCardboards * Program._netSalePrice * Program._itemsPerCardboard;
        }

        // Calculating Discount
        private static void CalculateDiscount()
        {
            decimal netPrice = Program._netPrice;

            if (netPrice <= 10000)
            {
                Program._calculatedDiscount = netPrice * 0.03m;
            }
            else if (netPrice > 10000 && netPrice <= 50000)
            {
                Program._calculatedDiscount = netPrice * 0.05m;
            }
            else if (netPrice > 50000)
            {
                Program._calculatedDiscount = netPrice * 0.07m;
            }
        }

        private static void FinalOutput()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("{0,-36} {1,13}", "Gewicht: ", Program._totalWeight + " kg");
            Console.WriteLine("{0,-36} {1,13}", "berechnetes Gewicht: ", Program._calculatedWeight + " kg");
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Fracht: ", Program._calculatedShipmentCost);
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Nettoumsatz (Umsatz ohne Fracht): ", Program._netPrice);
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Gesamtumsatz (Umsatz mit Fracht): ",
                Program._netPrice + Program._calculatedShipmentCost);
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Rabatt: ", Program._calculatedDiscount);
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("{0,-36} {1,10:N2} EUR", "Zielpreis: ",
                (Program._netPrice + Program._calculatedShipmentCost) - Program._calculatedDiscount);
        }

        #endregion Functional-Part
    }
}