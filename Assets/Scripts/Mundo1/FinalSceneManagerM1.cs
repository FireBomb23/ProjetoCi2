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
        rightAnswersText.text = GameManagerM1.RightAnswers.ToString();
        wrongAnswersText.text = GameManagerM1.WrongAnswers.ToString();
        rightAnswersText.text = GameManagerM1.GetRightAnswer().ToString();
        wrongAnswersText.text = GameManagerM1.GetWrongAnswer().ToString();
    }

    public void TestAgain()
    {
        GameManagerM1.Reset();
        SceneManager.LoadScene("main");
    }
}