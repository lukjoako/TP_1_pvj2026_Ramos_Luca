using UnityEngine;
using UnityEngine.InputSystem; // Requerido para el nuevo Input System

public class Llave : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public float distanciaInteraccion = 4.0f; // Distancia máxima para presionar 'E'

    private Transform jugadorTransform;
    private Transform puntoTransporte;
    private Rigidbody rb;
    private Collider col;

    private bool estaAgarrada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // Buscar al jugador por Tag
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            jugadorTransform = playerObj.transform;
            // Buscar el punto de transporte exacto en el jugador
            puntoTransporte = jugadorTransform.Find("transporte");
        }
    }

    void Update()
    {
        // Lectura de la tecla 'E'
        bool presionoE = false;

        if (Keyboard.current != null)
        {
            presionoE = Keyboard.current.eKey.wasPressedThisFrame;
        }
        else
        {
            presionoE = Input.GetKeyDown(KeyCode.E); // Fallback clásico
        }

        if (presionoE)
        {
            if (estaAgarrada)
            {
                SoltarLlave();
            }
            else
            {
                if (jugadorTransform != null)
                {
                    float distancia = Vector3.Distance(transform.position, jugadorTransform.position);
                    if (distancia <= distanciaInteraccion)
                    {
                        AgarrarLlave();
                    }
                }
            }
        }
    }

    void AgarrarLlave()
    {
        estaAgarrada = true;
        Quaternion rotacionOriginal = transform.rotation;

        // 1. Emparentar al punto de transporte del jugador mediante SetParent()
        if (puntoTransporte != null)
        {
            transform.SetParent(puntoTransporte);
            transform.localPosition = Vector3.zero;
        }
        else if (jugadorTransform != null)
        {
            transform.SetParent(jugadorTransform);
            transform.localPosition = new Vector3(0, 1.0f, 1.2f);
        }

        transform.rotation = rotacionOriginal;

        // 2. Control de física y colisiones durante el transporte
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (col != null)
        {
            col.isTrigger = true;
        }

        // 3. Notificar al sistema de victoria que tenemos la llave correcta
        ControladorVictoria victoria = jugadorTransform.GetComponent<ControladorVictoria>();
        if (victoria != null)
        {
            victoria.ObtenerObjeto();
        }

        Debug.Log("Llave recolectada y equipada.");
    }

    void SoltarLlave()
    {
        estaAgarrada = false;

        // Restablecer la independencia jerárquica
        transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (col != null)
        {
            col.isTrigger = false;
        }

        Debug.Log("Llave soltada.");
    }

    public bool EstaAgarrada()
    {
        return estaAgarrada;
    }
}