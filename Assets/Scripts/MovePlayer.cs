using UnityEngine;
using UnityEngine.InputSystem;


public class MovePlayer : MonoBehaviour
{
    
    private CharacterController ch;
    private float movementX;
    private float movementY;
    public float speed = 5;
    private float tempSpeed;

    private void Start()
    {
        tempSpeed = speed;
        ch = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector3 movementInput = ctx.ReadValue<Vector2>();
        movementX = movementInput.x;
        movementY = movementInput.y;
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            tempSpeed = speed * 2;
        }
        else if (ctx.canceled)
        {
            tempSpeed = speed;
        }
    }

    private void Update()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Debug.Log(movement);
        ch.Move(movement * tempSpeed * Time.deltaTime);
    }
}
