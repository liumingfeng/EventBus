using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GeneralEventBus
{
    public class ObserverAction
    {
        private object _target;
        private MethodInfo _method;
        public ObserverAction(object target, MethodInfo method)
        {
            this._target = target;
            this._method = method;
        }
        public void Execute(object eventArg)
        {
            try
            {
                _method.Invoke(_target, new[] { eventArg });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}       