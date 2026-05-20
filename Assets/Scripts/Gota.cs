using UnityEngine;

public class Gota : MonoBehaviour
{
    public enum TipoGota { Agua, Sumo, Refrigerante }
    public TipoGota tipo;
    public float velocidade = 400f;   // pixeis/segundo em Canvas Space

    RectTransform rt;
    RectTransform copRT;  // RectTransform do Copo

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Encontrar o Copo pelo nome (ou usa FindWithTag se preferires)
        GameObject copo = GameObject.Find("Copo");
        if (copo != null)
            copRT = copo.GetComponent<RectTransform>();
    }

    void Update()
    {
        // 1. Mover para baixo em coordenadas de Canvas
        rt.anchoredPosition += Vector2.down * velocidade * Time.deltaTime;

        // 2. Apagar se saiu do fundo do ecra
        if (rt.anchoredPosition.y < -620f)
        {
            Destroy(gameObject);
            return;
        }

        // 3. Detetar colisao com o Copo usando Overlaps()
        if (copRT != null && VerificarColisao())
        {
            GameManager.instancia.ApanarGota(tipo);
            Destroy(gameObject);  // gota desaparece ao tocar no copo
        }
    }

    // Verifica se este RectTransform se sobrepoe ao do Copo
    bool VerificarColisao()
    {
        // Obter os cantos de cada RectTransform em World Space
        Vector3[] gotaCantos = new Vector3[4];
        Vector3[] copoCantos = new Vector3[4];
        rt.GetWorldCorners(gotaCantos);
        copRT.GetWorldCorners(copoCantos);

        // Criar Rects a partir dos cantos
        Rect gotaRect = new Rect(
            gotaCantos[0].x, gotaCantos[0].y,
            gotaCantos[2].x - gotaCantos[0].x,
            gotaCantos[2].y - gotaCantos[0].y
        );
        Rect copoRect = new Rect(
            copoCantos[0].x, copoCantos[0].y,
            copoCantos[2].x - copoCantos[0].x,
            copoCantos[2].y - copoCantos[0].y
        );

        return gotaRect.Overlaps(copoRect);
    }
}
