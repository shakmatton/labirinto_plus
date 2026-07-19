using UnityEngine;
using System.Collections.Generic;                       // necessário para usar List<>

public class ResetLevel : MonoBehaviour
{
    public GameObject player;                           // representa nosso personagem
    public GameObject final;                            // representa o ponto final de Chegada
    private Vector3 inicial;                            // variável que irá guardar uma dada posição     

    public GameObject diePlayer;                        // jogo resetado também pelo botão de Retry (após jogador morrer no contato com um fantasma)
                                                        // Arraste o player (que tem o DiePlayer.cs) no Inspector.

    public List<GhostController> fantasmas;             // arraste cada fantasma da cena aqui, no Inspector

    private void Start()
    {
        inicial = player.transform.position;            // logo no começo, já salvamos a posição de início do personagem
    }
 
    public void ResetGame()                                                 // método que reseta o jogo
    {
        GhostController.jogoVencido = false;                                // se o jogo não foi vencido, os fantasmas podem continuar se movendo pelo labirinto.

        player.SetActive(true);                                             // reativa o player, caso tenha sido desativado pela situação de morte (contato com fantasma)
        player.GetComponent<CharacterController>().enabled = false;         // desativa o controle do jogador só para reposicionar com segurança
        player.transform.position = inicial;                                // a posição do jogador é atualizada com o valor de posição da variável "inicial"
        player.GetComponent<CharacterController>().enabled = true;          // reativa o controle do jogador     

        foreach (GhostController fantasma in fantasmas)                     // percorre a lista e reseta cada fantasma, um por um
            fantasma.ResetPosicao();

        final.GetComponent<Chegada>().ResetUI();                            // acessamos o método ResetUI(), do nosso objeto Chegada (o método está no script Chegada.cs)                        
        diePlayer.GetComponent<DiePlayer>().ResetUI();                      // ativamos o método ResetUI que há em DiePlayer.cs (botão Retry também reinicia o jogador na fase, tal como o botão Restart)
    }
}

