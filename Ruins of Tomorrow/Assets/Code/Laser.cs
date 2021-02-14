using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    private LineRenderer l;
    private Vector3 gun;
    public Vector2 dir;
    private Timeable _timeable;
    private float i;

    // Start is called before the first frame update
    void Start()
    {
        l = gameObject.GetComponent<LineRenderer>();
        gun = transform.position;
        _timeable = gameObject.GetComponent<Timeable>();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeable.GetTimeState() == Timeable.timeState.Past)
        {
            LaserRender();
        }
    }

    void LaserRender()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(gun, dir);
        RaycastHit2D hit = hits[1];
        
        Vector3 hitPoint = hit.point;
        List<Vector3> pos = new List<Vector3>(); 
        pos.Add(gun);
        pos.Add(hitPoint);
        l.startWidth = .1f; 
        l.endWidth = .1f;
        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;
            
        if (hit.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        /* else if (hit.collider.CompareTag("Crate"))
        {
            Destroy(hit.collider.gameObject);
        }*/
    }
    
    
}
