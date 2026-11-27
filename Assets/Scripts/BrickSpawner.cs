using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject brick1;
    [SerializeField] private GameObject brick2;
    [SerializeField] private bool flipped;
    private int nextBrickType = 0;
    private float offset = 0;
    private GameObject brick;
    private List<GameObject> bricks = new List<GameObject>();
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void SpawnBrick(int times)
    {
        //animate this using invokerepeating eventually
        for(int i=0; i<times; i++)
        {
            for(int j=0; j<bricks.Count; j++)
            {
                bricks[j].GetComponent<BrickController>().MoveUp(flipped);
            }
            if(nextBrickType == 0)
            {
                brick = Instantiate(brick1, Vector3.zero, transform.rotation);
                nextBrickType = 1;
            } else
            {
                brick = Instantiate(brick2, Vector3.zero, transform.rotation);
                nextBrickType = 0;
            }
            brick.transform.parent = this.transform;
            brick.transform.localScale = Vector3.one;
            if(flipped)
            {
                brick.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 101, 0);
            } else
            {
                brick.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -101, 0);
            }
            bricks.Add(brick);
        }
    }
    public void EndGame()
    {
        for(int i=0; i<bricks.Count; i++)
        {
            Destroy(bricks[i]);
        }
        bricks.Clear();
    }
}
