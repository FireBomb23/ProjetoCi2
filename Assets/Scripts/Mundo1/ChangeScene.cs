using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string GameScene;

    public void ChangeSceneAction()
    {
        SceneManager.LoadScene(GameScene);
    }
}