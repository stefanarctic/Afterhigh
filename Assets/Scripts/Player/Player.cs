using UnityEngine;

// Class for managing all the player stuff
public class Player : MonoBehaviour {

    private static Player m_Instance = null;
    public static Player instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<Player>();
            return m_Instance;
        }
    }

    [HideInInspector] public PlayerGridMovement playerGridMovement;

    void Start() {
        playerGridMovement = GetComponent<PlayerGridMovement>();
    }

}