using TMPro; // Necessário para controlar os textos TextMesh Pro
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManagerM1 : MonoBehaviour
{
    [SerializeField] private TMP_Text rightAnswersText; // Arrastamos o TextoAcertos para aqui
    [SerializeField] private TMP_Text wrongAnswersText; // Arrastamos o TextoErros para aqui

    public void Start()
    {
        // Vai buscar o valor de acertos ao GameManagerM1 e escreve no ecrã
        rightAnswersText.text = GameManagerM1.RightAnswers.ToString();
        
        // Vai buscar o valor de erros ao GameManagerM1 e escreve no ecrã
        wrongAnswersText.text = GameManagerM1.WrongAnswers.ToString();
    }

    // Função para o botão de "Jogar Novamente"
    public void TestAgain()
    {
        // Limpa os pontos antigos (põe a 0) antes de começar o novo jogo
        GameManagerM1.Reset();
        
        // Carrega a cena do jogo (ajusta para o nome exato da tua cena de jogo)
        SceneManager.LoadScene("GameScene11"); 
    }
}