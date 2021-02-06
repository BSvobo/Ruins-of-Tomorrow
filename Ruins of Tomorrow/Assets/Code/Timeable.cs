using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeable : MonoBehaviour
{
    // Start is called before the first frame update
    public enum timeState { Past, Present};
    timeState currentTime = timeState.Present;
    Sprite pastSprite;
    Sprite presentSprite;

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
            return;
        }
        else
        {
            if(new_time == timeState.Present)
            {
                currentTime = timeState.Past;
            }
            else
            {
                currentTime = timeState.Present;
            }
        }
    }
}
