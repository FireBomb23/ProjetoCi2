using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneM1 : MonoBehaviour
{
    public string GameScene;

    public void ChangeSceneAction()
    {
        SceneManager.LoadScene(GameScene);
    }
}