using UnityEngine;

public class Powerup {

    // Since this class doesn't extends MonoBehaviour, it can't access print properties and other objects
    // so we just reference them
    public Player player;
    public Logger logger;

    public Powerup() {}

    public virtual void Activate() {}

    protected virtual void PowerupUpdate() {}
    protected virtual void PowerupFixedUpdate() {}

    public virtual void PowerupRemove() {}

    public void print(object o) {
        logger.Log(o);
    }

    public void Update() {
        PowerupUpdate();
    }

    public void FixedUpdate() {
        PowerupFixedUpdate();
    }

}