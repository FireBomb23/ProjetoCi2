using UnityEngine;

public class HitScript : MonoBehaviour
{
    private bool isFastFood = false;

    public void SetIsFastFood(bool value)
    {
        isFastFood = value;
    }

    // Chamado pelo OnClick do botão "Item" (A comida)
    public void OnHit()
    {
        if (GameManager.instancia == null) return;

        if (isFastFood)
        {
            GameManager.instancia.IncrementWrongAnswer(); // Fast Food -> -1 ponto
        }
        else
        {
            GameManager.instancia.IncrementRightAnswer(); // Fruta -> +1 ponto
        }

        // Esconde especificamente a comida (Item) que está dentro do Spot
        Transform itemTransform = transform.Find("Spot/Item");
        if (itemTransform != null)
        {
            itemTransform.gameObject.SetActive(false); 
        }
        else
        {
            // Segurança: Se o clique vier do botão ativo diretamente, desativa-o
            if (UnityEngine.EventSystems.EventSystem.current != null && 
                UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
            {
                UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.SetActive(false);
            }
        }
    }

    // Chamado pelo OnClick do botão "Spot" (O buraco vazio)
    public void OnMiss()
    {
        if (GameManager.instancia == null) return;

        GameManager.instancia.IncrementWrongAnswer(); // Clicou no buraco vazio -> -1 ponto
    }
}