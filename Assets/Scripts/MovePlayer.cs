using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;

public class MovimentaçãoPlayer : MonoBehaviour
{
    // Referência ao CharacterController usado para mover o jogador.
    private CharacterController ch;

    // Referência ao BoxCollider do jogador.
    private BoxCollider boxCollider;

    // Referência ao GameObject que representa a caixa que o jogador pode interagir.
    private GameObject box = null;

    // Referência ao Animator que controla as animações.
    private Animator anim;

    // Modelo visual que será rotacionado independentemente do Player.
    [SerializeField] private Transform modelo;

    // Valores de entrada do movimento nos eixos X e Z.
    private float movementX;
    private float movementY;

    // Flag que indica se o jogador está segurando a caixa.
    private bool grabbing = false;

    // Velocidade normal de deslocamento.
    public float speed = 5;

    // Velocidade de rotação do modelo.
    public float rotationSpeed = 10f;

    // Velocidade atual (normal ou corrida).
    private float tempSpeed;

    void Start()
    {
        // Inicializa a velocidade atual com a velocidade padrão.
        tempSpeed = speed;

        // Obtém o CharacterController do objeto Player.
        ch = GetComponent<CharacterController>();

        // Obtém o Animator do objeto.
        anim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Lê a entrada do controle (Vector2).
        Vector3 movementInput = ctx.ReadValue<Vector2>();

        // Armazena os valores dos eixos horizontal e vertical.
        movementX = movementInput.x;
        movementY = movementInput.y;
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        // Ao iniciar a corrida, dobra a velocidade.
        if (ctx.performed)
        {
            tempSpeed = speed * 2;
        }

        // Ao soltar o botão, volta à velocidade normal.
        else if (ctx.canceled)
        {
            tempSpeed = speed;
        }
    }
    //Evento que detecta se ha uma caixa na frente do player
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Caixa") && box == null)
        {
            box = other.gameObject;
            Debug.Log("Colidiu com a caixa: " + box.name);
        }
    }
    //Evento que detecta se o player saiu da colisão com a caixa
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == box)
        {
            box.layer = 0;
            box.GetComponent<FixedJoint>().connectedBody = null;
            box = null;
            grabbing = false;
            Debug.Log("Collision OFF " + grabbing);
        }
    }
    //Função que detecta se o player está tentando pegar a caixa
    public void OnGrab(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (box != null)
            {
                box.layer = 3;
                if(box.GetComponent<FixedJoint>() == null)
                {
                    box.AddComponent<FixedJoint>();
                }
                box.GetComponent<FixedJoint>().connectedBody = GetComponent<Rigidbody>();
                grabbing = true;
                Debug.Log("Grabbing the box: " + box.name);
            }
        }
        else if (ctx.canceled)
        {
            if (grabbing)
            {
                box.layer = 0;
                box.GetComponent<FixedJoint>().connectedBody = null;
                grabbing = false;
                Debug.Log("Released the box: " + box.name);
            }
        }
    }

    void Update()
    {
        // Cria o vetor de movimento no plano XZ.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Exibe o vetor de movimento no Console.
        // Debug.Log(movement);                                                 // Lembrando que cada Debug.Log descomentado é executado todo frame. Se o jogo roda a 60 FPS, são impressas aproximadamente:
                                                                                // 60 mensagens de movement por segundo; e 60 mensagens de valorSpeed por segundo. Total: cerca de 120 mensagens por segundo.                                                                                

        // Só gira o modelo se houver movimento e n'ao estiver carregando uma caixa.
        if (movement != Vector3.zero && !grabbing)
        {
            // Calcula a rotação desejada conforme a direção do movimento.
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // Rotaciona suavemente o modelo até a direção desejada.
            modelo.rotation = Quaternion.Slerp(
                modelo.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Move o CharacterController.
        ch.Move(movement * tempSpeed * Time.deltaTime);

        // Calcula o valor usado para controlar o Animator.
        float valorSpeed = movement.magnitude * tempSpeed;

        // Exibe o valor da velocidade no Console.
        // Debug.Log(valorSpeed);                                               // 60 mensagens de valorSpeed por segundo.

        // Atualiza o parâmetro "Speed" do Animator.
        anim.SetFloat("Speed", valorSpeed);
    }
}