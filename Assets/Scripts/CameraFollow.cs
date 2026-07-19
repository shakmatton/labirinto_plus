using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo")]
    public Transform player;                             // referência ao Transform do player

    [Header("Posicionamento")]
    public Vector3 offset = new Vector3(0f, 10f, 0f);    // ajuste esses valores para reproduzir a posição relativa que a câmera já tinha como filha

    private void LateUpdate()                            // LateUpdate: roda DEPOIS do Update do player, evitando "tremor" de câmera atrasada em relação ao movimento
    {
        if (player == null) return;                      // segurança: se a referência sumir, o jogo não quebra
        transform.position = player.position + offset;   // reproduz exatamente o comportamento de câmera-filha (segue sem atraso nenhum)
    }
}


/*
 *  Mesmo com o player desativado (SetActive(false)), a leitura de player.position continua funcionando normalmente.
 *  Desativar um GameObject não apaga o Transform dele, só impede que Update()/renderização rodem. 
 *  
 *  Então, quando o player morrer, a câmera simplesmente vai congelar na última posição válida, 
 *  olhando pro local exato onde o player foi pego pelo fantasma. Isso é o comportamento desejado. */