using UnityEngine;
using TMPro;
public class Chegada : MonoBehaviour
{
    public TextMeshProUGUI textoVitoria;
    public GameObject ResetButton;
    public GameObject NextLevelButton;              // opcional: se vazio no Inspector, é ignorado em todo o script

    void Start()                                    // faz a primeira configuração inicial dos objetos e componentes
    {
        textoVitoria.enabled = false;               // no início, o componente textoVitoria tem que ficar escondido 

        ResetButton.SetActive(false);               // no início, o objeto ResetButton inteiro não deve estar ativo na cena

        if (NextLevelButton != null)                // só executa se o objeto existir no Inspector
        {
            NextLevelButton.SetActive(false);       // no início (caso exista), o objeto NextLevelButton inteiro não deve estar ativo na cena 
        }                                                                                               
    }
    void OnTriggerEnter()                           // quando personagem atinge Chegada, esse evento dispara
    {
        textoVitoria.enabled = true;                // personagem na Chegada vê mensagem de fim/vitória na tela
        ResetButton.SetActive(true);                // personagem na Chegada vê objeto ResetButton na tela (que faz o personagem retornar ao início do labirinto)

        if (NextLevelButton != null)                // só executa se o objeto existir no Inspector
            NextLevelButton.SetActive(true);
    }
    public void ResetUI()                           // controla a visibilidade do objeto Reset  
    {
        textoVitoria.enabled = false;               // botão ResetButton clicado significa: remova mensagem de fim/vitória na tela
        ResetButton.SetActive(false);               // botão ResetButton clicado significa: objeto ResetButton já foi clicado e cumpriu sua função, e já pode desaparecer da tela.

        if (NextLevelButton != null)                // só executa se o objeto existir no Inspector
            NextLevelButton.SetActive(false);
    }    
}

















// (Versões antigas abaixo funcionavam com um único botão de Reset...)

/*  =======  Versão 2, mais enxuta  =======
 
 public class Chegada : MonoBehaviour
{
    public TextMeshProUGUI TextoFinal;
    public GameObject Reset;

    void Start()
    {
        OcultarUI();                                // OcultarUI(): método elimina redundância, aparecendo no Start() e no ResetUI()
    }

    public void ResetUI()
    {
        OcultarUI();                                // OcultarUI(): método elimina redundância, aparecendo no Start() e no ResetUI()
    }

    void OnTriggerEnter()
    {
        MostrarUI();                                // MostrarUI(): método elimina redundância, aparecendo no onTriggerEnter()
    }

    private void OcultarUI()                        // método chamado por Start() e ResetUI()
    {
        TextoFinal.enabled = false;
        Reset.SetActive(false);
    }

    private void MostrarUI()                        // método chamado por onTriggerEnter()
    {
        TextoFinal.enabled = true;
        Reset.SetActive(true);
    }
}

 */


/*  =======  Versão 3, ainda mais enxuta e com parâmetro bool =======

using UnityEngine;
using TMPro;

public class Chegada : MonoBehaviour
{
    public TextMeshProUGUI TextoFinal;
    public GameObject Reset;

    void Start()
    {
        ControlarUI(false);                         // Start() envia valor FALSO para o método único ControlarUI(bool mostrar).
    }

    public void ResetUI()
    {
        ControlarUI(false);                         // ResetUI() envia valor FALSO para o método único ControlarUI(bool mostrar).
    }

    void OnTriggerEnter()                           
    {
        ControlarUI(true);                          // onTriggerEnter() envia valor VERDADEIRO para o método único ControlarUI(bool mostrar).
    }

    private void ControlarUI(bool mostrar)          // ControlarUI(bool mostrar): método único que vai receber um valor booleano (verdadeiro ou falso) de Start(), ResetUI() ou onTriggerEnter(). 
    {
        TextoFinal.enabled = mostrar;               // A variável mostrar é como um "valor coringa": ela assume o valor que foi passado para o parâmetro do método ControlarUI(bool mostrar).
        Reset.SetActive(mostrar);                   // A variável mostrar é o que vai configurar o estado dos nossos objetos e componentes (no caso, Reset e TextoFinal).
    }
}
 

 */