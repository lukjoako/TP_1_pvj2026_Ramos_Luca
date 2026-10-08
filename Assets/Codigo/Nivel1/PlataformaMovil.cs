using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Puntos de Recorrido")]
    public Transform puntoA;
    public Transform puntoB;

    [Header("Ajustes de Movimiento")]
    public float velocidad = 10.0f;
    public float tiempoEspera = 1.5f; // Tiempo que espera usando Invoke()

    private Vector3 objetivoActual;
    private bool estaEsperando = false;

    void Start()
    {
        // Definir el primer punto hacia el cual se moverá
        if (puntoB != null)
        {
            objetivoActual = puntoB.position;
        }
    }

    void Update()
    {
        // Si está esperando la llamada de Invoke(), no se desplaza
        if (estaEsperando) return;

        // Desplazamiento continuo hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);

        // Al llegar exactamente al punto objetivo
        if (Vector3.Distance(transform.position, objetivoActual) < 0.01f)
        {
            PausarYCambiarDireccion();
        }
    }

    void PausarYCambiarDireccion()
    {
        estaEsperando = true;

        // PROGRAMADO CON INVOKE: Ejecuta 'ReanudarMovimiento' tras el tiempo configurado
        Invoke(nameof(ReanudarMovimiento), tiempoEspera);
    }

    void ReanudarMovimiento()
    {
        // Cambiar el objetivo entre punto A y punto B
        if (objetivoActual == puntoA.position)
        {
            objetivoActual = puntoB.position;
        }
        else
        {
            objetivoActual = puntoA.position;
        }

        estaEsperando = false;
    }

    // --- ARRASTRE DEL JUGADOR ---
    private void OnCollisionEnter(Collision collision)
    {
        // Al subir el jugador, vuelve a la plataforma su 'padre' para seguir su movimiento
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Al bajar o saltar de la plataforma, se desvincula
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}