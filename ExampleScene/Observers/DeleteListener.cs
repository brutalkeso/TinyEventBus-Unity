using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteListener : MonoBehaviour {

    void Start()
    {
        TinyEventBus.sharedBus().addObserver(this, StringFile.delete, "deleteEventRecieved");
    }

    public void deleteEventRecieved(Dictionary<string, object> data)
    {
        Destroy(this.gameObject);
        TinyEventBus.sharedBus().removeObserverForKey(StringFile.delete, this);
    }

}
