using System;
using System.Linq;

namespace PatternRefactorings.FormTemplateMethod.Bad
{
    public abstract class OrderShipment
    {
        public string ShippingAddress { get; set; }
    }

    public class UpsOrderShipment : OrderShipment
    {
        public void Ship()
        {
            // confirm shipping data
            if (String.IsNullOrWhiteSpace(ShippingAddress))
            {
                throw new ApplicationException("Invalid shipping address");
            }

            // get shipping label / id from UPS
            string shipmentNumber = UpsService.GetShipmentNumber();

            // print label
            PrinterService.PrintLabel(shipmentNumber);
        }
    }

    public class FedExOrderShipment : OrderShipment
    {
        public void Ship()
        {
            // confirm shipping data
            if (String.IsNullOrWhiteSpace(ShippingAddress) || ShippingAddress.Contains("PO Box"))
            {
                throw new ApplicationException("Invalid shipping address");
            }

            // get shipping label / id from FedEx
            string shipmentNumber = FedExService.GetFedExNumber();

            // print label
            PrinterService.PrintLabel(shipmentNumber);
        }
    }

    public class Client
    {
        public void Main(string shippingChoice)
        {
            if (shippingChoice == "ups")
            {
                var upsShipment = new UpsOrderShipment();
                upsShipment.Ship();
            }
            if (shippingChoice == "fedex")
            {
                var fedExShipment = new FedExOrderShipment();
                fedExShipment.Ship();
            }
        }
    }
}
