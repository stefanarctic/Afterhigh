using UnityEngine;

public class TestPowerup : Powerup {

    public override void Activate() => print($"Activated powerup {GetType().Name}");

    public override void PowerupRemove() => print($"{GetType().Name} got removed off of {player.gameObject}");

}