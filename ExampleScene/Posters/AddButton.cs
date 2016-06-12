using UnityEngine;
using System.Collections;

public class AddButton : MonoBehaviour {

    public void add() {
        TinyEventBus.sharedBus().postEventForKey(StringFile.add);
    }

}
