using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Assignment_2
{
    class travelAgnet
    {
        public static Random rnd = new Random();
        public static event orderCreateEvent orderCreated;
        public void travelAgentFunc() // for starting a thread
        {
            while (Program.airlineThreadRunning) //thread will run until the chicken thread has ended
            {
                Thread.Sleep(rnd.Next(1500, 3000));
                createOrder(Thread.CurrentThread.Name);
            }
        }

        private void createOrder(string senderID)
        {
            Int32 cardNo = rnd.Next(5000, 6999);
            Int32 amount = rnd.Next(10, 50);
            Int32 unit_price = rnd.Next(50, 200);
            OrderClass orderObject = new OrderClass(senderID, cardNo, amount, unit_price);  

            string data = orderObject.getSenderId() + ":" + orderObject.getCardNo().ToString()     //encode a data into a string
                           + ":" + orderObject.getAmount().ToString() + ":" + orderObject.getUnit_price().ToString();

            //Console.WriteLine("TravelAgent {0}'s order has been created at {1}.", senderID, DateTime.Now.ToString("hh:mm:ss"));
            Program.mcb.setOneCell(data);
            orderCreated();
        }
        public void orderProcessed(string senderID, Int32 amountToCharge, Int32 price, Int32 amount) // Event handler when the order is processed
        {
            Console.WriteLine("travelAgent{0}'s order has been processed. The amount to be charged is $" + amountToCharge + " ($" + price + " per ticket for " + amount + " tickets).", senderID, Thread.CurrentThread.Name);
        }

        public void ticketOnSale(Int32 p, string senderID) // Event handler 
        {
            // order chickens from chicken farm - send order into queue    
            Console.WriteLine("Tickets are on sale. TravelAgent {0} decide to create an order.", senderID);
            createOrder(senderID);
        }
    }
}
