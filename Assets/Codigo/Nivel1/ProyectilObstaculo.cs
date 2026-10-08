using UnityEngine;

public class ProyectilObstaculo : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    public float velocidad = 8.0f;
    public float tiempoVida = 5.0f;

    void Start()
    {
        // Limpieza automática
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar choque con el Player
        if (other.CompareTag("Player"))
        {
            // Buscar el script ControladorVida en el objeto tocado
            ControladorVida sistemaVida = other.GetComponent<ControladorVida>();

            if (sistemaVida != null)
            {
                sistemaVida.RecibirDanio(1);
            }

            // Destruir el proyectil tras el impacto
            Destroy(gameObject);
        }
    }
}