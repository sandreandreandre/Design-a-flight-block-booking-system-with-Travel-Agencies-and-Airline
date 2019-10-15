using System;

using System;
using System.Threading;


namespace Assignment_2
{
    public delegate void priceCutEvent(Int32 pr, string senderId);
    public delegate void orderProcessedEvent(string senderID, Int32 amountToCharge, Int32 price, Int32 amount);
    public delegate void orderCreateEvent();

    class Program
    {
        public static bool airlineThreadRunning = true;
        public static MultiCellBuffer mcb;
        public static Thread[] travelAgents;

        static void Main(string[] args)
        {
            Airline airline = new Airline();
            travelAgnet TravelAgnets = new travelAgnet();

            mcb = new MultiCellBuffer(3);

            Thread airlinefunc = new Thread(new ThreadStart(airline.airlineFunc));

            airlinefunc.Start();

            Airline.priceCut += new priceCutEvent(TravelAgnets.ticketOnSale);
            OrderProcessing.orderProcessed += new orderProcessedEvent(TravelAgnets.orderProcessed);
            travelAgnet.orderCreated += new orderCreateEvent(airline.runOrder);

            travelAgents = new Thread[5];

            for (int i = 0; i < 5; i++) // N = 5 here
            { //Start N retailer threads
                travelAgents[i] = new Thread(new ThreadStart(TravelAgnets.travelAgentFunc)); //starts thread with retailer function
                travelAgents[i].Name = (i + 1).ToString();
                travelAgents[i].Start();
            }
        }
    }
}
