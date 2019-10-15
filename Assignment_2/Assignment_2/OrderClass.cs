using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    class OrderClass
    {
        private string senderId;
        private Int32 cardNo;
        private Int32 amount;
        private Int32 unit_price;

        public OrderClass(string senderId, Int32 cardNo, Int32 amount, Int32 unit_price)
        {
            this.senderId = senderId;
            this.cardNo = cardNo;
            this.amount = amount;
            this.unit_price = unit_price;
        }

        public string getSenderId()
        {
            return senderId;
        }

        public Int32 getCardNo()
        {
            return cardNo;
        }

        public Int32 getAmount()
        {
            return amount;
        }

        public Int32 getUnit_price()
        {
            return unit_price;
        }
    }
}
