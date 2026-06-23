using UnityEngine.SceneManagement;

public static class GameManagerM1
{
    // Variáveis privadas (guardam os valores reais de acertos e erros)
    private static int _rightAnswers = 0;
    private static int _wrongAnswers = 0;

    // Propriedades Públicas (permitem que outros scripts leiam os valores de forma segura)
    public static int RightAnswers => _rightAnswers;
    public static int WrongAnswers => _wrongAnswers;

    // Função que o DropPiecesM1 chama quando o jogador falha
    public static void IncrementWrongAnswer()
    {
        _wrongAnswers++;
    }

    // Função que o DropPiecesM1 chama quando o jogador acerta
    public static void IncrementRightAnswer()
    {
        _rightAnswers++;

        // Como o teu puzzle é de 4 peças (p1, p2, p3, p4), quando chega a 4 ele avança
        if (_rightAnswers == 4)
        {
            // O Reset() foi RETIRADO daqui. 
            // Agora os pontos vão guardados direitinhos para a Scene Final!
            SceneManager.LoadScene("ScoreScene11");
        }
    }

    // Esta função só deve ser chamada pelo botão "Jogar Novamente" no ecrã final
    public static void Reset()
    {
        _rightAnswers = 0;
        _wrongAnswers = 0;
    }
}