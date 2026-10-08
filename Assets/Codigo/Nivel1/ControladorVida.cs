using UnityEngine;
using UnityEngine.SceneManagement; // Requerido para cambiar a la escena "Perdiste"

public class ControladorVida : MonoBehaviour
{
    [Header("Configuración de Vidas")]
    public int vidas = 3;

    public void RecibirDanio(int cantidad = 1)
    {
        vidas -= cantidad;
        Debug.Log("¡Impacto recibido! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            PerderJuego();
        }
    }

    private void PerderJuego()
    {
        Debug.Log("El jugador se quedó sin vidas. Cargando escena Perdiste...");
        
        // Carga la escena de derrota cuando las vidas llegan a 0
        SceneManager.LoadScene("Perdiste");
    }
}