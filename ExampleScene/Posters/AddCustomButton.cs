using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddCustomButton : MonoBehaviour {

    public int customAmount = 2;

	public void addCustomAmount() {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add(StringFile.addCustomDataKey, this.customAmount);
        TinyEventBus.sharedBus().postEventForKey(StringFile.addCustom, data);
    }

}
