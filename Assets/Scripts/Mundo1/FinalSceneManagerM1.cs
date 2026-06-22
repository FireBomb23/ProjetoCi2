using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManagerM1 : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rightAnswersText;

    [SerializeField]
    private TMP_Text wrongAnswersText;

    public void Start()
    {
<<<<<<< Updated upstream
        rightAnswersText.text = GameManager.GetRightAnswer().ToString();
        wrongAnswersText.text = GameManager.GetWrongAnswer().ToString();
=======
        rightAnswersText.text = GameManagerM1.RightAnswers.ToString();
        wrongAnswersText.text = GameManagerM1.WrongAnswers.ToString();
>>>>>>> Stashed changes
    }

    public void TestAgain()
    {
        GameManager.Reset();
        SceneManager.LoadScene("main");
    }
}