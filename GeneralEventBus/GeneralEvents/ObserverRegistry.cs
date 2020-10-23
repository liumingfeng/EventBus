using GeneralEventBus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GeneralEventBus
{

    public class ObserverRegistry
    {
        private ConcurrentDictionary<Type, List<ObserverAction>> registry = new ConcurrentDictionary<Type, List<ObserverAction>>();

        public void Register(object observer)
        {
            Dictionary<Type, List<ObserverAction>> observerActions = FindAllObserverActions(observer);
            foreach (var item in observerActions)
            {
                List<ObserverAction> actions = item.Value;
                List<ObserverAction> registeredEventActions = registry.GetOrAdd(item.Key, key => new List<ObserverAction>());
                registeredEventActions.AddRange(actions);
            }
        }

        public List<ObserverAction> GetMatchedObserverActions(Type postedEventInfo)
        {
            List<ObserverAction> matchedObservers = new List<ObserverAction>();
            foreach (var entry in registry)
            {
                Type eventInfo = entry.Key;
                List<ObserverAction> eventActions = entry.Value;
                if (postedEventInfo.IsAssignableFrom(eventInfo))
                {
                    matchedObservers.AddRange(eventActions);
                }
            }
            return matchedObservers;
        }

        private Dictionary<Type, List<ObserverAction>> FindAllObserverActions(object observer)
        {
            Dictionary<Type, List<ObserverAction>> observerActions = new Dictionary<Type, List<ObserverAction>>();
            Type type = observer.GetType();

            foreach (MethodInfo method in GetAnnotatedMethods(type))
            {
                var parameters = method.GetParameters();
                Type eventType = parameters[0].ParameterType;
                if (!observerActions.ContainsKey(eventType))
                {
                    observerActions.Add(eventType, new List<ObserverAction>());
                }
                observerActions.GetValueOrDefault(eventType)?.Add(new ObserverAction(observer, method));
            }
            return observerActions;
        }

        private List<MethodInfo> GetAnnotatedMethods(Type type)
        {
            List<MethodInfo> annotatedMethods = new List<MethodInfo>();
            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.GetCustomAttribute<SubscribeAttribute>() != null)
                {
                    var parameters = method.GetParameters();
                    if (parameters.Length != 1)
                    {
                        // 检查sub方法参数的长度，必须为1，不然主动报错。
                        throw new Exception();
                    }
                    annotatedMethods.Add(method);
                }
            }
            return annotatedMethods;
        }
    }
}