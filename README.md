# TP_1_pvj2026_Ramos_Luca
Programación de Videojuegos I

Descripción del Proyecto
Este proyecto es una experiencia interactiva en 3D desarrollada en Unity, que integra mecánicas esenciales de juego, físicas, detección de colisiones, gestión de escenas y elementos de interfaz de usuario (UI). El jugador debe navegar por un escenario con obstáculos dinámicos, gestionar objetos e interactuar con el entorno para completar el nivel entregando la Llave en la Meta.

 Controles del Juego

 Tecla / Control |

Movimiento: Teclas `W`, `A`, `S`, `D` 
Recoger Soltar Objeto: Tecla `E` |
Interacción con Entorno Detección automática por colisión (`Trigger`)

Captura del Juego

<img width="614" height="260" alt="imagen del gameplay del juego" src="https://github.com/user-attachments/assets/d2e73947-eb4d-4cf5-a731-87c5ce71fe4e" />

 Mecánicas e Implementación Técnica

1. Sistema de Físicas y Movimiento
- Plataformas y Colisiones: Se utilizaron primitivas 3D (`Cube`) con `BoxCollider` estándar para asegurar una superficie de apoyo sólida y prevenir problemas de traspaso (*fall-through*).
- Estabilidad del Personaje: Se aplicaron restricciones en el `Rigidbody` (`FreezeRotation` en X, Y, Z) para evitar que el jugador se vuelque accidentalmente durante las colisiones.

2. Generación Periódica de Obstáculos (Spawner)
- Instanciación: Mediante el script `SpawnerObstaculos.cs` y la función `InvokeRepeating()`, se generan proyectiles de forma periódica desde un punto de origen en la escena.
- Desplazamiento: Cada proyectil se traslada continuamente sobre su eje hacia adelante (`transform.Translate`).
- Gestión de Memoria: Cada proyectil instanciado cuenta con una rutina de limpieza mediante `Destroy(gameObject, tiempoVida)` para prevenir la acumulación indefinida de GameObjects y fugas de memoria.

3. Sistema de Vidas y Derrota
- Detección de Daño: Al colisionar con un proyectil, el script `ControladorVida.cs` resta una vida al jugador.
- Condición de Derrota: Al acumular 3 impactos (vidas = 0), se llama a `SceneManager.LoadScene("Perdiste")` para transicionar automáticamente a la escena de derrota.

4. Transporte de Objetos e Interacción
- Objeto Transportable (`Caja.cs` y `Llave.cs`): Permite al jugador recoger y soltar elementos al estar dentro del rango de interacción y presionar la tecla `E`.
- Parentesco Jerárquico:** Al recoger un objeto, se asigna como hijo del punto `transporte` ubicado en el jugador utilizando `SetParent()`. Al soltarlo, se restablece `SetParent(null)` para devolverle su independencia.
- Control de Físicas: Mientras el objeto es transportado, su `Rigidbody` pasa a `isKinematic = true` y su colisionador a `isTrigger = true` para no interferir con la física del personaje. Al soltarlo, recupera sus propiedades físicas normales congelando su rotación para evitar volteos indeseados.

5. Power-Up
- Capacidad modificada: Velocidad de movimiento del personaje.
- Mecánica y Corrutinas: Al recoger el ítem (`PowerUpVelocidad.cs`), el script `EfectosJugador.cs` ejecuta una corrutina (`CorrutinaPowerUpVelocidad`) que multiplica la velocidad del jugador durante un tiempo determinado.
- Retroalimentación Visual: El personaje cambia de color de forma perceptible mientras el beneficio está activo.
- Cooldown y Respawn: Cuando el efecto finaliza, se restablece la velocidad original y se inicia un tiempo de recarga (*cooldown*). El ítem se oculta temporalmente (`MeshRenderer` y `Collider` desactivados) y vuelve a reaparecer en el mapa tras completar el ciclo de recarga.

6. Condición de Victoria y Entrega
- Validación de Objeto: La meta cuenta con un detector tipo `Trigger` que verifica si el jugador lleva equipado el objeto correcto (Llave).
- Escena de Victoria: Al ingresar a la zona de meta con la llave, se activa el panel de UI de Victoria y se pausa la ejecución del juego con `Time.timeScale = 0f`.

-----------------------------------------------------------------
🛠️ Problemas que no logre resolver y complicaciones

* al intentar usar un objeto plane, tuve muchas complicaciones, el cylinder para el proyectil no se ajustaba bien el collider, asi que decidi usar mejor los cube
* la caja iba a hacer en realidad una rampa para subir a la victoria, pero se iba volando, asi que decidi ponerlo para desbloquear el powerup pero no logre instanciarlo y preferi sacarlo y dejar la caja
* el mapa no logre ponerlo estetico para la presentacion, al tener problemas con el packagment que descargue (las texturas desaparecian, y debia subir todo el packagment que pesaba como 500 mb y no iba a poder presentar a tiempo el trabajo) 
* me falto poner mas elementos en la pantalla como las vidas, se nota en consola pero no en el juego si no se ve la consola

