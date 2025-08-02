using UnityEngine;

public class ParticleDontPlayOnAwake : MonoBehaviour
{
    public ParticleSystem particle;

    private void Awake()
    {
        //particleObject.SetActive(false);
        //particleObject.GetComponent<ParticleSystem>().Stop();
        particle.Stop();
    }
}