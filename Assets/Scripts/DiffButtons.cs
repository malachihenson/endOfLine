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
    public void ResetButtons()
    {
        selected = -1;
        buttons[0].sprite = unselectedSprites[0];
        buttons[1].sprite = unselectedSprites[1];
        buttons[2].sprite = unselectedSprites[2];
    }
}
