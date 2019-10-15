using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

namespace Assignment_2
{
    class MultiCellBuffer
    {
        public string[] bufferString;
        private const int N = 5;
        private int n; //number of bufferCells
        private int eleCount;
        private static Semaphore write_pool; //pool of writing resources
        private static Semaphore read_pool; //pool of reading resources

        public MultiCellBuffer(int n) //constructor 
        {
            lock (this) //ensures that there are no interruptions
            {
                eleCount = 0; //initialize elementCount

                if (n <= N)
                {
                    this.n = n;
                    write_pool = new Semaphore(n, n);
                    read_pool = new Semaphore(n, n);
                    bufferString = new string[n]; //create 'n' bufferString cells

                    for (int i = 0; i < n; i++)
                    {
                        bufferString[i] = "0"; //initialize all bufferString cells with "0"
                    }
                }
                else
                    Console.WriteLine("'n' value for number of buffer cells needs to be less than {0}.", N);
            }
        }

        public void setOneCell(string data)
        {
            write_pool.WaitOne();

            lock (this)
            {
                while (eleCount == n) //thread waits if all bufferCells are full
                {
                    Monitor.Wait(this);
                }

                for (int i = 0; i < n; i++)
                {
                    if (bufferString[i] == "0") //makes sure there is no data being over-written 
                    {
                        bufferString[i] = data;
                        eleCount++;
                        i = n; //exits loop
                    }
                }
                write_pool.Release();
                Monitor.Pulse(this);
            }
        }

        public string getOneCell()
        {
            read_pool.WaitOne();
            string output = "";

            lock (this)
            {
                while (eleCount == 0) //thread waits if no cells are full
                {
                    Monitor.Wait(this);
                }

                for (int i = 0; i < n; i++)
                {
                    if (bufferString[i] != "0") //makes sure there is valid data
                    {
                        output = bufferString[i];
                        bufferString[i] = "0";
                        eleCount--;
                        i = n; //exits loop
                    }
                }
                read_pool.Release();
                Monitor.Pulse(this);
            }
            return output;
        }
    }
}
