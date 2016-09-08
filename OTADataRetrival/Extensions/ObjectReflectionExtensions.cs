using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication1.Extensions
{
    public static class ObjectReflectionExtensions
    {
        public static object GetPropertyValue(this object instance, string propertyName)
        {
            return instance.GetType().InvokeMember(propertyName, BindingFlags.GetProperty,
                null, instance, new object[] { });
        }



        public static void SetPropertyValue(this object instance, string propertyName, object propertySetValue)
        {
            instance.GetType().InvokeMember(propertyName, BindingFlags.SetProperty,
                null, instance, new object[] { Convert.ChangeType(propertySetValue, propertySetValue.GetType()) });
        }


        public static List<string> GetPropertyNames(this object instance)
        {
            var propertyNames = instance.GetType().GetProperties().Select(x => x.Name).ToList();
            return propertyNames;
        }






        public static object RunStaticMethod(this Type T, string strMethod,
 object[] aobjParams)
        {
            BindingFlags eFlags =
             BindingFlags.Static | BindingFlags.Public |
             BindingFlags.NonPublic;
            return RunMethod(T, strMethod,
             null, aobjParams, eFlags);
        }

        public static object RunInstanceMethod(this object objInstance, Type T, string strMethod,
 object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public |
             BindingFlags.NonPublic;
            return RunMethod(T, strMethod,
             objInstance, aobjParams, eFlags);
        }

        private static object RunMethod(Type T, string
 strMethod, object objInstance, object[] aobjParams, BindingFlags eFlags)
        {
            try
            {
                var m = T.GetMethod(strMethod, eFlags);
                if (m == null)
                {
                    throw new ArgumentException("There is no method '" +
                     strMethod + "' for type '" + T + "'.");
                }

                object objRet = m.Invoke(objInstance, aobjParams);
                return objRet;
            }
            catch
            {
                throw;
            }
        }
    }
}


