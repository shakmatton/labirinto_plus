using Unity.VisualScripting;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;

    [Header("Movimento")]
    public float velocidade = 2f;
    public float distanciaLimiteParaPlayer = 0.5f;

    void Update()
    {
        MoverFantasma();
    }

    void MoverFantasma()
    {
        SeguirPlayer();
    }

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