using UnityEngine;
using UnityEngine.InputSystem; // Requerido si utilizas el nuevo Input System

public class ObjetoAgarrable : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public float distanciaInteraccion = 3.0f; // Distancia máxima para poder presionar 'E'

    private Transform jugadorTransform;
    private Transform puntoMano;
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
            // Busca el punto de agarre hijo en el personaje
            puntoMano = jugadorTransform.Find("HoldPoint");
        }
    }

    void Update()
    {
        // Detectar si se presiona la tecla 'E'
        bool presionoE = false;

        if (Keyboard.current != null)
        {
            presionoE = Keyboard.current.eKey.wasPressedThisFrame;
        }
        else
        {
            presionoE = Input.GetKeyDown(KeyCode.E); // Fallback para el Input clásico
        }

        if (presionoE)
        {
            if (estaAgarrado)
            {
                SoltarObjeto();
            }
            else
            {
                // Verificar si el jugador está lo suficientemente cerca para agarrarlo
                float distancia = Vector3.Distance(transform.position, jugadorTransform.position);
                if (distancia <= distanciaInteraccion)
                {
                    AgarrarObjeto();
                }
            }
        }
    }

    void AgarrarObjeto()
    {
        estaAgarrado = true;

        // 1. Emparentar al punto de transporte del jugador (SetParent)
        if (puntoMano != null)
        {
            transform.SetParent(puntoMano);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            transform.SetParent(jugadorTransform);
            transform.localPosition = new Vector3(0, 1f, 1.2f);
        }

        // 2. Desactivar físicas mientras se transporta
        if (rb != null)
        {
            rb.isKinematic = true; // Evita que la gravedad y las fuerzas afecten la caja
        }

        // 3. Opcional: Desactivar o ajustar colisiones para que no empuje al jugador
        if (col != null)
        {
            col.isTrigger = true;
        }

        Debug.Log("Objeto agarrado con la tecla E.");
    }

    void SoltarObjeto()
    {
        estaAgarrado = false;

        // 1. Restaurar independencia jerárquica (SetParent a null)
        transform.SetParent(null);

        // 2. Reactivar física
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // 3. Reactivar colisiones sólidas
        if (col != null)
        {
            col.isTrigger = false;
        }

        Debug.Log("Objeto soltado.");
    }
}