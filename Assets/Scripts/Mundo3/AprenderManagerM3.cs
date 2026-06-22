using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AprenderManagerM3 : MonoBehaviour
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
        "O nosso corpo e\n<b>60% agua!</b>\nSem agua nao conseguimos\nsobreviver!",
        "Precisamos de beber\n<b>6 a 8 copos</b> de agua\npor dia para pensar melhor!",
        "A agua ajuda a <b>regular\na temperatura</b> do corpo\ne transporta nutrientes!",
        "A <b>agua e sempre\na melhor escolha!</b>\nNatural, saudavel e sem acucar!",
        "Os <b>sumos</b> tem muito acucar!\nOs <b>refrigerantes</b>\nfazem mal aos dentes!",
        "Bebe agua ao <b>acordar</b>,\nnas <b>refeicoes</b> e quando\n<b>praticas desporto!</b>"
    };

    private readonly string[] perguntas = {
        "O nosso corpo e 60% agua.",
        "Devemos beber 6 a 8 copos de agua por dia.",
        "A agua faz mal aos dentes.",
        "Os sumos sao sempre a melhor escolha para beber.",
        "A agua ajuda a transportar nutrientes pelo corpo."
    };
    private readonly bool[] respostasCorretas = { true, true, false, false, true };

    private readonly string[] cardTitulos = {
        "Pensar melhor",
        "Regular a temperatura",
        "Transportar nutrientes",
        "Agua vs. Sumos e Refrigerantes"
    };
    private readonly string[] cardDetalhes = {
        "O nosso cerebro e 75% agua! Quando estas bem hidratado consegues concentrar-te melhor e aprender mais rapido na escola. Antes de um teste bebe um copo de agua!",
        "Quando tens calor ou fazes exercicio o teu corpo sua. O suor arrefece a pele mas perdes agua! Por isso bebe durante e depois do desporto para repor tudo.",
        "O sangue e 90% agua! Ele leva o oxigenio as vitaminas e os minerais a todas as celulas do corpo. Sem agua suficiente sentes-te cansado e sem energia.",
        "Uma caixinha de sumo pode ter ate 5 colheres de acucar! Os refrigerantes corroem o esmalte dos dentes. A agua nao tem acucar nem calorias - e sempre a melhor escolha!"
    };

    private readonly string[] cupMsgs = {
        "Clica num copo para comecar!",
        "Boa! 1 copo! Continua!",
        "2 copos! No bom caminho!",
        "3 copos! O cerebro agradece!",
        "4 copos! A meio do objetivo!",
        "5 copos! Quase la!",
        "6 copos! Chegaste ao minimo!",
        "7 copos! Incrivel! Mais um!",
        "PARABENS! Objetivo atingido!"
    };

    private int paginaAtual = 0;
    private int perguntaAtual = 0;
    private int pontuacao = 0;
    private bool aguardarResposta = false;
    private int coposCount = 0;
    private string detalheAtivo = "";

    void Start()
    {
        MostrarCards();
        AtualizarFacto();
        AtualizarCopos();
        if (painelDetalhe != null) painelDetalhe.SetActive(false);
    }

    // ── ESQUERDO ──
    private void AtualizarFacto()
    {
        if (textoFacto    != null) textoFacto.text    = factos[paginaAtual];
        if (textoIndicador!= null) textoIndicador.text = (paginaAtual + 1) + " / " + factos.Length;
        if (botaoAnterior != null) botaoAnterior.gameObject.SetActive(paginaAtual > 0);
        if (botaoProximo  != null) botaoProximo.gameObject.SetActive(paginaAtual < factos.Length - 1);
    }

    public void ProximoFacto()  { if (paginaAtual < factos.Length - 1) { paginaAtual++; AtualizarFacto(); } }
    public void FactoAnterior() { if (paginaAtual > 0)                 { paginaAtual--; AtualizarFacto(); } }

    // ── CARDS ──
    private void MostrarCards()
    {
        if (painelCards    != null) painelCards.SetActive(true);
        if (painelQuiz     != null) painelQuiz.SetActive(false);
        if (painelResultado!= null) painelResultado.SetActive(false);
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
        if (painelCards    != null) painelCards.SetActive(false);
        if (painelQuiz     != null) painelQuiz.SetActive(true);
        if (painelResultado!= null) painelResultado.SetActive(false);
        AtualizarPergunta();
    }

    private void AtualizarPergunta()
    {
        aguardarResposta = false;
        if (textoPergunta    != null) textoPergunta.text     = perguntas[perguntaAtual];
        if (textoProgressoQuiz!=null) textoProgressoQuiz.text= "Pergunta " + (perguntaAtual+1) + " de " + perguntas.Length;
        if (textoFeedback    != null) textoFeedback.text     = "Le a frase com atencao!";
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
        if (painelCards    != null) painelCards.SetActive(false);
        if (painelQuiz     != null) painelQuiz.SetActive(false);
        if (painelResultado!= null) painelResultado.SetActive(true);
        string msg = pontuacao == perguntas.Length ? "EXCELENTE! Acertaste TUDO!" :
                     pontuacao >= 3 ? "Muito bem! Continua assim!" :
                     "Continua a estudar, tu consegues!";
        if (textoPontuacao != null)
            textoPontuacao.text = "Acertaste " + pontuacao + " de " + perguntas.Length + " perguntas!\n" + msg;
    }

    public void TentarNovamente() { IniciarQuiz(); }
    public void Continuar()       { SceneManager.LoadScene(cenaSeguinte); }

    // ── COPOS ──
    public void AdicionarCopo()
    {
        if (coposCount >= 8) return;
        coposCount++;
        AtualizarCopos();
    }

    public void ResetarCopos()
    {
        coposCount = 0;
        AtualizarCopos();
    }

    private void AtualizarCopos()
    {
        if (textoContadorCopos != null) textoContadorCopos.text = coposCount + " / 8";
        if (textoMsgCopos != null && coposCount < cupMsgs.Length)
            textoMsgCopos.text = cupMsgs[coposCount];
    }
}
