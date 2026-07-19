using UnityEngine;
using TMPro;
public class Chegada : MonoBehaviour
{    
    public TextMeshProUGUI textoTitulo;
    public GameObject ResetButton;
    public GameObject NextLevelButton;              // opcional: se vazio no Inspector, é ignorado em todo o script    

    void Start()                                    // faz a primeira configuração inicial dos objetos e componentes
    {
        GhostController.jogoVencido = false;        // toda fase nova começa com os fantasmas perseguindo normalmente (essa linha de código deve se repetir no método ResetUI também)

        /* jogoVencido é static, e CAMPOS ESTÁTICOS NÃO SÃO RESETADOS AUTOMATICAMENTE QUANDO SE TROCA DE CENA com SceneManager.LoadScene().
         * Eles só resetam quando o Editor sai do Play Mode (domain reload) ou quando o processo é reiniciado.
         * Então, quando o player vence a Fase 1, jogoVencido vira true. Mas, ao carregar a Fase 2, essa flag continua true, porque carregar uma nova cena não zera variáveis estáticas.
         * Resultado: os fantasmas da Fase 2 já nascem "congelados" — o if (jogoVencido) return; já corta o movimento deles desde o início, mesmo sem o player ter tocado o portal daquela fase ainda.
         * Por isso é adicionada a reinicialização da flag no Start() do Chegada.cs (o que garante que toda vez que uma fase carrega do zero, os fantasmas daquela fase começam em movimento).*/



        textoTitulo.enabled = false;                // no início, o componente textoTitulo tem que ficar escondido 
        ResetButton.SetActive(false);               // no início, o objeto ResetButton inteiro não deve estar ativo na cena

        if (NextLevelButton != null)                // só executa se o objeto existir no Inspector
        {
            NextLevelButton.SetActive(false);       // no início (caso exista), o objeto NextLevelButton inteiro não deve estar ativo na cena 
        }                                                                                               
    }
    private void OnTriggerEnter(Collider other)     // quando alguém atinge a Chegada, esse evento dispara
    {
        if (!other.CompareTag("Player")) return;    // se esse alguém NÃO for o Player, ignora e sai do método. Mas se for o player, as próximas linhas desse método são executadas.

        GhostController.jogoVencido = true;         // avisa TODOS os fantasmas (prefabs) pra pararem de perseguir

        textoTitulo.enabled = true;                 // player na Chegada vê mensagem de fim/vitória na tela
        ResetButton.SetActive(true);                // player na Chegada vê objeto ResetButton na tela (que faz o player retornar ao início do labirinto)

        if (NextLevelButton != null)                // só executa se o objeto existir no Inspector
            NextLevelButton.SetActive(true);
    }
    public void ResetUI()                           // controla a visibilidade do objeto Reset  
    {
        GhostController.jogoVencido = false;        // reset da fase também libera a movimentação dos fantasmas de novo (ver explicação no método Start).

        textoTitulo.enabled = false;                // botão ResetButton clicado significa: remova mensagem de fim/vitória na tela
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