using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public int aguaAtual = 0;
    public int aguaMaxima = 8;
    public bool jogoAtivo = true;

    public Slider barraAgua;
    public Text textoMensagem;
    // REMOVIDO: painelVitoria e painelDerrota

    void Awake() { instancia = this; }

    void Start()
    {
        barraAgua.maxValue = aguaMaxima;
        barraAgua.value = 0;
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
            // SEM derrota — apenas perde progresso
        }
        barraAgua.value = aguaAtual;
    }

    void Ganhar()
    {
        jogoAtivo = false;
        // Carrega a cena de vitória separada
        SceneManager.LoadScene("ScoreScene");
    }

    void MostrarMensagem(string msg)
    {
        textoMensagem.text = msg;
        CancelInvoke("LimparMensagem");
        Invoke("LimparMensagem", 1.5f);
    }
    void LimparMensagem() { textoMensagem.text = ""; }
}
