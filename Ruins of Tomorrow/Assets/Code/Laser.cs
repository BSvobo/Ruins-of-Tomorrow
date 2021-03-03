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
    public AudioSource hitAudio;
    public AudioClip hitPlayer;
    public GameObject particleHolder;
    private Vector3 laserspot;
    private GameObject particleHolderToDestroy;


    //private float i;

    // Start is called before the first frame update
    void Start()
    {
        l = gameObject.GetComponent<LineRenderer>();
        gun = transform.position;
        _timeable = gameObject.GetComponent<Timeable>();
        
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
        RaycastHit2D hit;

        if (hits[1].collider.isTrigger)
        {
            hit = hits[2];
        }else{hit = hits[1];}
        
        Vector3 hitPoint = hit.point;
        List<Vector3> pos = new List<Vector3>(); 
        pos.Add(gun);
        pos.Add(hitPoint);
        l.startWidth = .15f; 
        l.endWidth = .15f;
        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;

        if (laserspot != hitPoint)
        {
            Destroy(particleHolderToDestroy);
            var particles = Instantiate(particleHolder);
            particles.transform.position = hitPoint;
            particles.transform.parent = gameObject.transform; 
            particleHolderToDestroy = particles;
        }
        
        laserspot = hitPoint;

        if (hit.collider.CompareTag("Player"))
        {
            hit.collider.GetComponent<Animator>().SetBool("dying", true);
            if (!hitAudio.isPlaying)
            {
                hitAudio.PlayOneShot(hitPlayer);
            }

            StartCoroutine("ResetLevelCo");
        }

        if (hit.collider.CompareTag("Crate"))
        {
            Color newColor = new Vector4(0.00027f, 0.00027f, 0.00027f, 0f);
            hit.collider.GetComponent<SpriteRenderer>().color = hit.collider.GetComponent<SpriteRenderer>().color - newColor;
        }
    }

    public IEnumerator ResetLevelCo()
    {
        yield return new WaitForSeconds(.694f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
