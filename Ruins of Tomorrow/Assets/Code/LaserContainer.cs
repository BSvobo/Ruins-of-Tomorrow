using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserContainer : MonoBehaviour
{
    private int _children;
    private AudioSource _buzzing;
    
    // Start is called before the first frame update
    void Start()
    {
        _children = transform.childCount;
        _buzzing = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        Audio();   
    }

    void Audio()
    {
        if (ChildPast())
        {
            if (Player.inPauseMenu)
            {
                _buzzing.Stop();
            }
            else
            {
                if (!_buzzing.isPlaying)
                {
                    _buzzing.Play();
                }
            }
        }
        else
        {
            _buzzing.Stop();
        }
    }

    bool ChildPast()
    {
        for (int i = 0; i < _children; i++)
        {
            if (transform.GetChild(i).GetComponent<Timeable>().GetTimeState() == Timeable.timeState.Past)
            {
                return true;
            }
        }
        return false;
    }
        
}
