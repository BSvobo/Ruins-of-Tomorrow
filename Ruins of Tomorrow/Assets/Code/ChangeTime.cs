using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    Collider[] Affectables;
    public static Timeable.timeState LocalTimeState = Timeable.timeState.Present;
    // Start is called before the first frame update
    void Start()
    {
         Affectables = Physics.OverlapSphere(GetComponent<Transform>().position, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sendThemBack()
    {
        int objects_affected = 0;

        //Get the time state we want to change the radius to
        Timeable.timeState toChange = newTime();

        //Change the time state of all of the (affectable) objects in the radius
        foreach (Collider collider in Affectables)
        {
            var iter = collider.gameObject.GetComponent<Timeable>();
            if (!iter)
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