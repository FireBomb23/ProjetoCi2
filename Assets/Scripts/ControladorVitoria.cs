using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorVitoria : MonoBehaviour
{
    // Chama este método no OnClick() do BotaoTentarNovamente
    public void TentarNovamente()
    {
        // Carrega a cena principal pelo nome
        SceneManager.LoadScene("GameScene");
    }
}
