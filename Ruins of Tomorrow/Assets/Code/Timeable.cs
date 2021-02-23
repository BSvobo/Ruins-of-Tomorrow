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
    private Quaternion rotation;
    private Vector2 laserdirection;
    private int radius;

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
            rotation = GetComponent<Transform>().rotation;
            var laser = GetComponent<Laser>();
            var clockrock = GetComponent<ChangeTime>();


            if (new_time == timeState.Past)
            {
                currentTime = timeState.Past;
                //Debug.Log("Setting this " + this + " object to past!");
                if (laser)
                {
                    laserdirection = laser.dir;
                    Destroy(this.gameObject);
                    var newobj = Instantiate(pastObject);
                    newobj.GetComponent<Laser>().dir = laserdirection;
                    newobj.GetComponent<Transform>().position = position;
                    newobj.GetComponent<Transform>().rotation = rotation;
                }
                else if (clockrock)
                {
                    radius = clockrock.CastRadius;
                    Destroy(this.gameObject);
                    var newobj = Instantiate(pastObject);
                    newobj.GetComponent<ChangeTime>().CastRadius = radius;
                    newobj.GetComponent<Transform>().position = position;
                }
                else
                {
                    Destroy(this.gameObject);
                    var newobj = Instantiate(pastObject);
                    newobj.GetComponent<Transform>().position = position;
                }
            }
            else
            {
                currentTime = timeState.Present;
                //Debug.Log("Setting this " + this + "object to present!");
                if (laser)
                {
                    laserdirection = laser.dir;
                    Destroy(this.gameObject);
                    var newobj = Instantiate(presentObject);
                    newobj.GetComponent<Laser>().dir = laserdirection;
                    newobj.GetComponent<Transform>().position = position;
                }
                else if (clockrock)
                {
                    radius = clockrock.CastRadius;
                    Destroy(this.gameObject);
                    var newobj = Instantiate(presentObject);
                    newobj.GetComponent<ChangeTime>().CastRadius = radius;
                    newobj.GetComponent<Transform>().position = position;
                }
                else
                {
                    Destroy(this.gameObject);
                    var newobj = Instantiate(presentObject);
                    newobj.GetComponent<Transform>().position = position;
                }
            }
        }
    }

    public timeState GetTimeState()
    {
        return currentTime;
    }
}
