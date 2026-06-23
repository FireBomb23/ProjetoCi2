using UnityEngine;
using UnityEngine.SceneManagement; // Isto chama o SceneManager original do Unity

public class MudarCena : MonoBehaviour
{
    // Função para mudar de cena escrevendo o nome dela no botão
    public void CarregarCenaPorNome(string ScoreScene13)
    {
        // Ao usares o UnityEngine. antes, garantes que o Unity usa o gestor oficial
        UnityEngine.SceneManagement.SceneManager.LoadScene(ScoreScene13);
    }
}