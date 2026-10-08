using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSistema : MonoBehaviour
{
    // Función para el botón de Iniciar
    public void CargarNivel1()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    // Función para el botón de Salir
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cierra el ejecutable (.exe / .apk)
    }
}