using TMPro;
using UnityEngine;

public class DiePlayer : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject player;              // o próprio player
    [SerializeField] private GameObject playerDeathScreen;   // tela "You Died"
    [SerializeField] private GameObject RetryButton;         // botão próprio dessa tela (não mais o ResetButton do Chegada)

    private void Start()
    {
        playerDeathScreen.SetActive(false);
        RetryButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fantasma")) return;    // só reage se for um fantasma

        GhostController.jogoVencido = true;            // congela TODOS os fantasmas (mesma flag reaproveitada do Chegada.cs)

        player.GetComponent<CharacterController>().enabled = false;   // player para de se mover / colidir

        var movimento = player.GetComponent<MovePlayer>();
        if (movimento != null)
            movimento.enabled = false;                 // desliga o script de input, sem tocar na câmera (filha)

        playerDeathScreen.SetActive(true);
        RetryButton.SetActive(true);
    }

    public void ResetUI()
    {
        playerDeathScreen.SetActive(false);
        RetryButton.SetActive(false);
    }
}