using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text answersText; // O Teu "Text (TMP)" que está no Inspector

    public void Start()
    {
        // Lê diretamente o valor estático ultra seguro
        if (answersText != null)
        {
            answersText.text = GameManager.pontuacaoFinalGuardada.ToString();
        }
    }

    public void TestAgain()
    {
        // Se encontrar o gestor ativo, faz reset aos scores
        GameManager manager = Object.FindFirstObjectByType<GameManager>();
        if (manager != null)
        {
            manager.ResetWhackScore();
        }
        else
        {
            GameManager.pontuacaoFinalGuardada = 0;
        }
        
        SceneManager.LoadScene("GameScene2"); // Recarrega o teu jogo
    }
}