using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform playerTransform;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
    }
}
