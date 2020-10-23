using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralEventBus
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeAttribute : Attribute
    {
    }
}
