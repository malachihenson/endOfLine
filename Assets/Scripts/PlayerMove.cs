using UnityEngine;
using UnityEngine.InputSystem;
//this was originally only going to include the movement code, but oh well. Renaming it now would break things in way too many places :P
public class PlayerMove : MonoBehaviour
{
    public int context = 0;
    public int currentArea = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private PlayerInput pi;
    [SerializeField] private SettingsManager sm;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private InventoryManager im;
    private InputAction moveAction;
    private InputAction sprintAction;
    private int inRadius = 0;
    private NPCController currNPC;
    private float realSpeed;
    private bool autoSprint;
    private float isSprinting;
    void Start()
    {
        UpdateSettings();
        pi.actions.FindActionMap("UI").Disable();
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
    }
    void FixedUpdate()
    {
        Vector3 moveValue = moveAction.ReadValue<Vector2>();
        isSprinting = sprintAction.ReadValue<float>();
        if(isSprinting == 1)
        {
            if(autoSprint)
            {
                realSpeed = moveSpeed;
            } else
            {
                realSpeed = moveSpeed * 2;
            }
        } else
        {
            if(autoSprint)
            {
                realSpeed = moveSpeed * 2;
            } else
            {
                realSpeed = moveSpeed;
            }
        }
        if(moveValue.x > 0)
        {
            playerAnim.SetInteger("WalkDir", 2);
        }
        if(moveValue.x < 0)
        {
            playerAnim.SetInteger("WalkDir", 4);
        }
        if(moveValue.y > 0)
        {
            playerAnim.SetInteger("WalkDir", 1);
        }
        if(moveValue.y < 0)
        {
            playerAnim.SetInteger("WalkDir", 3);
        }
        /*if(moveValue == new Vector3(0, 0, 0))
        {
            playerAnim.SetInteger("WalkDir", 0);
        }*/
        playerRB.MovePosition(gameObject.transform.position + (realSpeed * Time.fixedDeltaTime * moveValue));
    }
    /*player context states:
    1: npc dialog box
    2: battle(normal)
    3: battle(word submit)
    4: inventory*/
    public void Interact(InputAction.CallbackContext InputContext)
    {
        if(!InputContext.performed) return;
        switch(inRadius)
        {
            case 1:
                currNPC.Activate();
                break;
        }
    }
    public void Submit(InputAction.CallbackContext InputContext)
    {
        //add context for submitting text in battle soon please
        if(!InputContext.performed) return;
        switch(context)
        {
            case 1:
                //dialog box
                currNPC.Advance();
                break;
            case 4:
                im.SelectItem();
                break;
        }
    }
    public void Inventory(InputAction.CallbackContext InputContext)
    {
        if(!InputContext.performed) return;
        im.ToggleInv();
        pi.SwitchCurrentActionMap("UI");
        context = 4;
    }
    public void Quit(InputAction.CallbackContext InputContext)
    {
        if(!InputContext.performed) return;
        switch(context)
        {
            case 4:
                im.ToggleInv();
                pi.SwitchCurrentActionMap("Player");
                break;
        }
    }
    public void Navigate(InputAction.CallbackContext InputContext)
    {
        if(!InputContext.performed) return;
        switch(context)
        {
            case 4:
                if(InputContext.ReadValue<Vector2>().y > 0)
                {
                    im.ChangeSelection(1);
                } else if(InputContext.ReadValue<Vector2>().y < 0)
                {
                    im.ChangeSelection(0);
                } else if(InputContext.ReadValue<Vector2>().x > 0)
                {
                    im.ChangeSelection(3);
                } else if(InputContext.ReadValue<Vector2>().x < 0)
                {
                    im.ChangeSelection(2);
                }
                break;
        }
    }
    public void UpdateSettings()
    {
        autoSprint = sm.autoSprint;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // show interact tooltip here eventually
        if(other.gameObject.CompareTag("NPC"))
        {
            inRadius = 1;
            currNPC = other.gameObject.GetComponent<NPCController>();
            currNPC.ToggleTooltip(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        currNPC.ToggleTooltip(false);
        //maybe this could case errors in the future? check back here if problems with multiple collisions
        inRadius = 0;
    }
    public void BattleReturn()
    {
        pi.SwitchCurrentActionMap("Player");
    }
}
