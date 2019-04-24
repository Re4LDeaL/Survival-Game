using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // De locatie van de target.
    public float smoothing = 5f;        // De snelheid van de camera.

    Vector3 offset;                     // De default offset van de target.

    void Start()
    {
        // Maakt de default offset.
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Maak de positie van de camera met de offset van de target.
        Vector3 targetCamPos = target.position + offset;

        // Rustig interpoleren tussen de huidige camera's positie naar de target.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}