using UnityEngine;
using System.Collections.Generic;

public class TinyEventBusTests : MonoBehaviour
{

    private EventBus eventBus;

    // Use this for initialization
    void Start()
    {

        //try to add observer
        Debug.Log("Test 1: Trying to add observer.");
        eventBus = new TinyEventBus();
        eventBus.addObserver(this, "testKey", "asd");
        Debug.Log("Test 1 (add observer) successfull");

        //try to post event
        Debug.Log("Test 2: Trying to post event to observer.");
        eventBus = new TinyEventBus();
        eventBus.addObserver(this, "testKey", "postMethod");
        eventBus.postEventForKey("testKey");
        Debug.Log("Test 2 end");

        //try to post event when no one is observing
        Debug.Log("Test3: Trying to post event which no one listens to, error message should show right after this message.");
        eventBus = new TinyEventBus();
        eventBus.postEventForKey("testKey");
        Debug.Log("Test 3 end");

        //removal of observer
        Debug.Log("Test4: Trying to add observer and then remove observer.");
        eventBus = new TinyEventBus();

        eventBus.addObserver(this, "testKey", "postMethod2");
        eventBus.postEventForKey("testKey");
        eventBus.removeObserver(this);

        Debug.Log("Observer removed, should now show error message");
        eventBus.postEventForKey("testKey");
        Debug.Log("Test 4 end");

        //removal of observer from multiple keys
        Debug.Log("Test 5: Trying to add observer to multiple keys and then remove observer.");
        eventBus = new TinyEventBus();

        eventBus.addObserver(this, "testKey", "postMethod3");
        eventBus.addObserver(this, "testKey2", "postMethod4");
        eventBus.addObserver(this, "testKey3", "postMethod5");
        Debug.Log("Posting three events");
        eventBus.postEventForKey("testKey");
        eventBus.postEventForKey("testKey2");
        eventBus.postEventForKey("testKey3");
        Debug.Log("Removing observer");
        eventBus.removeObserver(this);
        Debug.Log("Posting three events again, should result in three error messages");
        eventBus.postEventForKey("testKey");
        eventBus.postEventForKey("testKey2");
        eventBus.postEventForKey("testKey3");
        Debug.Log("Test 5: end");

        //test adding multiple objects to same key
        Debug.Log("Test 6: Trying to add multiple observers a key");
        eventBus = new TinyEventBus();
        Debug.Log("adding 3 observers");
        EventBusObserver firstObserver = new EventBusObserver("testKey", eventBus, 1);
        EventBusObserver secondObserver = new EventBusObserver("testKey", eventBus, 2);
        EventBusObserver thirdObserver = new EventBusObserver("testKey", eventBus, 3);
        Debug.Log("Posting event,  should result in three posts");
        eventBus.postEventForKey("testKey");
        Debug.Log("Test 6 end");

        //test adding multiple objects, removing one
        Debug.Log("Test 7: Trying to add multiple observers and removing one of them");
        eventBus = new TinyEventBus();
        Debug.Log("adding 3 observers");
        firstObserver = new EventBusObserver("testKey", eventBus, 1);
        secondObserver = new EventBusObserver("testKey", eventBus, 2);
        thirdObserver = new EventBusObserver("testKey", eventBus, 3);
        Debug.Log("Removing observer 1");
        eventBus.removeObserver(firstObserver);
        Debug.Log("Posting event, should result in post for observer 2 and 3");
        eventBus.postEventForKey("testKey");
        Debug.Log("Test 7 end");

        //test remove observer for one key, eventBus should retain key observing
        Debug.Log("Test 8: remove observer for specific key");
        eventBus = new TinyEventBus();
        Debug.Log("Adding observer for two keys");
        eventBus.addObserver(this, "testKey", "postMethod6");
        eventBus.addObserver(this, "testKey2", "postMethod7");
        Debug.Log("Removing observer for first keys");
        eventBus.removeObserverForKey("testKey", this);

        Debug.Log("Posting two keys, should result in error message one regular print");
        eventBus.postEventForKey("testKey");
        eventBus.postEventForKey("testKey2");
        Debug.Log("Test 8 end");

        //test remove observer for one key which has never been added, should not crash
        Debug.Log("Test 9: remove observer when observer has not been added");
        eventBus = new TinyEventBus();
        eventBus.removeObserver(this);
        Debug.Log("Test 9 success");

        //test remove observer for key for one key which has never been added, should not crash
        Debug.Log("Test 10: remove observer for key when observer has not been added");
        eventBus = new TinyEventBus();
        eventBus.removeObserverForKey("asd", this);
        Debug.Log("Test 10 success");

        //test posting method with variables
        Debug.Log("Test 11: Posting event with dictionary data");
        eventBus = new TinyEventBus();
        Debug.Log("Adding observer for one key");
        eventBus.addObserver(this, "testKey", "testMethodWithData");
        Debug.Log("Posting with dictionary");
        Dictionary<string, object> dict = new Dictionary<string, object>();
        eventBus.postEventForKey("testKey", dict);

		//test posting method with variables, should send correct
		Debug.Log("Test 12: Posting event with dictionary data");
		eventBus = new TinyEventBus();
		Debug.Log("Adding observer for one key");
		eventBus.addObserver(this, "testKey", "testMethodWithData2");
		Debug.Log("Posting with dictionary, with int value 2");
		dict = new Dictionary<string, object>();
		dict.Add ("dataKey", 2);
		eventBus.postEventForKey("testKey", dict);

		//testMethodWithData2

        //test posting event which is connected to method with incorrect signature
        Debug.Log("Test 13: Posting event which is connected to method with incorrect signature");
        Debug.Log("should result in nice error message pointing to the incorrect method");
        eventBus = new TinyEventBus();
        Debug.Log("Adding observer for one key");
        eventBus.addObserver(this, "testKey", "incorrectMethod");
        eventBus.postEventForKey("testKey");
    }

    public void postMethod(Dictionary<string, object> data)
    {
        Debug.Log("Test 2 post event recieved");
    }

    public void postMethod2(Dictionary<string, object> data)
    {
        Debug.Log("Test 4 post event recieved");
    }

    public void postMethod3(Dictionary<string, object> data)
    {
        Debug.Log("Event 1/3 recieved post event recieved");
    }

    public void postMethod4(Dictionary<string, object> data)
    {
        Debug.Log("Event 2/3 recieved post event recieved");
    }

    public void postMethod5(Dictionary<string, object> data)
    {
        Debug.Log("Event 3/3 recieved post event recieved");
    }

    public void postMethod6(Dictionary<string, object> data)
    {
        Debug.Log("TEST FAIL: this should not be printed (Test8)");
    }

    public void postMethod7(Dictionary<string, object> data)
    {
        Debug.Log("This should print (Test8)");
    }

    public void testMethodWithData(Dictionary<string, object> data)
    {
        string result = data != null ? "SUCCESS" : "FAILURE";
        Debug.Log("Test 11 finished with " + result);
    }

	public void testMethodWithData2(Dictionary<string, object> data) 
	{
		string result = data != null && (int)(data["dataKey"]) == 2 ? "SUCCESS" : "FAILURE";
		Debug.Log ("Test 12 finished with " + result);
	}

    public void incorrectMethod()
    {
        Debug.Log("this will not be called");
    }

    private class EventBusObserver
    {

        private int number;

        public EventBusObserver(string key, EventBus eventBus, int number)
        {
            eventBus.addObserver(this, key, "listenerMethod");
            this.number = number;
        }

        public void listenerMethod(Dictionary<string, object> data)
        {
            Debug.Log("recieved event on observer number " + this.number);
        }

    }

}
