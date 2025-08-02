using UnityEngine;

public class Logger : MonoBehaviour {

    public bool active = true;
    public const char newLine = '\n';
    
    private string logContent = "";

    private static Logger m_Instance = null;
    public static Logger instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<Logger>();
            return m_Instance;
        }
    }

    public void Log(object o) {
        print(o);
        // Object object = new Object(o as Object);
        logContent += o.ToString() + newLine;
    }

    public string GetLogContent() => logContent;

}