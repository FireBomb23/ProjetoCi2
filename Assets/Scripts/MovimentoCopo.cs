using UnityEngine;

public class MovimentoCopo : MonoBehaviour
{
    // Limites em coordenadas de Canvas (Canvas 1920 -> X de -960 a +960)
    public float limiteEsquerdo = -800f;
    public float limiteDireito = 800f;

    RectTransform rt;
    Canvas canvas;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        Vector2 posCanvas;

        // Converter posicao do rato para coordenadas do Canvas
        if (Input.GetMouseButton(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                canvas.worldCamera,
                out posCanvas
            );
            float x = Mathf.Clamp(posCanvas.x, limiteEsquerdo, limiteDireito);
            rt.anchoredPosition = new Vector2(x, rt.anchoredPosition.y);
        }

        // Suporte a toque (mobile)
        if (Input.touchCount > 0)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                Input.GetTouch(0).position,
                canvas.worldCamera,
                out posCanvas
            );
            float x = Mathf.Clamp(posCanvas.x, limiteEsquerdo, limiteDireito);
            rt.anchoredPosition = new Vector2(x, rt.anchoredPosition.y);
        }
    }
}
