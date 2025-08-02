using UnityEngine;

public class PlayerGridMovement : MonoBehaviour {

    private static PlayerGridMovement m_Instance = null;
    public static PlayerGridMovement instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<PlayerGridMovement>();
            return m_Instance;
        }
    }

    void Start() {
        Debug.Log(PlayerGridMovement.instance.gameObject.name);
    }

    void Update() {
        foreach(string key in KeyBindings.instance.moveForward) {
            if(Input.GetKeyDown(key)) {
                transform.position += transform.TransformDirection(Vector3.forward) * GridManager.instance.gridSize;
            }
        }
    }

}