using UnityEngine;

public class LevelManagerM1 : MonoBehaviour
{
    void Awake()
    {
        // Procura todas as peças com o script PuzzlePieceM1 na cena atual
        PuzzlePieceM1[] pieces = FindObjectsByType<PuzzlePieceM1>(FindObjectsSortMode.None);
        
        // Define o objetivo do nível com base no número real de peças encontradas
        GameManagerM1.SetLevelTarget(pieces.Length);
    }
}