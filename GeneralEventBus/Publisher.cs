using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralEventBus
{
    public class Publisher
    {
        private EventBus _eventBus;
        public Publisher(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Register(new Sub1());
            _eventBus.Register(new Sub2());
        }

        public void Trigger1()
        {
            SubEventArg eventArg = new SubEventArg() 
            {
                EventTime = DateTime.Now,
                Sender = this
            };
            _eventBus.Post(eventArg);
        }
    }
}
