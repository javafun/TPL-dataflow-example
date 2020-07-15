using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task producerTask = Task.Run(() => Producer());

            Task consumerTask = Task.Run(async () => await ConsumerAsync());

            Task.WaitAll(producerTask, consumerTask);
        }


        private static BufferBlock<string> _buffer = new BufferBlock<string>();


        public static void Producer()
        {
            bool exit = false;           
            while (!exit)
            {
                string input = Console.ReadLine();

                if(string.Compare(input,"exit",ignoreCase:true) == 0)
                {
                    exit = true;
                }
                else
                {
                    _buffer.Post(input);
                }
            }
        }

        public static async Task ConsumerAsync()
        {
            while (true)
            {
                Console.Write("Enter something here: ");
                string data = await _buffer.ReceiveAsync();

                Console.WriteLine($"User input: {data}");
            }
        }
    }
}
