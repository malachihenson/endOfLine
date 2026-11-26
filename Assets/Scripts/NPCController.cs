using UnityEngine;
using UnityEngine.InputSystem;
public class NPCController : MonoBehaviour
{
    [SerializeField] private DialogRenderer dr;
    [SerializeField] private PlayerInput pi;
    [SerializeField] private PlayerMove pm;
    [SerializeField] private int id;
    [SerializeField] private int startingLine;
    [SerializeField] private int endingLine;
    //note about events: they will always trigger before the text of their associated line
    [SerializeField] private int[] eventLines;
    [SerializeField] private int[] eventLineTypes;
    private int currentLine;
    private int currentEvent;
    void Start()
    {
        currentEvent = 0;
    }
    void Update()
    {
        
    }
    public void Activate()
    {
        pi.SwitchCurrentActionMap("UI");
        pm.context = 1;
        dr.ToggleBox(true);
        currentLine = startingLine;
        CheckEvent();
        dr.RenderText(currentLine, false);
    }
    public void Advance()
    {
        CheckEvent();
        if(currentLine < endingLine)
        {
            currentLine++;
            if(currentLine == endingLine)
            {
                dr.RenderText(currentLine, true);
            } else
            {
                dr.RenderText(currentLine, false);
            }
        } else
        {
            dr.ToggleBox(false);
            pi.SwitchCurrentActionMap("Player");
            pm.context = 0;
        }
    }
    private void CheckEvent()
    {
        if(eventLines[currentEvent] == currentLine)
        {
            switch(eventLineTypes[currentEvent])
            {
                case 0:
                    //battle
                    break;
            }
            currentEvent++;
        }
    }
}
