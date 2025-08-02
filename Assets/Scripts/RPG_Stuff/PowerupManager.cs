using UnityEngine;
using System;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour {

    private static PowerupManager m_Instance = null;
    public static PowerupManager instance {
        get {
            if(m_Instance == null) m_Instance = FindObjectOfType<PowerupManager>();
            return m_Instance;
        }
    }

    private List<Powerup> m_Powerups;

    private Logger m_Logger;

    void Start() {
        m_Powerups = new List<Powerup>();
        m_Logger = Logger.instance;
    }

    void Update() {
        foreach(var powerup in m_Powerups) {
            powerup.Update();
        }
    }

    void FixedUpdate() {
        foreach(var powerup in m_Powerups) {
            powerup.FixedUpdate();
        }
    }

    public T AddPowerup<T>() where T : Powerup {
        T powerup = Activator.CreateInstance(typeof(T)) as T;
        powerup.player = Player.instance;
        powerup.logger = Logger.instance;
        m_Powerups.Add(powerup);
        powerup.Activate();
        return powerup;
    }

    public T GetPowerup<T>() where T : Powerup {
        foreach(var currentPowerup in m_Powerups) {
            if(currentPowerup.GetType() == typeof(T)) {
                return currentPowerup as T;
            }
        }
        return null;
    }

    public bool TryGetPowerup<T>(out T outputPowerup) where T : Powerup {
        T powerup = GetComponent<T>();
        outputPowerup = powerup;
        return powerup != null;
    }

    public void RemovePowerup<T>(T powerup) where T : Powerup {
        powerup.PowerupRemove();
        m_Powerups.Remove(powerup);
    }

}