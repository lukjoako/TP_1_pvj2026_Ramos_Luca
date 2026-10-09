using System.Collections;
using UnityEngine;

public class EfectosJugador : MonoBehaviour
{
    [Header("Multiplicador")]
    public float multiplicadorVelocidad = 2.0f; // Multiplica por 2 la velocidad actual

    [Header("Indicador Visual")]
    public Renderer playerRenderer;
    public Color colorPowerUp = Color.yellow;
    private Color colorOriginal;

    private bool powerUpActivo = false;
    private bool enCooldown = false;

    // Referencia al controlador de movimiento
    // NOTA: Si tu script de movimiento tiene otro nombre, cámbialo aquí
    private PlayerController controladorMovimiento; 

    void Start()
    {
        if (playerRenderer != null)
        {
            colorOriginal = playerRenderer.material.color;
        }

        // Obtener el script de movimiento adjunto al personaje
        controladorMovimiento = GetComponent<PlayerController>();
    }

    public bool PuedoActivarPowerUp()
    {
        return !powerUpActivo && !enCooldown;
    }

    public void ActivarVelocidad(float duracion, float tiempoCooldown)
    {
        if (PuedoActivarPowerUp())
        {
            StartCoroutine(CorrutinaPowerUpVelocidad(duracion, tiempoCooldown));
        }
    }

    private IEnumerator CorrutinaPowerUpVelocidad(float duracion, float tiempoCooldown)
    {
        powerUpActivo = true;
        Debug.Log("¡Power-Up Activado! Velocidad aumentada.");

        // 1. AUMENTAR VELOCIDAD REAL
        if (controladorMovimiento != null)
        {
            // Reemplaza 'velocidad' por el nombre exacto de la variable en tu script de movimiento
            controladorMovimiento.speed *= multiplicadorVelocidad; 
        }

        // Cambio visual
        if (playerRenderer != null)
        {
            playerRenderer.material.color = colorPowerUp;
        }

        // 2. Esperar la duración del efecto
        yield return new WaitForSeconds(duracion);

        // 3. RESTABLECER VELOCIDAD ORIGINAL
        if (controladorMovimiento != null)
        {
            controladorMovimiento.speed /= multiplicadorVelocidad;
        }

        if (playerRenderer != null)
        {
            playerRenderer.material.color = colorOriginal;
        }

        powerUpActivo = false;
        enCooldown = true;
        Debug.Log("Power-Up finalizado. Entrando en Cooldown...");

        // 4. Esperar el tiempo de recarga
        yield return new WaitForSeconds(tiempoCooldown);

        enCooldown = false;
        Debug.Log("Power-Up disponible nuevamente.");
    }
}