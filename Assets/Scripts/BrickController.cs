using UnityEngine;

public class BrickController : MonoBehaviour
{
    private RectTransform rt;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    //eventually interpolate this movement
    public void MoveUp(bool flipped)
    {
        if(flipped)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition -= Vector2.up * 7;
        } else
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition += Vector2.up * 7;
        }
    }
}
