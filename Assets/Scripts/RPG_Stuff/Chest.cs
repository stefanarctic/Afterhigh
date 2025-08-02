using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool playerInRange = false;
    [SerializeField] private bool playerFacingChest = false;

    [Header("UI")]
    public GameObject interactionPrompt; // Assign a UI text or image in inspector

    private Animator m_Animator;
    private Transform playerTransform;

    void Awake() {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("Is Chest Open", false);
    }

    void Start() {
        playerTransform = Player.instance.transform;
        
        // Hide interaction prompt initially
        if(interactionPrompt != null) {
            interactionPrompt.SetActive(false);
        }
    }

    void Update() {
        CheckPlayerProximity();
        CheckPlayerFacing();
        UpdateInteractionPrompt();
        
        if(playerInRange && playerFacingChest && IsInteractKeyPressed() && !isOpen) {
            OpenChest();
        }
    }

    private void CheckPlayerProximity() {
        if(playerTransform == null) return;
        
        // Get grid positions
        Vector3 chestGridPos = GetGridPosition(transform.position);
        Vector3 playerGridPos = GetGridPosition(playerTransform.position);
        
        // Check if player is on an adjacent tile (including diagonal)
        float gridDistance = Vector3.Distance(chestGridPos, playerGridPos);
        playerInRange = gridDistance <= GridManager.instance.gridSize * 1.5f; // Allow some tolerance for grid snapping
    }

    private Vector3 GetGridPosition(Vector3 worldPosition) {
        float gridSize = GridManager.instance.gridSize;
        return new Vector3(
            Mathf.Round(worldPosition.x / gridSize) * gridSize,
            Mathf.Round(worldPosition.y / gridSize) * gridSize,
            Mathf.Round(worldPosition.z / gridSize) * gridSize
        );
    }

    private void CheckPlayerFacing() {
        if(playerTransform == null) return;
        
        // Get the direction from player to chest
        Vector3 directionToChest = (transform.position - playerTransform.position).normalized;
        
        // Get the direction the player is facing
        Vector3 playerForward = playerTransform.forward;
        
        // Check if player is facing the chest (with some tolerance)
        float dotProduct = Vector3.Dot(playerForward, directionToChest);
        playerFacingChest = dotProduct > 0.7f; // About 45 degrees tolerance
    }

    private void UpdateInteractionPrompt() {
        if(interactionPrompt != null) {
            bool canInteract = playerInRange && playerFacingChest && !isOpen;
            interactionPrompt.SetActive(canInteract);
        }
    }

    private bool IsInteractKeyPressed() {
        if(KeyBindings.instance.interact == null || KeyBindings.instance.interact.Count == 0) {
            // Fallback to E key if no interact keys are configured
            return Input.GetKeyDown(KeyCode.E);
        }
        
        foreach(string key in KeyBindings.instance.interact) {
            if(Input.GetKeyDown(key)) {
                return true;
            }
        }
        return false;
    }

    private void OpenChest() {
        isOpen = true;
        m_Animator.SetTrigger("Open Chest Trigger");
        m_Animator.SetBool("Is Chest Open", true);
        PowerupManager powerupManager = PowerupManager.instance;
        powerupManager.AddPowerup<TestPowerup>();

        // Hide interaction prompt after opening
        if(interactionPrompt != null) {
            interactionPrompt.SetActive(false);
        }
    }

    // Optional: Visual feedback for debugging
    void OnDrawGizmosSelected() {
        if(playerTransform != null) {
            // Draw line to player
            Gizmos.color = playerInRange ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, playerTransform.position);
            
            // Draw player's forward direction
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(playerTransform.position, playerTransform.forward * 2f);
            
            // Draw direction to chest
            Gizmos.color = playerFacingChest ? Color.yellow : Color.gray;
            Vector3 directionToChest = (transform.position - playerTransform.position).normalized;
            Gizmos.DrawRay(playerTransform.position, directionToChest * 2f);
        }
    }

}