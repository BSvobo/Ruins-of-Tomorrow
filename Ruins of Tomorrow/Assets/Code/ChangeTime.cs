using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public static Timeable.timeState LocalTimeState = Timeable.timeState.Present;
    private bool called = false; 
    Collider2D[] Affectables;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.unscaledTime > 5 && !called)
        {
            called = true;
            sendThemBack();
        }
    }

    void sendThemBack()
    {
        Debug.Log("We called sendThemBack() at " + Time.unscaledTime);
        var CircleCenter = GetComponent<Transform>().transform.position;
        Affectables = Physics2D.OverlapCircleAll(new Vector2(CircleCenter.x, CircleCenter.y), 5f);

        int objects_affected = 0;

        if (Affectables.Length != 0)
        {
            Debug.Log("WE GOT A LIVE ONE BOB!");
        }
       

        //Get the time state we want to change the radius to
        Timeable.timeState toChange = newTime();

        //Change the time state of all of the (affectable) objects in the radius
        foreach (Collider2D collider in Affectables)
        {
            var iter = collider.gameObject.GetComponent<Timeable>();
            if (!iter || !iter.enabled)
            {
                continue;
            }
            else
            {
                objects_affected += 1;
                iter.changeTime(toChange);
            }
        }

        LocalTimeState = toChange;
    }

    Timeable.timeState newTime()
        {
            if (LocalTimeState == Timeable.timeState.Present)
            {
                return Timeable.timeState.Past;
            }
            else
            {
                return Timeable.timeState.Present;
            }
        }
}