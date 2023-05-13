using System;
using System.Collections.Generic;

namespace Services.Locator
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _services;

        public ServiceLocator()
        {
            _services = new();
        }

        public void RegisterService<T>(T service)
        {
            Type serviceType = typeof(T);

            _services.TryAdd(serviceType, service);
        }

        public T GetService<T>()
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            else
            {
                throw new Exception("Service: " + typeof(T).Name + " is not found!");
            }
        }
    }
}