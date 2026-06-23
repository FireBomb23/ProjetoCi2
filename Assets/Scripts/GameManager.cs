using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// CORREÇÃO: Removido o 'static' daqui. A classe é normal, apenas as variáveis de pontuação são estáticas!
public class GameManager : MonoBehaviour
{
    // =========================================================================
    // 💧 JOGO DAS GOTAS - CÓDIGO ORIGINAL COM PROTEÇÕES CONTRA CRASH
    // =========================================================================
    public static GameManager instancia;
    public int aguaAtual = 0;
    public int aguaMaxima = 8;
    public bool jogoAtivo = true;

    public Slider barraAgua;
    public Text textoMensagem;

    void Awake()
{
    if (instancia != null && instancia != this)
    {
        Destroy(gameObject);
        return;
    }
    instancia = this;
    DontDestroyOnLoad(gameObject);

    // ← NOVO: subscreve ao evento de cena carregada
    SceneManager.sceneLoaded += OnCenaCarregada;
}

// ← NOVO: chamado automaticamente quando qualquer cena carrega
void OnCenaCarregada(Scene scene, LoadSceneMode mode)
{
    // Só reseta se for uma cena de jogo (não a ScoreScene)
    if (scene.name.Contains("ScoreScene")) return;

    jogoAtivo = true;
    aguaAtual = 0;

    // Re-encontra os objetos UI na nova cena
    barraAgua = FindFirstObjectByType<Slider>();
    textoMensagem = FindFirstObjectByType<Text>();

    if (barraAgua != null)
    {
        barraAgua.maxValue = aguaMaxima;
        barraAgua.value = 0;
    }
}

// ← NOVO: evita memory leak
void OnDestroy()
{
    SceneManager.sceneLoaded -= OnCenaCarregada;
}

    void Start()
    {
        if (barraAgua != null)
        {
            barraAgua.maxValue = aguaMaxima;
            barraAgua.value = 0;
        }
    }

    public void ApanarGota(Gota.TipoGota tipo)
    {
        if (!jogoAtivo) return;

        if (tipo == Gota.TipoGota.Agua)
        {
            aguaAtual = Mathf.Min(aguaAtual + 1, aguaMaxima);
            MostrarMensagem("+1 Agua! Muito bem!");
            if (aguaAtual >= aguaMaxima) Ganhar();
        }
        else
        {
            aguaAtual = Mathf.Max(aguaAtual - 1, 0);
            string nome = tipo == Gota.TipoGota.Sumo ? "Sumo" : "Refrigerante";
            MostrarMensagem("Cuidado! " + nome + " faz mal!");
        }

        if (barraAgua != null)
            barraAgua.value = aguaAtual;
    }

   void Ganhar()
{
    jogoAtivo = false;
    GuardarPontuacaoAntesDeSair(SceneManager.GetActiveScene().name);
    SceneManager.LoadScene("ScoreScene1"); // ← nome exato da tua cena
}

    void MostrarMensagem(string msg)
    {
        if (textoMensagem == null) return;
        textoMensagem.text = msg;
        CancelInvoke("LimparMensagem");
        Invoke("LimparMensagem", 1.5f);
    }
    void LimparMensagem() { if (textoMensagem != null) textoMensagem.text = ""; }


    // =========================================================================
    // 🍎 JOGO WHACK-A-MOLE (FRUTAS) - SUPORTE DINÂMICO NÍVEIS 2.1, 2.2 E 2.3
    // =========================================================================
    
    private int whackScore = 0; 
    
    public static int pontuacaoFinalGuardada = 0; 
    
    // Suporta qualquer nível dinamicamente (2.1, 2.2, 2.3, etc.)
    public static string ultimaCenaJogada = "GameScene2.1"; 

    public void IncrementRightAnswer()
    {
        whackScore += 1;
    }

    public void IncrementWrongAnswer()
    {
        whackScore -= 1;
    }

    public int GetScore()
    {
        return whackScore;
    }

    public void ResetWhackScore()
    {
        whackScore = 0;
    }

    public void GuardarPontuacaoAntesDeSair(string nomeDaCenaAtual)
    {
        pontuacaoFinalGuardada = whackScore;
        ultimaCenaJogada = nomeDaCenaAtual; 
    }
}