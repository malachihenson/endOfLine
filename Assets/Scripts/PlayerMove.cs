using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//this was originally only going to include the movement code, but oh well. Renaming it now would break things in way too many places :P
public class PlayerMove : MonoBehaviour
{
    public int context = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private PlayerInput pi;
    [SerializeField] private SettingsManager sm;
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
        playerRB.MovePosition(gameObject.transform.position + (realSpeed * Time.fixedDeltaTime * moveValue));
    }
    public void Interact(InputAction.CallbackContext InputContext)
    {
        if(!InputContext.performed) return;
        switch(inRadius)
        {
            case 1:
                //npc
                currNPC.Activate();
                break;
        }
    }
    public void Submit(InputAction.CallbackContext InputContext)
    {
        if(!InputContext.performed) return;
        switch(context)
        {
            case 1:
                //dialog box
                currNPC.Advance();
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
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //maybe this could case errors in the future? check back here if problems with multiple collisions
        inRadius = 0;
    }
    public void BattleReturn()
    {
        pi.SwitchCurrentActionMap("Player");
    }
}
