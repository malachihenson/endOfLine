using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject brick1;
    [SerializeField] private GameObject brick2;
    [SerializeField] private bool flipped;
    private int nextBrickType = 0;
    private int brickHeight;
    private float offset = 0;
    private GameObject brick;
    void Start()
    {
        if(flipped)
        {
            brickHeight = -117;
        } else
        {
            brickHeight = 117;
        }
    }
    void Update()
    {
        
    }
    public void SpawnBrick(int times)
    {
        //animate this using invokerepeating eventually
        for(int i=0; i<times; i++)
        {
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
            brick.transform.position = this.transform.position + new Vector3(0, (brickHeight * 2) - offset, 0);
            if(flipped)
            {
                offset -= 15f;
            } else
            {
                offset += 15f;
            }
        }
    }
}
