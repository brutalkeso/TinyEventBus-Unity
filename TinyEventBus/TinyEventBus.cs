using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class TinyEventBus : EventBus {

    private static TinyEventBus _sharedBus;

    public static EventBus sharedBus()
    {
        TinyEventBus._sharedBus = TinyEventBus._sharedBus != null ? TinyEventBus._sharedBus : new TinyEventBus();
        return TinyEventBus._sharedBus;
    }

    private Dictionary<string, List<ObserverObject>> events;
    private class ObserverObject {
        public object observer;
        public string methodString;
        public bool markedForDestruction = false;
    }

    public TinyEventBus() {
        this.events = new Dictionary<string, List<ObserverObject>>();
    }

    /**
	 * observer: Object interested in event
	 * forKey: Event key
	 * method: Method name that should be executed on Listener when event is picked up
	 */
    public void addObserver(object observer, string forKey, string method)
    {

        //retrieve list
        List<ObserverObject> currentList;
        this.events.TryGetValue(forKey, out currentList);
        currentList = currentList != null ? currentList : new List<ObserverObject>();

        //add observer
        ObserverObject observerObject = new ObserverObject();
        observerObject.observer = observer;
        observerObject.methodString = method;
        currentList.Add(observerObject);
        this.events.Remove(forKey);
        this.events.Add(forKey, currentList);
    }

    public void postEventForKey(string key, Dictionary<string, object> data) {
        List<ObserverObject> currentList;
        this.events.TryGetValue(key, out currentList);

        if (currentList != null && currentList.Count > 0) {

            foreach (ObserverObject obj in currentList.ToArray()) {
                this.postNotification(obj, data);
            }
        } else {
            Debug.Log("ERROR: tried to post to key: " + key + ", but no observers were registered");
        }
    }

    public void removeObserver(object observer) {

        foreach (var keyValuePair in this.events) {
            List<ObserverObject> observerList = keyValuePair.Value;

            for (int i = 0; i < observerList.Count; i++) {
                object obj = observerList[i].observer;

                if (obj == observer) {
                    observerList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public void removeObserverForKey(string key, object observer) {
        List<ObserverObject> observerList; 
        this.events.TryGetValue(key, out observerList);

        if (observerList != null) {

            foreach (ObserverObject obj in observerList) {

                if (obj.observer == observer) {
                    observerList.Remove(obj);
                    break;
                }
            }
        }
    }

    //PRIVATE

    private bool postNotification(ObserverObject obj, Dictionary<string, object> data) {
        Type type = obj.observer.GetType();
        MethodInfo method = type.GetMethod(obj.methodString);
        object[] signature = new object[1];
        signature[0] = data;

        try {
            method.Invoke(obj.observer, signature);
        } catch (TargetParameterCountException e) {
            Debug.Log("ERROR: Wrong signature on method " + obj.methodString + ". Method should have signature: functionName(Dictionary<string, object> data) ");
            Debug.Log("Stacktrace: ");
            Debug.Log(e);
        } catch (TargetInvocationException e) {
            if (e.InnerException is MissingReferenceException) {
                Debug.Log("ERROR: Calling method " + obj.methodString + ". Tried to post to a null observer, did you forget to remove observer when destroying an object?");
            }
            Debug.Log("Stacktrace: ");
            Debug.Log(e);
        } catch (Exception e) {
            Debug.Log("went here");
            Debug.Log(e);
        }
        return true;
    }

}