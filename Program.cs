using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Call_Center
{
    public class IncomingCall
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CallTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Consultant { get; set; }
    }
    public class CallCenter
    {
        private int _counter = 0;
        public Queue<IncomingCall> Calls { get; private set; }
        public CallCenter()
        { Calls = new Queue<IncomingCall>(); }
        public void call(int clientId)
        {
            IncomingCall call = new IncomingCall()
            {
                Id = ++_counter,
                ClientId = clientId,
                CallTime = DateTime.Now
            };
            Calls.Enqueue(call);
        }
        public IncomingCall answer(string consultant)
        {
            if (Calls.Count > 0)
            {
                IncomingCall call = Calls.Dequeue();
                call.Consultant = consultant;
                call.StartTime = DateTime.Now;
                return call;
            }
            return null;
        }
        public void end(IncomingCall call)
        { call.EndTime = DateTime.Now; }
        public bool areWaitingCalls()
        { return Calls.Count > 0; }
        public void testCallCenter()
        {
            Random random = new Random();
            CallCenter center = new CallCenter();
            center.call(1111);
            center.call(2222);
            center.call(3333);
            center.call(4444);
            center.call(5555);
            while (center.areWaitingCalls())
            {
                IncomingCall call = center.answer("Lojain");
                Console.Write(DateTime.Now.ToString("HH:mm:ss : "));
                Console.WriteLine("Call #{0} from {1} is answered by {2}.", call.Id, call.ClientId, call.Consultant);
                Thread.Sleep(random.Next(1000, 10000));
                center.end(call);
                Console.Write(DateTime.Now.ToString("HH:mm:ss : "));
                Console.WriteLine("Call #{0} from {1} is ended by {2}.",
                call.Id, call.ClientId, call.Consultant);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CallCenter call = new CallCenter();
            call.testCallCenter();
        }
    }
}
