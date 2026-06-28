using UnityEngine;
 
public class ResetScript : MonoBehaviour
{
    public GameObject player;                           // representa nosso personagem
    public GameObject final;                            // representa o ponto final de Chegada
    Vector3 inicial;                                    // variável que irá guardar uma dada posição     
    
    void Start()
    {
        inicial = player.transform.position;            // logo no começo, já salvamos a posição de início do personagem
    }
 
    public void ResetGame()                                                 // método que reseta o jogo
    {
        player.GetComponent<CharacterController>().enabled = false;         // desativamos o controle do jogador, para conseguirmos manipular a posição dele de modo seguro e sem interferências
        
        player.transform.position = inicial;                                // a posição do jogador é atualizada com o valor de posição da variável "inicial"
                
        final.GetComponent<Chegada>().ResetUI();                            // acessamos o método ResetUI(), do nosso objeto Chegada (o método está no script Chegada.cs)
        
        player.GetComponent<CharacterController>().enabled = true;          // reativamos o controle do jogador, que volta a poder controlar o personagem
    }
}
