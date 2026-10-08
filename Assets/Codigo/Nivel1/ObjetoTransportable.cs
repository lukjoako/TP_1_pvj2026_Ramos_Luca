using UnityEngine;

public class ObjetoTransportable : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private bool estaTransportado = false;

    void Update()
    {
        // Gira mientras esté en el suelo
        if (!estaTransportado)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!estaTransportado && other.CompareTag("Player"))
        {
            estaTransportado = true;

            // Emparentar al personaje para que lo siga
            transform.SetParent(other.transform);
            
            // Ajustar posición para que quede flotando sobre la cabeza del personaje
            transform.localPosition = new Vector3(0, 2.0f, 0);

            // Avisarle al jugador que ya lleva el objeto
            other.GetComponent<ControladorVictoria>().ObtenerObjeto();
        }
    }
}