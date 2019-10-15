using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Assignment_2
{

    class Airline
    {
        static Random rnd = new Random();
        public static event priceCutEvent priceCut;

        private static Int32 priceCutCount = 0;
        private static Int32 loc = 0;
        private static Int32 airlinePrice = 50;

        public Int32 getPrice() { return airlinePrice; }

        public static void changePrice(Int32 price)
        {
            if (loc == Program.travelAgents.Length)
                loc = 0;
            if(priceCut != null)
            {
                if(price < airlinePrice)
                {
                    priceCut(price, Program.travelAgents[loc].Name);
                    loc++;
                    priceCutCount++;
                }

                if (price != airlinePrice)
                    airlinePrice = price;
            }
        }

        private Int32 pricingModel()
        {
            Int32 price = rnd.Next(50, 200);
            return price;
        }

        public void airlineFunc()
        {
            while(priceCutCount < 10)
            {
                Thread.Sleep(rnd.Next(1000, 2000));
                Int32 price = pricingModel();
                changePrice(price);
            }
            Program.airlineThreadRunning = false;
        }
        public void runOrder() //event handler
        {
            string order = Program.mcb.getOneCell(); //retrieves the order from the MultiCellBuffer

            var output = order.Split(new[] { ':' }); //splits the string with the colons
            OrderClass orderObject = new OrderClass(output[0], Convert.ToInt32(output[1]), Convert.ToInt32(output[2]), Convert.ToInt32(output[3]));
            
            Thread thread = new Thread(() => OrderProcessing.processOrder(orderObject, getPrice()));
            thread.Start(); //starts the order processing thread
        }
    }
   
}
