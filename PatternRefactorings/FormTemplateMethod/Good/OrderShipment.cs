using System;
using System.Linq;

namespace PatternRefactorings.FormTemplateMethod.Good
{
    public abstract class OrderShipment
    {
        public string ShippingAddress { get; set; }
        public void Ship()
        {
            ConfirmShippingAddress();

            string label = GetShippingLabel();

            PrintLabel(label);
        }

        protected void PrintLabel(string label)
        {
            PrinterService.PrintLabel(label);
        }
        protected abstract void ConfirmShippingAddress();
        protected abstract string GetShippingLabel();
    }

    public class UpsOrderShipment : OrderShipment
    {
        protected override void ConfirmShippingAddress()
        {
            if (String.IsNullOrWhiteSpace(ShippingAddress))
            {
                throw new ApplicationException("Invalid shipping address");
            }
        }

        protected override string GetShippingLabel()
        {
            return UpsService.GetShipmentNumber();
        }
    }

    public class FedExOrderShipment : OrderShipment
    {

        protected override void ConfirmShippingAddress()
        {
            if (String.IsNullOrWhiteSpace(ShippingAddress) || ShippingAddress.Contains("PO Box"))
            {
                throw new ApplicationException("Invalid shipping address");
            }
        }

        protected override string GetShippingLabel()
        {
            return FedExService.GetFedExNumber();
        }
    }

    public class Client
    {
        public void Main(string shippingChoice)
        {
            var shipment = GetShipment(shippingChoice);
            shipment.Ship();
        }

        private OrderShipment GetShipment(string shippingChoice)
        {
            if (shippingChoice == "ups") return new UpsOrderShipment();
            if (shippingChoice == "fedex") return new FedExOrderShipment();
            throw new NotImplementedException();
        }
    }
}
