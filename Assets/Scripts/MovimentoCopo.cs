using UnityEngine;

public class MovimentoCopo : MonoBehaviour
{
    public float velocidade = 10f;
    public float limitoEsquerdo = -3.5f;
    public float limitoDireito = 3.5f;

    void Update()
    {
        float posX = 0;

        // Controlo por rato
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posX = pos.x;
        }

        // Controlo por toque (telemóvel)
        if (Input.touchCount > 0)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            posX = pos.x;
        }

        float x = Mathf.Clamp(posX, limitoEsquerdo, limitoDireito);
        transform.position = new Vector3(x, transform.position.y, 0);
    }
}
