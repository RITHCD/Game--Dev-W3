using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera follow for the "Orb Collector" lab goal. COMPLETED.
/// Attach to the Main Camera. Drag the Player into the Target field.
/// Follows the player (LateUpdate) and zooms with the mouse wheel (TODO 8).
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;     // drag the Player here

    [Header("Framing")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 12f, -8f);
    [SerializeField] private float followSharpness = 5f;

    [Header("Zoom (stretch)")]
    [SerializeField] private float minZoom = 6f;
    [SerializeField] private float maxZoom = 18f;
    [SerializeField] private float zoomSpeed = 20f;

    // TODO 8 (stretch): scroll wheel changes the distance to the player.
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // about 0.1 per notch
        if (scroll == 0f) return;

        float distance = offset.magnitude;
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minZoom, maxZoom);
        offset = offset.normalized * distance;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Follow AFTER the player moved. This is why we use LateUpdate.
        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired, followSharpness * Time.deltaTime);
        transform.LookAt(target);
    }
}
