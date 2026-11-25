using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    InputAction moveAction;
    private Vector2 moveInput;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveValue = moveAction.ReadValue<Vector2>();
        gameObject.transform.Translate(moveValue * moveSpeed * Time.deltaTime);
    }
}
