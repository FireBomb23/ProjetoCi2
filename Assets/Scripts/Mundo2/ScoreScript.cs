using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField; 

    void Update()
    {
        if (GameManager.instancia != null && textField != null)
        {
            textField.text = GameManager.instancia.GetScore().ToString();
        }
    }
}