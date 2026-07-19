using UnityEngine;
using System.Collections;

public class GhostVisibilidade : MonoBehaviour
{
    public Renderer corpoFantasma;                     // arraste aqui o Renderer do corpo do fantasma
    
    //[Range(0f, 1f)]
    public float alphaInvisivel = 0f;                  // "variável de invisibilidade" = 100% transparente (por padrão)
    public float duracaoFade = 0.5f;                   // duração da transição, em segundos

    private int paredesTocando = 0;                    // contador: quantas paredes o fantasma está tocando agora
    private Coroutine fadeEmAndamento;                 // guarda a coroutine ativa, para poder cancelar se precisar

    private void Start()
    {
        Color cor = corpoFantasma.material.color;
        cor.a = 1f;                                    // começa 100% visível
        corpoFantasma.material.color = cor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parede"))
        {
            paredesTocando++;                          // entrou em contato com mais uma parede
            IniciarFade(alphaInvisivel);               // começa a ficar semi-invisível
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Parede"))
        {
            paredesTocando--;                           // saiu de uma parede
            if (paredesTocando <= 0)                    // só volta a ficar visível quando NÃO estiver tocando mais nenhuma parede
            {
                paredesTocando = 0;                     // proteção extra, evita valor negativo
                IniciarFade(1f);                        // começa a voltar ao normal
            }
        }
    }

    private void IniciarFade(float alphaAlvo)
    {
        if (fadeEmAndamento != null)
            StopCoroutine(fadeEmAndamento);             // cancela um fade anterior, se estiver rolando, para começar o novo do ponto atual

        fadeEmAndamento = StartCoroutine(FadeParaAlpha(alphaAlvo));
    }

    private IEnumerator FadeParaAlpha(float alphaAlvo)
    {
        float tempoDecorrido = 0f;
        float alphaInicial = corpoFantasma.material.color.a;

        while (tempoDecorrido < duracaoFade)
        {
            tempoDecorrido += Time.deltaTime;
            float t = tempoDecorrido / duracaoFade;                      // vai de 0 até 1
            float alphaAtual = Mathf.Lerp(alphaInicial, alphaAlvo, t);   // interpola suavemente entre o valor inicial e o alvo

            Color cor = corpoFantasma.material.color;
            cor.a = alphaAtual;
            corpoFantasma.material.color = cor;

            yield return null;                                           // espera o próximo frame
        }

        // Garante que termine exatamente no valor alvo (evita erro de arredondamento do Lerp)
        Color corFinal = corpoFantasma.material.color;
        corFinal.a = alphaAlvo;
        corpoFantasma.material.color = corFinal;

        /* Explicação sobre a correção de segurança acima: como Time.deltaTime varia a cada frame, o "tempoDecorrido", raramente bate em cima da hora exata de duracaoFade.
         * Então o Lerp pode terminar com um alpha bem próximo do alvo, mas não idêntico. Este trecho força o valor final exato. */
    }
}