using System.Collections;
using UnityEngine;

public class PowerUpVelocidad : MonoBehaviour
{
    [Header("Tiempos del Power-Up")]
    public float duracionEfecto = 5.0f;   // Tiempo que dura el aumento de velocidad
    public float tiempoCooldown = 3.0f;  // Tiempo de espera para que vuelva a aparecer

    [Header("Efecto de Rotación")]
    public float velocidadRotacion = 100f;

    private MeshRenderer meshRenderer;
    private Collider col;
    private bool disponible = true;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        // Gira solo si está disponible para recoger
        if (disponible)
        {
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disponible && other.CompareTag("Player"))
        {
            EfectosJugador efectos = other.GetComponent<EfectosJugador>();

            if (efectos != null && efectos.PuedoActivarPowerUp())
            {
                // Activa el efecto en el jugador
                efectos.ActivarVelocidad(duracionEfecto, tiempoCooldown);

                // Inicia el ciclo de ocultar y reaparecer el ítem
                StartCoroutine(DesaparecerYReaparecer());
            }
        }
    }

    private IEnumerator DesaparecerYReaparecer()
    {
        disponible = false;

        // Ocultar visualmente y desactivar la colisión
        if (meshRenderer != null) meshRenderer.enabled = false;
        if (col != null) col.enabled = false;

        // Esperar el tiempo total (Duración del efecto + Cooldown)
        float tiempoEsperaTotal = duracionEfecto + tiempoCooldown;
        yield return new WaitForSeconds(tiempoEsperaTotal);

        // Reaparecer el ítem
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (col != null) col.enabled = true;
        disponible = true;

        Debug.Log("¡El Power-Up ha reaparecido en el mapa!");
    }
}