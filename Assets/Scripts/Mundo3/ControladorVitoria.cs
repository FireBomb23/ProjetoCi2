using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorVitoria : MonoBehaviour
{
    // Chama este m�todo no OnClick() do BotaoTentarNovamente
    public void TentarNovamente()
    {
        SceneManager.LoadScene("LevelSelectScene3");
    }
}
