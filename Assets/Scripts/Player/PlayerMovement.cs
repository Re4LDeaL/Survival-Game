using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // snelheid van de player.

    Vector3 movement;                   // de vector voor de direction van de speler .
    Rigidbody playerRigidbody;          // Rigidbody van de speler pakken.
    int floorMask;                      // Een mask zodat een ray kan gewoon gebruikt worden voor de gameobjects op de "Floor" layer.
    float camRayLength = 100f;          // Lengte van de ray.

    void Awake()
    {
        // Maakt een layer mask voor de "Floor" layer.
        floorMask = LayerMask.GetMask("Floor");

        // Hier pak ik de Rigidbody.
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        // Bewaar de input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Beweegt speler.
        Move(h, v);

        // Draait speler naar de muis cursor.
        Turning();
     
    }

    void Move(float h, float v)
    {
        // Zet de movement vector als op de axis.
        movement.Set(h, 0f, v);

        // Normalise de vector movement en maak het proportioneel naar speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Beweegt de player naar zijn locatie + movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // Maak een ray van de cursor in de kant van de camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Maak een RaycastHit variable zodat ik weet welke wordt aangeraakt door de ray
        RaycastHit floorHit;

        // Het uitvoeren van Raycast en als het iets aanraakt in de "floor" layer
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Maak een Vector van Player tot het punt op de vloer - De raycast van mousehit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Dit maakt de vector precies op de vloer.
            playerToMouse.y = 0f;

            // Maak een quaternion (rotation) gebaseerd op de vector van de player naar het muis.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Maak de rotation van de player naar deze rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

}