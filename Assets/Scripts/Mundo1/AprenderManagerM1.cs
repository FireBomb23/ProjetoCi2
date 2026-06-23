using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AprenderManagerM1 : MonoBehaviour
{
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

    // ── Painel Direito – Grupos no Prato ──
    [Header("Painel Direito")]
    [SerializeField] private Text textoContadorGrupos;
    [SerializeField] private Text textoMsgGrupos;

    // ── Data ──
    private readonly string[] factos = {
        "Os alimentos dividem-se\nem <b>grupos</b>! Cada grupo\ndá-nos nutrientes diferentes\ne todos são importantes!",
        "Os <b>cereais</b>\n(pão, massa, arroz)\nfornecem a <b>energia</b>\nque o teu corpo precisa!",
        "Os <b>legumes e frutas</b>\nestão cheios de vitaminas\ne minerais para\ncrescer saudável!",
        "As <b>proteínas</b>\n(carne, peixe, ovos)\nconstroem e reparam\nos músculos do corpo!",
        "Os <b>lacticínios</b>\n(leite, queijo, iogurte)\nfortalecem os <b>ossos</b>\ne os dentes!",
        "Devemos comer de\n<b>todos os grupos</b>\ntodos os dias para ter\numa dieta equilibrada!"
    };

    private readonly string[] perguntas = {
        "Os cereais fornecem energia para o nosso corpo.",
        "Frutas e legumes dão-nos vitaminas e minerais.",
        "A carne e o peixe sao fontes de proteina.",
        "Os lacticinios nao sao importantes para os ossos.",
        "Devemos comer so de um grupo alimentar por dia."
    };
    private readonly bool[] respostasCorretas = { true, true, true, false, false };

    private readonly string[] cardTitulos = {
        "Cereais",
        "Legumes & Frutas",
        "Proteinas",
        "Lacticinios"
    };
    private readonly string[] cardDetalhes = {
        "Pao, massa, arroz, batata e aveia sao cereais que nos dao energia. Devem ser a base da nossa alimentacao! Escolhe versoes integrais para teres mais fibra e sentires-te satisfeito por mais tempo.",
        "Legumes e frutas sao ricos em vitaminas, minerais e fibra. Devemos comer 5 porcoes por dia! Quanto mais coloridos, mais nutrientes diferentes. Come o arco-iris da natureza!",
        "Carne, peixe, ovos e leguminosas sao ricos em proteinas. As proteinas constroem musculos e reparam celulas. Come peixe pelo menos 2 vezes por semana - e muito saudavel!",
        "Leite, queijo e iogurte sao ricos em calcio e vitamina D. Estes nutrientes sao essenciais para ter ossos e dentes fortes. Escolhe versoes com menos gordura para uma opcao mais saudavel!"
    };

    private readonly string[] gruposMsgs = {
        "Clica num grupo para comecar!",
        "1 grupo! Continua!",
        "2 grupos! Muito bem!",
        "3 grupos! Quase la!",
        "PARABENS! Prato completo!"
    };

    private int paginaAtual = 0;
    private int perguntaAtual = 0;
    private int pontuacao = 0;
    private bool aguardarResposta = false;
    private int gruposCount = 0;
    private string detalheAtivo = "";

    void Start()
    {
        MostrarCards();
        AtualizarFacto();
        AtualizarGrupos();
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
    public void IrParaNiveis() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }

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
        if (textoPergunta      != null) textoPergunta.text      = perguntas[perguntaAtual];
        if (textoProgressoQuiz != null) textoProgressoQuiz.text = "Pergunta " + (perguntaAtual+1) + " de " + perguntas.Length;
        if (textoFeedback      != null) textoFeedback.text      = "Le a frase com atencao!";
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

    // ── GRUPOS NO PRATO ──
    public void AdicionarGrupo()
    {
        if (gruposCount >= 4) return;
        gruposCount++;
        AtualizarGrupos();
    }

    public void ResetarGrupos()
    {
        gruposCount = 0;
        AtualizarGrupos();
    }

    private void AtualizarGrupos()
    {
        if (textoContadorGrupos != null) textoContadorGrupos.text = gruposCount + " / 4";
        if (textoMsgGrupos != null && gruposCount < gruposMsgs.Length)
            textoMsgGrupos.text = gruposMsgs[gruposCount];
    }
}
