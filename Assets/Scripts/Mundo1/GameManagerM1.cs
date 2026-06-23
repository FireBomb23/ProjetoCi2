using UnityEngine; // Necessário para o Debug.Log
using UnityEngine.SceneManagement;

public static class GameManagerM1
{
    private static int _rightAnswers = 0;
    private static int _wrongAnswers = 0;

    public static int RightAnswers => _rightAnswers;
    public static int WrongAnswers => _wrongAnswers;

    public static void IncrementWrongAnswer()
    {
        _wrongAnswers++;
    }

    public static void IncrementRightAnswer()
    {
        _rightAnswers++;

        // Isto vai mostrar na consola do Unity quantas peças o jogo acha que tu já encaixaste
        Debug.Log("Peças encaixadas nesta ronda: " + _rightAnswers);

        if (_rightAnswers == 4)
        {
            SceneManager.LoadScene("ScoreScene11");
        }
    }

    public static void Reset()
    {
        _rightAnswers = 0;
        _wrongAnswers = 0;
    }
}