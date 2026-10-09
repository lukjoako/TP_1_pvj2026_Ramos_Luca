using UnityEngine;
using UnityEngine.InputSystem; // Requerido para el nuevo Input System

public class Caja : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public float distanciaInteraccion = 3.0f; // Distancia máxima para poder presionar 'E'

    private Transform jugadorTransform;
    private Transform puntoTransporte;
    private Rigidbody rb;
    private Collider col;

    private bool estaAgarrado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // Buscar al Player por Tag
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            jugadorTransform = playerObj.transform;
            // Buscar el punto de transporte exacto dentro del Player
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
            presionoE = Input.GetKeyDown(KeyCode.E); // Compatibilidad con Input clásico
        }

        if (presionoE)
        {
            if (estaAgarrado)
            {
                SoltarObjeto();
            }
            else
            {
                if (jugadorTransform != null)
                {
                    float distancia = Vector3.Distance(transform.position, jugadorTransform.position);
                    if (distancia <= distanciaInteraccion)
                    {
                        AgarrarObjeto();
                    }
                }
            }
        }
    }

    void AgarrarObjeto()
    {
        estaAgarrado = true;

        // Guardar rotación exacta para no alterarla al agarrar
        Quaternion rotacionOriginal = transform.rotation;

        // 1. Emparentar con SetParent() al punto 'transporte'
        if (puntoTransporte != null)
        {
            transform.SetParent(puntoTransporte);
            transform.localPosition = Vector3.zero;
        }
        else if (jugadorTransform != null)
        {
            // Fallback si no encuentra el objeto 'transporte'
            transform.SetParent(jugadorTransform);
            transform.localPosition = new Vector3(0, 1.0f, 1.2f);
        }

        // Mantener la rotación tal como estaba
        transform.rotation = rotacionOriginal;

        // 2. Control de física y colisiones durante el transporte
        if (rb != null)
        {
            rb.isKinematic = true; // Desactiva gravedad e impulsos
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Congela rotaciones
        }

        if (col != null)
        {
            col.isTrigger = true; // Desactiva la colisión física pesada con el jugador
        }

        Debug.Log("Caja agarrada.");
    }

    void SoltarObjeto()
    {
        estaAgarrado = false;

        // 1. Restaurar independencia jerárquica (SetParent a null)
        transform.SetParent(null);

        // 2. Restaurar física y colisiones
        if (rb != null)
        {
            rb.isKinematic = false; // Vuelve a reaccionar a la gravedad
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Evita que se vuelque al caer
        }

        if (col != null)
        {
            col.isTrigger = false; // Vuelve a ser un objeto sólido
        }

        Debug.Log("Caja soltada.");
    }
}