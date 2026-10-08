using UnityEngine;

public class SpawnerObstaculos : MonoBehaviour
{
    [Header("Prefab a Generar")]
    public GameObject prefabObstaculo;

    [Header("Configuración de Tiempo")]
    public float tiempoInicial = 1.0f;  // Espera antes de lanzar el primero
    public float intervalo = 2.5f;     // Tiempo entre cada generación

    void Start()
    {
        // GENERACIÓN PERIÓDICA CON INVOKEREPEATING:
        // Llama a 'GenerarProyectil' por primera vez en 'tiempoInicial' segundos
        // y luego lo repite automáticamente cada 'intervalo' segundos.
        InvokeRepeating(nameof(GenerarProyectil), tiempoInicial, intervalo);
    }

    void GenerarProyectil()
    {
        if (prefabObstaculo != null)
        {
            // Instancia el Prefab en la posición y rotación del Spawner
            Instantiate(prefabObstaculo, transform.position, transform.rotation);
        }
    }
}