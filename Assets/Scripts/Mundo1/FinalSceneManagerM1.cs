using TMPro; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManagerM1 : MonoBehaviour
{
    [SerializeField] private TMP_Text rightAnswersText; 
    [SerializeField] private TMP_Text wrongAnswersText; 

    public void Start()
    {
        rightAnswersText.text =  GameManagerM1.RightAnswers.ToString();
        wrongAnswersText.text =  GameManagerM1.WrongAnswers.ToString();
    }

    public void TestAgain()
    {
        // 1. Limpa os pontos antigos para a nova tentativa começar do zero
        GameManagerM1.Reset();
        
        // 2. Carrega automaticamente o nível guardado na memória
        SceneManager.LoadScene(GameManagerM1.LastPlayedScene); 
    }
}