using System;
using System.Reflection;

namespace Task_2
{
    public class ReflectionUtility
    {
        public void CallPrivate(object targetObject, string methodName, object[] args)
        {
            
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var obj = targetObject.GetType();
            foreach (var type in types)
            {
                if (type.Name== obj.Name)
                {
                    var methods=type.GetMethod(methodName, 
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    methods.Invoke(targetObject, args);
                }
            }

        }
    }
}