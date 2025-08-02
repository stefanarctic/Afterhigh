using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

    private static CameraRotation m_Instance = null;
    public static CameraRotation instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<CameraRotation>();
            return m_Instance;
        }
    }

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 180f; // Degrees per second
    
    private bool isRotating = false;
    private Quaternion targetRotation;
    private Coroutine rotationCoroutine;

    void Update() {
        if (isRotating) return;

        foreach(string key in KeyBindings.instance.rotateRight) {
            if(Input.GetKeyDown(key)) {
                RotateRight();
                break; // Only rotate once per frame
            }
        }
        
        foreach(string key in KeyBindings.instance.rotateLeft) {
            if(Input.GetKeyDown(key)) {
                RotateLeft();
                break; // Only rotate once per frame
            }
        }
    }

    private void RotateRight() {
        if (isRotating) return;
        
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
        StartRotation(newRotation);
    }

    private void RotateLeft() {
        if (isRotating) return;
        
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, -90f, 0f);
        StartRotation(newRotation);
    }

    private void StartRotation(Quaternion newRotation) {
        if (rotationCoroutine != null) {
            StopCoroutine(rotationCoroutine);
        }
        
        targetRotation = newRotation;
        rotationCoroutine = StartCoroutine(SmoothRotate());
    }

    private IEnumerator SmoothRotate() {
        isRotating = true;
        
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;
        float rotateDuration = 90f / rotationSpeed; // 90 degrees at specified speed

        while (elapsedTime < rotateDuration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotateDuration;
            
            // Use smooth step for more natural rotation
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, smoothT);
            
            yield return null;
        }

        // Ensure we end up exactly at the target rotation
        transform.rotation = targetRotation;
        isRotating = false;
    }

    // Public method to check if camera is currently rotating
    public bool IsRotating() {
        return isRotating;
    }

    // Public method to check if camera can rotate
    public bool CanRotate() {
        return !isRotating;
    }

    // Public method to force stop rotation (useful for external interruptions)
    public void StopRotation() {
        if (rotationCoroutine != null) {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
        isRotating = false;
    }
}