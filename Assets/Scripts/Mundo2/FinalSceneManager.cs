using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text answersText; 

    public void Start()
    {
        if (answersText != null)
        {
            answersText.text = GameManager.pontuacaoFinalGuardada.ToString();
        }
    }

    public void TestAgain()
    {
        GameManager manager = Object.FindFirstObjectByType<GameManager>();
        if (manager != null)
        {
            manager.ResetWhackScore();
        }
        else
        {
            GameManager.pontuacaoFinalGuardada = 0;
        }
        
        // Devolve o jogador dinamicamente para a cena de onde veio (2.1, 2.2 ou 2.3)
        SceneManager.LoadScene(GameManager.ultimaCenaJogada); 
    }
}