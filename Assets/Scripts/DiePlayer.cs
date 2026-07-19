using TMPro;
using UnityEngine;

public class DiePlayer : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject player;              // o próprio player
    [SerializeField] private GameObject playerDeathScreen;   // tela "You Died"
    [SerializeField] private GameObject RetryButton;         // botão próprio dessa tela (não mais o ResetButton do Chegada)
    [SerializeField] private GameObject chegada;             // arraste aqui o objeto que tem o script Chegada.cs

    private void Start()
    {
        playerDeathScreen.SetActive(false);
        RetryButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fantasma")) return;                           // só reage se for um fantasma

        GhostController.jogoVencido = true;                                  // congela TODOS os fantasmas (mesma flag reaproveitada do Chegada.cs)
        player.SetActive(false);                                             // player desativado (sem problemas, pois a câmera não é mais filha dele - ver comentários sobre a câmera em CameraFollow.cs).

        chegada.GetComponent<Chegada>().EsconderTelaVitoria();               // esconde a tela de vitória, caso ela esteja visível

        playerDeathScreen.SetActive(true);
        RetryButton.SetActive(true);
    }

    public void ResetUI()
    {
        playerDeathScreen.SetActive(false);
        RetryButton.SetActive(false);
    }
}    
