using UnityEngine;
using System.Collections;

public class PlayerGridMovement : MonoBehaviour {

    private static PlayerGridMovement m_Instance = null;
    public static PlayerGridMovement instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<PlayerGridMovement>();
            return m_Instance;
        }
    }

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f; // Speed of smooth movement
    [SerializeField] private float movementCooldown = 0.3f; // Cooldown between movements
    
    private bool isMoving = false;
    private bool canMove = true;
    private Vector3 targetPosition;
    private Coroutine movementCoroutine;

    void Start() {
        Debug.Log(PlayerGridMovement.instance.gameObject.name);
        targetPosition = transform.position;
    }

    void Update() {
        if (!canMove || isMoving || CameraRotation.instance.IsRotating()) return;

        foreach(string key in KeyBindings.instance.moveForward) {
            if(Input.GetKeyDown(key)) {
                MoveForward();
                break; // Only move once per frame
            }
        }
        
        foreach(string key in KeyBindings.instance.moveBackward) {
            if(Input.GetKeyDown(key)) {
                MoveBackward();
                break; // Only move once per frame
            }
        }
    }

    private void MoveForward() {
        if (isMoving || !canMove || CameraRotation.instance.IsRotating()) return;

        Vector3 newPosition = transform.position + transform.TransformDirection(Vector3.forward) * GridManager.instance.gridSize;
        StartMovement(newPosition);
    }

    private void MoveBackward() {
        if (isMoving || !canMove || CameraRotation.instance.IsRotating()) return;

        Vector3 newPosition = transform.position + transform.TransformDirection(Vector3.back) * GridManager.instance.gridSize;
        StartMovement(newPosition);
    }

    private void StartMovement(Vector3 newPosition) {
        if (movementCoroutine != null) {
            StopCoroutine(movementCoroutine);
        }
        
        targetPosition = newPosition;
        movementCoroutine = StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove() {
        isMoving = true;
        canMove = false;
        
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        float moveDuration = GridManager.instance.gridSize / movementSpeed;

        while (elapsedTime < moveDuration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            
            // Use smooth step for more natural movement
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            
            yield return null;
        }

        // Ensure we end up exactly at the target position
        transform.position = targetPosition;
        isMoving = false;

        // Start cooldown
        yield return new WaitForSeconds(movementCooldown);
        canMove = true;
    }

    // Public method to check if player is currently moving
    public bool IsMoving() {
        return isMoving;
    }

    // Public method to check if player can move
    public bool CanMove() {
        return canMove && !isMoving && !CameraRotation.instance.IsRotating();
    }

    // Public method to force stop movement (useful for external interruptions)
    public void StopMovement() {
        if (movementCoroutine != null) {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
        isMoving = false;
        canMove = true;
    }
}