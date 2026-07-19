using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject player;                           // representa nosso personagem
    public GameObject final;                            // representa o ponto final de Chegada
    Vector3 inicial;                                    // variável que irá guardar uma dada posição     
    
    public string nextLevelName;

    private void Start()
    {
        inicial = player.transform.position;            // logo no começo, já salvamos a posição de início do personagem
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);          // simplesmente carrega a nova cena. Note que LoadScene() por padrão é assíncrona na prática — ela destrói os objetos da cena atual.
                                                        // Por quê isso funciona? Cada cena tem seu próprio player na posição inicial e sua própria Chegada com UI oculta. Não há nada para resetar manualmente.

    // Caso houvesse mais linhas após ela, elas tentariam acessar objetos que já não existem (ou estão sendo destruídos), o que causaria o erro no Update() de MovimentaçãoPlayer.
    // Assim, não é necessário resetar nem a posição nem a UI aqui — a nova cena já inicializa tudo pelo Start() dos seus scripts.

    }
}


