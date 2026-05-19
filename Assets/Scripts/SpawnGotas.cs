using UnityEngine;

public class SpawnGotas : MonoBehaviour
{
    public GameObject prefabAgua;
    public GameObject prefabSumo;
    public GameObject prefabRefrigerante;

    public float intervalo = 1.2f;
    public float posYSpawn = 6f;
    float timer = 0;

    void Update()
    {
        if (!GameManager.instancia.jogoAtivo) return;

        timer += Time.deltaTime;
        if (timer >= intervalo)
        {
            timer = 0;
            SpawnarGota();
        }
    }

    void SpawnarGota()
    {
        float x = Random.Range(-3f, 3f);
        Vector3 pos = new Vector3(x, posYSpawn, 0);

        // 50% água, 25% sumo, 25% refrigerante
        float r = Random.value;
        GameObject prefab;
        if (r < 0.5f) prefab = prefabAgua;
        else if (r < 0.75f) prefab = prefabSumo;
        else prefab = prefabRefrigerante;

        GameObject g = Instantiate(prefab, pos, Quaternion.identity);

        // Velocidade aleatória
        g.GetComponent<Gota>().velocidade = Random.Range(2.5f, 4.5f);
    }
}
