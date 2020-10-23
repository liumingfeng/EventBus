using System;

namespace GeneralEventBus
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher(new EventBus());
            publisher.Trigger1();
        }
    }
}
