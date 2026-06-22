using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AprenderManagerM2 : MonoBehaviour
{
    [Header("Navegacao")]
    [SerializeField] private string cenaSeguinte = "LevelSelectScene";

    // ── Painel Esquerdo – Curiosidades ──
    [Header("Painel Esquerdo")]
    [SerializeField] private Text textoFacto;
    [SerializeField] private Text textoIndicador;
    [SerializeField] private Button botaoProximo;
    [SerializeField] private Button botaoAnterior;

    // ── Centro – Cards ──
    [Header("Centro Cards")]
    [SerializeField] private GameObject painelCards;
    [SerializeField] private GameObject painelDetalhe;
    [SerializeField] private Text tituloDetalhe;
    [SerializeField] private Text textoDetalhe;

    // ── Centro – Quiz ──
    [Header("Centro Quiz")]
    [SerializeField] private GameObject painelQuiz;
    [SerializeField] private Text textoProgressoQuiz;
    [SerializeField] private Text textoPergunta;
    [SerializeField] private Text textoFeedback;
    [SerializeField] private Button botaoV;
    [SerializeField] private Button botaoF;

    // ── Centro – Resultado ──
    [Header("Centro Resultado")]
    [SerializeField] private GameObject painelResultado;
    [SerializeField] private Text textoPontuacao;

    // ── Painel Direito – Copos ──
    [Header("Painel Direito")]
    [SerializeField] private Text textoContadorCopos;
    [SerializeField] private Text textoMsgCopos;

    // ── Data ──
    private readonly string[] factos = {
        "As frutas e legumes dao-nos\n<b>vitaminas e minerais</b>\nessenciais para crescer\nsaudavel!",
        "A <b>vitamina C</b> da laranja\ne dos morangos\nprotege-nos de constipacoes\ne reforca as defesas!",
        "O <b>ferro</b> dos espinafres\ne do brocolo ajuda o\nsangue a transportar\noxigenio pelo corpo!",
        "O <b>calcio</b> do brocolo\ne de outros legumes\nfortalece os <b>ossos</b>\ne os dentes!",
        "Devemos comer <b>5 porcoes</b>\nde frutas e legumes\npor dia —\numa de cada cor!",
        "Cada <b>cor</b> de fruta e legume\ntem vitaminas diferentes\n— come o <b>arco-iris</b>\nda natureza!"
    };

    private readonly string[] perguntas = {
        "Devemos comer 5 porcoes de frutas e legumes por dia.",
        "A vitamina C ajuda a proteger-nos de constipacoes.",
        "O brocolo nao tem calcio.",
        "Os legumes fazem mal ao crescimento.",
        "Cada cor de fruta ou legume tem vitaminas diferentes."
    };
    private readonly bool[] respostasCorretas = { true, true, false, false, true };

    private readonly string[] cardTitulos = {
        "Vitamina C",
        "Ferro",
        "Calcio",
        "5 Porcoes por Dia"
    };
    private readonly string[] cardDetalhes = {
        "A vitamina C encontra-se na laranja, nos morangos, no kiwi e no pimento. Ela ajuda o teu sistema imunitario a combater virus e bacterias. Por isso, quando comes fruta, estas a proteger o teu corpo!",
        "O ferro e um mineral super importante — esta nos espinafres, no brocolo e nas leguminosas. O ferro ajuda os globulos vermelhos a transportar oxigenio para todos os musculos. Sem ferro ficamos cansados e palidos!",
        "O calcio nao esta so no leite! O brocolo, os feijoes verdes e as amendoas tambem tem calcio. Este mineral e essencial para ter ossos e dentes fortes. Come legumes verdes para cresceres saudavel!",
        "Os nutricionistas recomendam 5 porcoes de frutas e legumes por dia. Uma porcao e, por exemplo, uma peca de fruta, um punhado de legumes ou um copo de sumo natural. Varia as cores para ter todas as vitaminas!"
    };

    private readonly string[] porcoesMsgs = {
        "Come uma fruta para comecar!",
        "Otimo! 1 porcao! Continua!",
        "2 porcoes! Muito bem!",
        "3 porcoes! O teu corpo agradece!",
        "4 porcoes! Quase la!",
        "PARABENS! 5 porcoes atingidas!"
    };

    private int paginaAtual = 0;
    private int perguntaAtual = 0;
    private int pontuacao = 0;
    private bool aguardarResposta = false;
    private int porcoesCount = 0;
    private string detalheAtivo = "";

    void Start()
    {
        MostrarCards();
        AtualizarFacto();
        AtualizarPorcoes();
        if (painelDetalhe != null) painelDetalhe.SetActive(false);
    }

    // ── ESQUERDO ──
    private void AtualizarFacto()
    {
        if (textoFacto     != null) textoFacto.text     = factos[paginaAtual];
        if (textoIndicador != null) textoIndicador.text  = (paginaAtual + 1) + " / " + factos.Length;
        if (botaoAnterior  != null) botaoAnterior.gameObject.SetActive(paginaAtual > 0);
        if (botaoProximo   != null) botaoProximo.gameObject.SetActive(paginaAtual < factos.Length - 1);
    }

    public void ProximoFacto()  { if (paginaAtual < factos.Length - 1) { paginaAtual++; AtualizarFacto(); } }
    public void FactoAnterior() { if (paginaAtual > 0)                  { paginaAtual--; AtualizarFacto(); } }

    // ── CARDS ──
    private void MostrarCards()
    {
        if (painelCards     != null) painelCards.SetActive(true);
        if (painelQuiz      != null) painelQuiz.SetActive(false);
        if (painelResultado != null) painelResultado.SetActive(false);
    }

    public void MostrarDetalhe0() { MostrarDetalhe(0); }
    public void MostrarDetalhe1() { MostrarDetalhe(1); }
    public void MostrarDetalhe2() { MostrarDetalhe(2); }
    public void MostrarDetalhe3() { MostrarDetalhe(3); }

    private void MostrarDetalhe(int i)
    {
        if (painelDetalhe == null) return;
        bool jaMostrado = painelDetalhe.activeSelf && detalheAtivo == cardTitulos[i];
        if (jaMostrado) { FecharDetalhe(); return; }
        detalheAtivo = cardTitulos[i];
        if (tituloDetalhe != null) tituloDetalhe.text = cardTitulos[i];
        if (textoDetalhe  != null) textoDetalhe.text  = cardDetalhes[i];
        painelDetalhe.SetActive(true);
    }

    public void FecharDetalhe()
    {
        detalheAtivo = "";
        if (painelDetalhe != null) painelDetalhe.SetActive(false);
    }

    public void JogarQuiz()    { IniciarQuiz(); }
    public void IrParaNiveis() { SceneManager.LoadScene(cenaSeguinte); }

    // ── QUIZ ──
    private void IniciarQuiz()
    {
        perguntaAtual = 0; pontuacao = 0;
        FecharDetalhe();
        if (painelCards     != null) painelCards.SetActive(false);
        if (painelQuiz      != null) painelQuiz.SetActive(true);
        if (painelResultado != null) painelResultado.SetActive(false);
        AtualizarPergunta();
    }

    private void AtualizarPergunta()
    {
        aguardarResposta = false;
        if (textoPergunta     != null) textoPergunta.text      = perguntas[perguntaAtual];
        if (textoProgressoQuiz!= null) textoProgressoQuiz.text = "Pergunta " + (perguntaAtual+1) + " de " + perguntas.Length;
        if (textoFeedback     != null) textoFeedback.text      = "Le a frase com atencao!";
        if (botaoV != null) botaoV.interactable = true;
        if (botaoF != null) botaoF.interactable = true;
    }

    public void ResponderVerdadeiro() { if (!aguardarResposta) VerificarResposta(true); }
    public void ResponderFalso()      { if (!aguardarResposta) VerificarResposta(false); }

    private void VerificarResposta(bool resposta)
    {
        aguardarResposta = true;
        if (botaoV != null) botaoV.interactable = false;
        if (botaoF != null) botaoF.interactable = false;
        if (resposta == respostasCorretas[perguntaAtual])
        {
            pontuacao++;
            if (textoFeedback != null) textoFeedback.text = "Correto! Muito bem!";
        }
        else
        {
            bool certa = respostasCorretas[perguntaAtual];
            if (textoFeedback != null) textoFeedback.text = "Ops! Era " + (certa ? "VERDADEIRO" : "FALSO") + "!";
        }
        StartCoroutine(EsperarEProgredir());
    }

    private IEnumerator EsperarEProgredir()
    {
        yield return new WaitForSeconds(2f);
        perguntaAtual++;
        if (perguntaAtual < perguntas.Length) AtualizarPergunta();
        else MostrarResultado();
    }

    private void MostrarResultado()
    {
        if (painelCards     != null) painelCards.SetActive(false);
        if (painelQuiz      != null) painelQuiz.SetActive(false);
        if (painelResultado != null) painelResultado.SetActive(true);
        string msg = pontuacao == perguntas.Length ? "EXCELENTE! Acertaste TUDO!" :
                     pontuacao >= 3 ? "Muito bem! Continua assim!" :
                     "Continua a estudar, tu consegues!";
        if (textoPontuacao != null)
            textoPontuacao.text = "Acertaste " + pontuacao + " de " + perguntas.Length + " perguntas!\n" + msg;
    }

    public void TentarNovamente() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void Continuar()       { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); }

    // ── PORCOES ──
    public void AdicionarPorcao()
    {
        if (porcoesCount >= 5) return;
        porcoesCount++;
        AtualizarPorcoes();
    }

    public void ResetarPorcoes()
    {
        porcoesCount = 0;
        AtualizarPorcoes();
    }

    private void AtualizarPorcoes()
    {
        if (textoContadorCopos != null) textoContadorCopos.text = porcoesCount + " / 5";
        if (textoMsgCopos != null && porcoesCount < porcoesMsgs.Length)
            textoMsgCopos.text = porcoesMsgs[porcoesCount];
    }
}
