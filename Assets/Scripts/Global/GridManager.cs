using UnityEngine;

public class GridManager : MonoBehaviour {
    
    private static GridManager m_Instance = null;
    public static GridManager instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<GridManager>();
            return m_Instance;
        }
    }

    public float gridSize = 10;

}