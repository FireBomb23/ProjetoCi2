using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    void Awake() { instancia = this; }

    void Start()
    {
        // PROTEÇÃO: Só atualiza a barra se ela tiver sido arrastada no Inspector
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

        // PROTEÇÃO: Só atualiza o valor se a barra existir
        if (barraAgua != null)
        {
            barraAgua.value = aguaAtual;
        }
    }

    void Ganhar()
    {
        jogoAtivo = false;
        SceneManager.LoadScene("ScoreScene");
    }

    void MostrarMensagem(string msg)
    {
        if (textoMensagem == null) return; // Proteção técnica
        textoMensagem.text = msg;
        CancelInvoke("LimparMensagem");
        Invoke("LimparMensagem", 1.5f);
    }
    void LimparMensagem() { if (textoMensagem != null) textoMensagem.text = ""; }


    // =========================================================================
    // 🍎 JOGO WHACK-A-MOLE - (FRUTA +1 | FAST FOOD -1 | BURACO -1)
    // =========================================================================
    
    private int whackScore = 0; 
    
    public static int pontuacaoFinalGuardada = 0; 
    
    // CORREÇÃO: Variável que vai guardar o nome do nível que o jogador estava a jogar
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
        pontuacaoFinalGuardada = 0;
    }


    public void GuardarPontuacaoAntesDeSair(string nomeDaCenaAtual)
    {
        pontuacaoFinalGuardada = whackScore;
        ultimaCenaJogada = nomeDaCenaAtual;
    }
}