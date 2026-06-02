using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rightAnswersText;

    [SerializeField]
    private TMP_Text wrongAnswersText;

    public void Start()
    {
        rightAnswersText.text = GameManager.GetRightAnswer().ToString();
        wrongAnswersText.text = GameManager.GetWrongAnswer().ToString();
    }

    public void TestAgain()
    {
        GameManager.Reset();
        SceneManager.LoadScene("main");
    }
}