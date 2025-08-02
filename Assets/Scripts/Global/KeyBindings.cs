using UnityEngine;
using System.Collections.Generic;

public class KeyBindings : MonoBehaviour {

    private static KeyBindings m_Instance = null;
    public static KeyBindings instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<KeyBindings>();
            return m_Instance;
        }
    }


    [Header("Movement")]
    public List<string> rotateRight;
    public List<string> rotateLeft;
    public List<string> moveForward;
    public List<string> moveBackward;

    [Header("Interaction")]
    public List<string> interact;

}