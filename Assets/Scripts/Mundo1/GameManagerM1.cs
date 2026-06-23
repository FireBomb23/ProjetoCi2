using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManagerM1
{
    private static int _rightAnswers = 0;
    private static int _wrongAnswers = 0;
    private static int _targetAnswers = 0; 
    private static string _lastPlayedScene = "Nivel 1"; // Guarda o nome do último nível jogado

    public static int RightAnswers => _rightAnswers;
    public static int WrongAnswers => _wrongAnswers;
    public static string LastPlayedScene => _lastPlayedScene; // Permite ao ecrã final ler o nome da cena

    public static void SetLevelTarget(int totalPieces)
    {
        _targetAnswers = totalPieces;
        _rightAnswers = 0; 
    }

    public static void IncrementWrongAnswer()
    {
        _wrongAnswers++;
    }

    public static void IncrementRightAnswer()
    {
        _rightAnswers++;

        if (_rightAnswers == _targetAnswers)
        {
            // ANTES de mudar de cena, guarda o nome do nível que acabou de ser concluído
            _lastPlayedScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("ScoreScene11");
        }
    }

    public static void Reset()
    {
        _rightAnswers = 0;
        _wrongAnswers = 0;
    }
}