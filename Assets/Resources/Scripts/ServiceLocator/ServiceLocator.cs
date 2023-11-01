using System;
using System.Collections.Generic;

public class ServiceLocator<T> : IServiceLocator<T>
{
    protected Dictionary<Type, T> _services { get;}

    public ServiceLocator()
    {
        _services = new Dictionary<Type, T>();
    }

    public TP Get<TP>() where TP : T
    {
        var type = typeof(TP);

        if(!_services.ContainsKey(type))
        {
            throw new Exception($"There is object of type {type} in this Service Locator");
        }
        
        return (TP)_services[type];
    }

    public TP Register<TP>(TP newService) where TP : T
    {
        var type = newService.GetType();

        if(_services.ContainsKey(type))
        {
            throw new Exception($"You can't register this ({type}) service. This service is already registered.");
        }

        _services[type] = newService;

        return newService;
    }

    public void Unregister<TP>(TP newService) where TP : T
    {
        var type = newService.GetType();

        if( _services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }
}
