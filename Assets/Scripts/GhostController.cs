using Unity.VisualScripting;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;                                    // referência de posição (para quem o inimigo vai olhar)

    [Header("Movimento")]                                       
    public float velocidade = 2f;                               // velocidade do inimigo
    public float distanciaLimiteParaPlayer = 0.5f;              // distância mínima que o inimigo deve se manter do centro do player    

    [Header("Posição Y")]                                       
    public float alturaY = 1.85f;                               // altura inicial do fantasma em relação ao chão

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, alturaY, transform.position.z);          // altura inicial aplicada logo no começo do jogo
    }

    void Update()
    {
        MoverFantasma();                                                                // método usado abaixo    
        // SeguirPlayer();                                                              // método alternativo (escolha um dos dois métodos para ser comentado) 
    }

    void MoverFantasma()
    {
     
        Vector3 direcao = player.position - transform.position;                         // vetor que representa a distância entre o player e o inimigo

        direcao.y = 0f;                                                                 // vetor direção: coordenada 3D no espaço. O eixo y zerado significa que nos interessa só o que ocorre no plano XZ.

        float distancia = direcao.magnitude;                                            // distancia recebe o valor do módulo (comprimento) do vetor direção.

        if (distancia <= distanciaLimiteParaPlayer)                                     // se a distancia entre o jogador e o inimigo for menor que distanciaLimiteParaPlayer, não se faz mais nada.
            return;                                                                     // assim, inimigo ficará parado, em vez de tentar continuar a se mover e avançar sobre o player.

        // ===== Movimento ======

        transform.position += direcao.normalized * velocidade * Time.deltaTime;         // posição do inimigo: vetor unitário de Direção X Velocidade X Tempo

        // ===== Rotação ========

        transform.LookAt(                                                               // LookAt: "para onde o inimigo olha".
            new Vector3(                                                                // O inimigo se volta para um outra coordenada 3D, dada pela posição do player no lano XZ e a altura Y do inimigo na cena.
                player.position.x,
                transform.position.y,   
                player.position.z));        

        // transform.Rotate(0, 0, 0);                                                   // comando para rotacionar comentado porque o combo "movimento + rotação" mais acima já resolve a questão.
    }
}


    // ============ OUTRA FORMA DE FAZER: =================

    /*
    void SeguirPlayer()
    {
        Vector3 direcao = player.position - transform.position;
        direcao.y = 0f;

        float distancia = direcao.magnitude;

        if (distancia <= distanciaLimiteParaPlayer)
            return;

        // Movimento
        transform.position +=
            direcao.normalized *
            velocidade *
            Time.deltaTime;

        // Rotação
        float angulo = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg;

        // transform.rotation = Quaternion.Euler(0, angulo, 0);
        transform.rotation = Quaternion.Euler(0, angulo + 90f, 0);
        // transform.rotation = Quaternion.Euler(0, angulo - 90f, 0);
        // transform.rotation = Quaternion.Euler(0, angulo + 180f, 0);


    }
}


/*

 SOBRE A ROTAÇÃO DO FANTASMA

 A função Mathf.Atan2(x, z) calcula o ângulo do vetor "direcao" em relação ao
 eixo Z do mundo (plano XZ). Em outras palavras, ela determina para qual direção
 o fantasma deve olhar para ficar voltado ao player. O resultado é retornado em
 radianos e, por isso, é convertido para graus com Mathf.Rad2Deg, já que o Unity
 representa rotações em graus.

 O Unity considera que a frente ("forward") de qualquer objeto aponta para o seu
 eixo local +Z. Assim, ao aplicar esse ângulo à rotação do objeto, estamos dizendo
 ao Unity para alinhar seu eixo +Z com a direção do player.

 Entretanto, este modelo do fantasma foi criado/exportado no Blender com sua face
 orientada para um eixo diferente do esperado pelo Unity. Isso faz com que exista
 uma diferença entre:

 • Orientação lógica: para onde o código manda o fantasma olhar;
 • Orientação visual: para onde a face do modelo realmente aponta.

 Como consequência, ao usar apenas:

     transform.rotation = Quaternion.Euler(0, angulo, 0);

 o fantasma perseguia corretamente o player, porém caminhava "de lado", pois sua
 orientação visual não coincidia com sua orientação lógica.

 Para compensar essa diferença, foi aplicado um deslocamento (offset) de +90° ao
 ângulo calculado:

     transform.rotation = Quaternion.Euler(0, angulo + 90f, 0);

 Esse offset NÃO altera a direção da perseguição; ele apenas corrige a orientação
 visual do modelo, fazendo com que a face do fantasma aponte para a mesma direção
 calculada pelo script.

 Observação:
 O valor de +90° não é uma constante da Unity nem da matemática. Ele depende
 exclusivamente da orientação em que o modelo 3D foi criado e exportado. Caso um
 novo modelo seja utilizado futuramente, esse offset poderá precisar ser alterado
 (por exemplo, para -90° ou 180°), até que a orientação visual do modelo coincida
 com a orientação lógica utilizada pelo código.

    transform.rotation = Quaternion.Euler(0, angulo + 90f, 0); 

 */