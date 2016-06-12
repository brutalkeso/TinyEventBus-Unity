using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface EventBus
{

    void addObserver(object observer, string forKey, string method);

    void postEventForKey(string key, Dictionary<string, object> data = null);

    void removeObserver(object observer);

    void removeObserverForKey(string key, object observer);

}