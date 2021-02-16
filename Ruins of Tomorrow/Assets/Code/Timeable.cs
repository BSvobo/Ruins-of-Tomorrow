using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeable : MonoBehaviour
{
    // Start is called before the first frame update
    public enum timeState { Past, Present};
    public timeState currentTime;

    public GameObject pastObject;
    public GameObject presentObject;

    private Vector3 position;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Actually add stuff here to change the state of the object as necessary, this just makes it actually affected by the change in time
    //Should probably write more functions to change sprite, interactability/collider, etc.
    public void changeTime(timeState new_time)
    {
        if(currentTime == new_time){
            Debug.Log("Time already set! No changes to be made to " + this + " object.");
            return;
        }
        else
        {
            position = GetComponent<Transform>().position;

            if (new_time == timeState.Past)
            {
                currentTime = timeState.Past;
                Debug.Log("Setting this " + this + " object to past!");
                Destroy(gameObject);
                var newobj = Instantiate(pastObject);
                newobj.GetComponent<Transform>().position = position;
            }
            else
            {
                currentTime = timeState.Present;
                Debug.Log("Setting this " + this + "object to present!");
                Destroy(gameObject);
                var newobj = Instantiate(presentObject);
                newobj.GetComponent<Transform>().position = position;
            }
        }
    }

    public timeState GetTimeState()
    {
        return currentTime;
    }
}
