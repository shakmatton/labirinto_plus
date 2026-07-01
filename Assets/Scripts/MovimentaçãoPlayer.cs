using UnityEngine;
using UnityEngine.InputSystem;


public class MovimentaçãoPlayer : MonoBehaviour
{
    private CharacterController ch;
    private Animator anim;
    [SerializeField] private Transform modelo;
    private float movementX;
    private float movementY;
    public float speed = 5;
    public float rotationSpeed = 10f;
    private float tempSpeed;
    
    void Start()
    {
        tempSpeed = speed;
        ch = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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

    void Update()
    {
        

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Debug.Log(movement);
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            modelo.rotation = Quaternion.Slerp(
                modelo.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        ch.Move(movement * tempSpeed * Time.deltaTime);
        float valorSpeed = movement.magnitude * tempSpeed;

        Debug.Log(valorSpeed);

        anim.SetFloat("Speed", valorSpeed);
        anim.SetFloat("Speed", movement.magnitude * tempSpeed);
    }
}
