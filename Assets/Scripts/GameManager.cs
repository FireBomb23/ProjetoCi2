using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int aguaAtual = 0;
    public int aguaMaxima = 8;
    public bool jogoAtivo = true;

    public Slider barraAgua;
    public TMP_Text textoMensagem;
    public GameObject painelVitoria;
    public GameObject painelDerrota;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        barraAgua.maxValue = aguaMaxima;
        barraAgua.value = 0;
        painelVitoria.SetActive(false);
        painelDerrota.SetActive(false);
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

        barraAgua.value = aguaAtual;
    }

    void Ganhar()
    {
        jogoAtivo = false;
        painelVitoria.SetActive(true);
    }

    void MostrarMensagem(string msg)
    {
        textoMensagem.text = msg;
        CancelInvoke("LimparMensagem");
        Invoke("LimparMensagem", 1.5f);
    }

    void LimparMensagem() { textoMensagem.text = ""; }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
