using UnityEngine.SceneManagement;

public static class GameManagerM1
{
    // Variáveis privadas (o underline no início é uma boa prática para campos privados)
    private static int _rightAnswers = 0;
    private static int _wrongAnswers = 0;

    // Propriedades Públicas (substituem os métodos Get com uma sintaxe mais limpa)
    public static int RightAnswers => _rightAnswers;
    public static int WrongAnswers => _wrongAnswers;

    public static void IncrementWrongAnswer()
    {
        _wrongAnswers++;
    }

    public static void IncrementRightAnswer()
    {
        _rightAnswers++;

        if (_rightAnswers == 4)
        {
            // Resetamos os valores ANTES de mudar de cena para o próximo jogo começar limpo
            Reset(); 
            SceneManager.LoadScene("FinalScene");
        }
    }

    public static void Reset()
    {
        _rightAnswers = 0;
        _wrongAnswers = 0;
    }
}