using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public Timeable.timeState LocalTimeState;
    public int CastRadius;
    private bool called = false; 
    Collider2D[] Affectables;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Time.unscaledTime > 5 && !called && LocalTimeState == Timeable.timeState.Present)
        {
            called = true;
            sendThemBack();
        }*/
    }

    public void sendThemBack()
    {
        var CircleCenter = GetComponent<Transform>().transform.position;
        Affectables = Physics2D.OverlapCircleAll(new Vector2(CircleCenter.x, CircleCenter.y), CastRadius);
        Debug.Log("We called sendThemBack() at " + Time.unscaledTime + "\r\nNumber of objects to change is " + Affectables.Length);

        foreach (Collider2D changing in Affectables)
        {
            Debug.Log("Changing object " + changing.gameObject);
        }

        int objects_affected = 0;

        //Get the time state we want to change the radius to
        Timeable.timeState toChange = newTime();

        //Change the time state of all of the (affectable) objects in the radius
        foreach (Collider2D collider in Affectables)
        {
            Timeable iter = collider.gameObject.GetComponent<Timeable>();
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