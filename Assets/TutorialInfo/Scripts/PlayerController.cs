using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player controller for the "Orb Collector" lab goal. COMPLETED.
/// Attach to a cube named "Player" (Rigidbody, freeze rotation X/Z).
/// Drag the orb prefab and Main Camera into the Inspector fields.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Lab wiring (drag your objects here)")]
    [SerializeField] private GameObject orbPrefab;      // TODO 5: used to spawn a bonus orb
    [SerializeField] private Transform cameraTransform; // used by TODO 3 (camera follow)

    [Header("Camera follow (TODO 3)")]
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 12f, -8f);
    [SerializeField] private float cameraSharpness = 5f;

    private Rigidbody rb;
    private Vector3 lastPosition;
    private bool quitting = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Debug.Log("[PlayerController] Ready. Collect all orbs to win.");
    }

    void Update()
    {
        HandleMovement();

        // Remember where we are, so OnDestroy knows the last position.
        lastPosition = transform.position;

        // Test key for TODO 5: press X to destroy the player.
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // A/D + arrows
        float v = Input.GetAxis("Vertical");   // W/S + arrows

        Vector3 movement = new Vector3(h, 0f, v).normalized;

        // Time.deltaTime makes speed identical at any frame rate.
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    // TODO 1: log the name of any wall you bump.
    // Runs once when the contact starts, not every frame.
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name.Contains("Wall"))
        {
            Debug.Log("[OnCollisionEnter] Hit " + c.gameObject.name);
        }
    }

    // TODO 2: touch an orb, destroy it, and log the pickup.
    // The Orb must have Is Trigger ticked and the tag "Orb".
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            Debug.Log("[OnTriggerEnter] Picked up " + other.gameObject.name);
            Destroy(other.gameObject);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.Collect();
            }
        }
    }

    // TODO 3: move the camera to the player position + offset.
    // LateUpdate runs after all Update calls, so the player has already moved.
    // If CameraFollow is on the camera, it does this job, so we skip here.
    void LateUpdate()
    {
        if (cameraTransform == null) return;
        if (cameraTransform.GetComponent<CameraFollow>() != null) return;

        Vector3 desired = transform.position + cameraOffset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desired, cameraSharpness * Time.deltaTime);
        cameraTransform.LookAt(transform.position);
    }

    // TODO 5: when the player is destroyed, spawn one bonus orb at the last position.
    void OnDestroy()
    {
        if (quitting || orbPrefab == null) return;

        Instantiate(orbPrefab, lastPosition, Quaternion.identity);
        Debug.Log("[OnDestroy] Bonus orb spawned at " + lastPosition);
    }

    // Stops OnDestroy from spawning an orb when you press Stop in the editor.
    void OnApplicationQuit()
    {
        quitting = true;
    }
}
