using System;
using UnityEngine;

public class VectorController : MonoBehaviour
{
    public float approachSpeed;
    public bool moving = false;
    public int setback = 50;
    [SerializeField] private bool flipped;
    [SerializeField] private GameObject vectorTrail;
    //[SerializeField] private Rigidbody2D rb;
    private int offset;
    private int pcs = 0;
    void Start()
    {
        if(flipped)
        {
            offset = 14;
        } else
        {
            offset = -14;
        }
    }
    void Update()
    {
        //eventually add sine wiggle here
        if(moving)
        {
            transform.Translate(Vector3.up * approachSpeed * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        
        
    }
    public void BeginPath()
    {
        moving = true;
        InvokeRepeating("SpawnTrail", 2, 2.8f);
    }
    private void SpawnTrail()
    {
        var newTrail = Instantiate(vectorTrail, Vector3.zero, transform.localRotation);
        newTrail.transform.parent = this.transform;
        newTrail.transform.localScale = Vector3.one;
        newTrail.transform.position = this.transform.position + new Vector3(0, offset, 0);
        if(flipped)
        {
            offset += 14;
        } else
        {
            offset -= 14;
        }
        if(pcs == 5)
        {
            CancelInvoke();
        }
        pcs++;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("boom");
        Destroy(col.gameObject);
        if(flipped)
        {
            transform.position += Vector3.up * setback;
        } else
        {
            transform.position += Vector3.down * setback;
        }
    }
}
