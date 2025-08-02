using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private static CameraRotation m_Instance = null;
    public static CameraRotation instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<CameraRotation>();
            return m_Instance;
        }
    }

    void Update() {
        foreach(string key in KeyBindings.instance.rotateRight) {
            if(Input.GetKeyDown(key)) {
                transform.Rotate(new Vector3(0f, 90f, 0f));
            }
        }
        foreach(string key in KeyBindings.instance.rotateLeft) {
            if(Input.GetKeyDown(key)) {
                transform.Rotate(new Vector3(0f, -90f, 0f));
            }
        }
    }

}