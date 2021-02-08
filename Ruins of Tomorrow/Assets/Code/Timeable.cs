using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeable : MonoBehaviour
{
    // Start is called before the first frame update
    public enum timeState { Past, Present};
    private timeState currentTime = timeState.Present;
    public Sprite pastSprite;
    public Sprite presentSprite;

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
        Debug.Log("We successfully called changeTime on this " + this + " object");
        if(currentTime == new_time){
            Debug.Log("Time already set! No changes to be made to " + this + " sprite.");
            return;
        }
        else
        {
            if (new_time == timeState.Past)
            {
                currentTime = timeState.Past;
                Debug.Log("Setting sprite to past!");
                GetComponent<SpriteRenderer>().sprite = pastSprite;
            }
            else
            {
                currentTime = timeState.Present;
                Debug.Log("Setting sprite to present!");
                GetComponent<SpriteRenderer>().sprite = presentSprite;
            }
        }
    }
}
