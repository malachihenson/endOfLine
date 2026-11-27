using UnityEngine;
using UnityEngine.InputSystem;
public class NPCController : MonoBehaviour
{
    [SerializeField] private DialogRenderer dr;
    [SerializeField] private PlayerInput pi;
    [SerializeField] private PlayerMove pm;
    [SerializeField] private BattleManager bm;
    [SerializeField] private int id;
    [SerializeField] private int startingLine;
    [SerializeField] private int endingLine;
    //note about events: they will always trigger before the text of their associated line
    [SerializeField] private int[] eventLines;
    [SerializeField] private int[] eventLineTypes;
    private int currentLine;
    private int currentEvent;
    private bool inBattle = false;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Activate()
    {
        currentEvent = 0;
        inBattle = false;
        pi.SwitchCurrentActionMap("UI");
        pm.context = 1;
        dr.ToggleBox(true);
        currentLine = startingLine;
        CheckEvent();
        dr.RenderText(currentLine, false);
        currentLine++;
    }
    public void Advance()
    {
        CheckEvent();
        if(currentLine < endingLine)
        {
            if(currentLine == endingLine)
            {
                dr.RenderText(currentLine, true);
            } else
            {
                dr.RenderText(currentLine, false);
            }
            currentLine++;
        } else
        {
            dr.ToggleBox(false);
            if(!inBattle)
            {
                pi.SwitchCurrentActionMap("Player");
            }
            pm.context = 0;
        }
    }
    private void CheckEvent()
    {
        if(eventLines.Length > 0)
        {
            if(eventLines[currentEvent] == currentLine)
            {
                switch(eventLineTypes[currentEvent])
                {
                    case 0:
                        inBattle = true;
                        bm.StartBattle();
                        break;
                }
                currentEvent++;
            }
        }
    }
}
