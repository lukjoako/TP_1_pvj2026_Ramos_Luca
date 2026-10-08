using UnityEngine;
using TMPro; // Si usas TextMeshPro

public class ControladorVictoria : MonoBehaviour
{
    [Header("UI y Configuración")]
    public GameObject cartelVictoria; // Arrastra aquí el texto de Victoria
    private bool tieneObjeto = false;

    public void ObtenerObjeto()
    {
        tieneObjeto = true;
        Debug.Log("¡Objeto recolectado! Llévalo a la meta.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si entra a la zona de Meta y lleva el objeto
        if (other.CompareTag("Meta") && tieneObjeto)
        {
            Debug.Log("¡Llegaste a la meta con el objeto!");
            
            if (cartelVictoria != null)
            {
                cartelVictoria.SetActive(true); // Mostrar mensaje en pantalla
            }

            // Opcional: Detener la física o el movimiento al ganar
            Time.timeScale = 0f; 
        }
    }
}