using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddListener : MonoBehaviour {

    void Start () {
        TinyEventBus.sharedBus().addObserver(this, StringFile.add, "addEventRecieved");
        TinyEventBus.sharedBus().addObserver(this, StringFile.addCustom, "addCustomEventRecieved");
	}
	
    public void addEventRecieved(Dictionary<string, object> data) {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1.0f, this.gameObject.transform.position.z);
        cube.AddComponent<DeleteListener>();
        cube.AddComponent<Rigidbody>();
    }

    public void addCustomEventRecieved(Dictionary<string, object> data)
    {
        object numberOfItemsToAdd;
        data.TryGetValue(StringFile.addCustomDataKey, out numberOfItemsToAdd);

        for (int i = 0; i < (int)numberOfItemsToAdd; i++) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1.0f, this.gameObject.transform.position.z);
            cube.AddComponent<DeleteListener>();
            cube.AddComponent<Rigidbody>();
        }
    }

}
