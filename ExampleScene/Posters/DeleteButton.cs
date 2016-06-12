using UnityEngine;
using System.Collections;

public class DeleteButton : MonoBehaviour {

	public void delete() {
        TinyEventBus.sharedBus().postEventForKey(StringFile.delete);
    }

}
