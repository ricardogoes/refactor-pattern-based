using System;
using System.Linq;

namespace PatternRefactorings.FormTemplateMethod.Better
{
    public abstract class OrderShipment
    {
        public string ShippingAddress { get; set; }
    }

    public class UpsOrderShipment : OrderShipment
    {
        public void Ship()
        {
            ConfirmShippingAddress();

            string label = GetShippingLabel();

            PrintLabel(label);
        }

        private void ConfirmShippingAddress()
        {
            if (String.IsNullOrWhiteSpace(ShippingAddress))
            {
                throw new ApplicationException("Invalid shipping address");
            }
        }

        private string GetShippingLabel()
        {
            return UpsService.GetShipmentNumber();
        }

        private void PrintLabel(string label)
        {
            PrinterService.PrintLabel(label);
        }
    }

    public class FedExOrderShipment : OrderShipment
    {
        public void Ship()
        {
            ConfirmShippingAddress();

            string label = GetShippingLabel();

            PrintLabel(label);
        }

        private void ConfirmShippingAddress()
        {
            if (String.IsNullOrWhiteSpace(ShippingAddress) || ShippingAddress.Contains("PO Box"))
            {
                throw new ApplicationException("Invalid shipping address");
            }
        }

        private string GetShippingLabel()
        {
            return FedExService.GetFedExNumber();
        }

        private void PrintLabel(string label)
        {
            PrinterService.PrintLabel(label);
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
