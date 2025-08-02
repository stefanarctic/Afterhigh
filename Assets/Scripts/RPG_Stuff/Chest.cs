using UnityEngine;

public class Chest : MonoBehaviour {

    public float range;
    //public string particleObjectName = "Chest Open Particle";
    //private GameObject chestOpenObject;

    [SerializeField] private bool isHovering = false;
    [SerializeField] private bool isOpen = false;

    private Animator m_Animator;

    void Awake() {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("Is Chest Open", false);

        //chestOpenObject = GameObject.Find(particleObjectName);
    }

    void Update() {
        if(isHovering && Input.GetMouseButtonDown(0) && !isOpen) {
            OpenChest();
        }
    }

    void OnMouseEnter() => isHovering = true;
    
    void OnMouseExit() => isHovering = false;

    private void OpenChest() {
        isOpen = true;
        m_Animator.SetTrigger("Open Chest Trigger");
        m_Animator.SetBool("Is Chest Open", true);
        PowerupManager powerupManager = PowerupManager.instance;
        powerupManager.AddPowerup<TestPowerup>();

        //chestOpenObject.transform.position = transform.position;
        //ParticleSystem ps = chestOpenObject.GetComponent<ParticleSystem>();
        //ps.Play();
    }

}