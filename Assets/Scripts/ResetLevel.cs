using UnityEngine;
 
public class ResetLevel : MonoBehaviour
{
    public GameObject player;                           // representa nosso personagem
    public GameObject final;                            // representa o ponto final de Chegada
    Vector3 inicial;                                    // variável que irá guardar uma dada posição     

    public GameObject diePlayer;                        // jogo resetado também pelo botão de Retry (após jogador morrer no contato com um fantasma)
                                                        // Arraste o player (que tem o DiePlayer.cs) no Inspector.

    private void Start()
    {
        inicial = player.transform.position;            // logo no começo, já salvamos a posição de início do personagem
    }
 
    public void ResetGame()                                                 // método que reseta o jogo
    {
        GhostController.jogoVencido = false;                                // se o jogo não foi vencido, os fantasmas podem continuar se movendo pelo labirinto.

        player.GetComponent<CharacterController>().enabled = false;         // desativamos o controle do jogador, para conseguirmos manipular a posição dele de modo seguro e sem interferências
        
        player.transform.position = inicial;                                // a posição do jogador é atualizada com o valor de posição da variável "inicial"

        var movimento = player.GetComponent<MovePlayer>();                  // buscamos pelo componente de movimentação do player
        if (movimento != null) movimento.enabled = true;                    // se ele existir, deixamos esse componente ativado

        final.GetComponent<Chegada>().ResetUI();                            // acessamos o método ResetUI(), do nosso objeto Chegada (o método está no script Chegada.cs)
        
        player.GetComponent<CharacterController>().enabled = true;          // reativamos o controle do jogador, que volta a poder controlar o personagem
        
        diePlayer.GetComponent<DiePlayer>().ResetUI();                      // ativamos o método ResetUI que há em DiePlayer.cs (botão Retry também reinicia o jogador na fase, tal como o botão Restart)
    }
}

