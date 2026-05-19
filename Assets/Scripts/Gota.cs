using UnityEngine;

public class Gota : MonoBehaviour
{
    public enum TipoGota { Agua, Sumo, Refrigerante }
    public TipoGota tipo;
    public float velocidade = 3f;

    void Update()
    {
        transform.Translate(Vector3.down * velocidade * Time.deltaTime);

        // Apaga se saiu do ecrã
        if (transform.position.y < -7f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Copo"))
        {
            GameManager.instancia.ApanarGota(tipo);
            Destroy(gameObject);
        }
    }
}
