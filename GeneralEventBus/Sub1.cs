using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralEventBus
{
    public class SubEventArg
    {
        public DateTime EventTime { get; set; }
        public object Sender { get; set; }
    }

    public class Sub1
    {
        [Subscribe]
        public void DoSub1(SubEventArg eventArg)
        {
            Console.WriteLine("Do Sub1 handler.");
        }
    }

    public class Sub2
    {
        [Subscribe]
        public void DoSub2(SubEventArg eventArg)
        {
            Console.WriteLine("Do Sub2 handler.");
        }
    }
}
