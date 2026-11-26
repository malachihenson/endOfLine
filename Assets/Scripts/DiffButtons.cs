using UnityEngine;
using UnityEngine.UI;
public class DiffButtons : MonoBehaviour
{
    [SerializeField] private Image[] buttons;
    [SerializeField] private Sprite[] unselectedSprites;
    [SerializeField] private Sprite[] selectedSprites;
    private int selected = -1;
    public void SwitchSelection(int buttonNum)
    {
        if(selected != buttonNum)
        {
            if(selected != -1)
            {
                buttons[selected].sprite = unselectedSprites[selected];
            }
            buttons[buttonNum].sprite = selectedSprites[buttonNum];
            selected = buttonNum;
        }
    }
}
