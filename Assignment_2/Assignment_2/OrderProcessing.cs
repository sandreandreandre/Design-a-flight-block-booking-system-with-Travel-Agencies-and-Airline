using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    class OrderProcessing
    {
        public static event orderProcessedEvent orderProcessed;

        public static void processOrder(OrderClass orderObject, Int32 unitPrice)
        {
            if (!checkValidity(orderObject.getCardNo()))
            {
                Console.WriteLine("{0} is invalid credit card number.", orderObject.getCardNo());
                return;

            }
            else
            {
                Int32 amountToPay = Convert.ToInt32( (unitPrice * orderObject.getAmount())); // unitPrice * amount + tax (8%)  
                orderProcessed(orderObject.getSenderId(), amountToPay, unitPrice, orderObject.getAmount()); // emits event to subscribers
            }
        }

        private static Boolean checkValidity(Int32 cardNo)
        {
            if (cardNo <= 6999 && cardNo >= 5000)
                return true;
            else
                return false;
        }
    }
}
