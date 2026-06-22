using UnityEngine;

public class SpawnGotas : MonoBehaviour
{
    public GameObject prefabAgua;
    public GameObject prefabSumo;
    public GameObject prefabRefrigerante;

    public float intervalo = 1.2f;
    // Canvas 1920x1080: X vai de -960 a +960, Y de -540 a +540
    public float posYSpawn = 620f;   // acima do topo do Canvas
    public float xMin = -900f;  // margem lateral esquerda
    public float xMax = 900f;  // margem lateral direita

    // Referencia ao RectTransform do Canvas pai
    RectTransform canvasRect;
    float timer = 0f;

    void Start()
    {
        // Encontrar o Canvas pai automaticamente
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!GameManager.instancia.jogoAtivo) return;

        timer += Time.deltaTime;
        if (timer >= intervalo)
        {
            timer = 0f;
            SpawnarGota();
        }
    }

    void SpawnarGota()
    {
        // Posicao aleatoria em X dentro do Canvas
        float x = Random.Range(xMin, xMax);

        // Escolher tipo de gota: 50% agua, 25% sumo, 25% refrigerante
        float r = Random.value;
        GameObject prefab;
        if (r < 0.50f) prefab = prefabAgua;
        else if (r < 0.75f) prefab = prefabSumo;
        else prefab = prefabRefrigerante;

        // Instanciar como filho do Canvas
        GameObject g = Instantiate(prefab, canvasRect);

        // Posicionar em coordenadas de Canvas (anchoredPosition)
        RectTransform grt = g.GetComponent<RectTransform>();
        grt.anchoredPosition = new Vector2(x, posYSpawn);

        // Velocidade aleatoria (pixeis/segundo)
        g.GetComponent<Gota>().velocidade = Random.Range(300f, 550f);
    }
}
