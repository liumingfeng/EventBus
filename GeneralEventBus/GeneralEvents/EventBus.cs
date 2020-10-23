using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralEventBus
{
    public class EventBus
    {
        private ObserverRegistry registry = new ObserverRegistry();

        public void Register(object observer)
        {
            registry.Register(observer);
        }

        public void Post(object @eventArg) 
        {
            List<ObserverAction> observerActions = registry.GetMatchedObserverActions(@eventArg.GetType());
            foreach (ObserverAction observerAction in observerActions) 
            {
                observerAction.Execute(@eventArg);
            }
        }
    }
}
