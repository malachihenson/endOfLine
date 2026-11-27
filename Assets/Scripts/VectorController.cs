using System;
using System.Collections.Generic;
using UnityEngine;

public class VectorController : MonoBehaviour
{
    public float approachSpeed;
    public bool moving = false;
    public int setback = 50;
    [SerializeField] private bool flipped;
    [SerializeField] private GameObject vectorTrail;
    [SerializeField] private RectTransform rt;
    [SerializeField] private BrickSpawner bs;
    private int offset;
    private int pcs = 0;
    private List<GameObject> trails = new List<GameObject>();
    void Start()
    {
        
    }
    void Update()
    {
        //eventually add sine wiggle here
        if(moving)
        {
            transform.Translate(Vector3.up * approachSpeed * Time.deltaTime);
        }
    }
    public void BeginPath()
    {
        offset = -7;
        trails.Clear();
        moving = true;
        InvokeRepeating("SpawnTrail", 2, 5f);
    }
    public void EndPath()
    {
        CancelInvoke();
        moving = false;
    }
    private void SpawnTrail()
    {
        var newTrail = Instantiate(vectorTrail, Vector3.zero, transform.localRotation);
        newTrail.transform.parent = this.transform;
        newTrail.transform.localScale = Vector3.one;
        newTrail.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, offset, 0);
        trails.Add(newTrail);
        offset -= 7;
        if(pcs == 5)
        {
            CancelInvoke();
        }
        pcs++;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        bs.bricks.Remove(col.gameObject);
        Destroy(col.gameObject);
        if(flipped)
        {
            transform.position += Vector3.up * setback;
        } else
        {
            transform.position += Vector3.down * setback;
        }
    }
    public void EndGame()
    {
        for(int i=0; i<trails.Count; i++)
        {
            Destroy(trails[i]);
        }
    }
}
