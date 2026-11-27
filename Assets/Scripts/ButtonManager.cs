using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Image[] buttons;
    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;

    public void UpdateButton(int index)
    {
        buttons[index].sprite = selected;
        //there has to be a better way to do this :L
        buttons[index].gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
    }
    public void ResetAllButtons()
    {
        for(int i=0; i<buttons.Length; i++)
        {
            buttons[i].sprite = unselected;
            //in all honesty i'm definitely just lazy and didn't want to assign the values for 40 textboxes again :(
            //i think that alone would get me to philly
            buttons[i].gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Top;
        }
    }
}
