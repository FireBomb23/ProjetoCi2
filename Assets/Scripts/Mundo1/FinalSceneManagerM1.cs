using TMPro; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManagerM1 : MonoBehaviour
{
    [SerializeField] private TMP_Text rightAnswersText; 
    [SerializeField] private TMP_Text wrongAnswersText; 

    public void Start()
    {
        rightAnswersText.text = GameManagerM1.RightAnswers.ToString();
        wrongAnswersText.text = GameManagerM1.WrongAnswers.ToString();
    }

    public void TestAgain()
    {
        // Retirámos o Reset daqui. A cena de jogo agora limpa-se a si própria!
        // ATENÇÃO: Garante que "GameScene" é o nome EXATO da tua cena de jogo.
        SceneManager.LoadScene("GameScene11"); 
    }
}