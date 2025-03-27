using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance  { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }
    [FormerlySerializedAs("counterTopPoint")] [SerializeField] private Transform kitchenObjectHoldPoint;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    
    private bool isWalking;
    private Vector3 lastInteraction; 
    private BaseCounter selectedCounter;
    private KitchenObject  kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player in the scene. Something went wrong.");
        }
        Instance = this;
    }
    private void Update() {
        HandleMovement();
        HandleInteraction();
    }
    
    public bool IsWalking() {
        return isWalking;
    }

    public void Start() {
        gameInput.OnInteractAction += GameInput_OnInteraction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractionAlternateAction;
    }

    private void GameInput_OnInteraction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractionAlternateAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirc = new Vector3(inputVector.x, 0f,  inputVector.y);

        if (moveDirc != Vector3.zero) {
            lastInteraction = moveDirc;
        }
        
        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit rayCastHit, interactionDistance, countersLayerMask)) {
            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // does have clear counter
                //clearCounter.Interact();
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirc = new Vector3(inputVector.x, 0f,  inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirc, moveDistance);

        if (!canMove) {
            // cannot move towards moveDirc
            
            // try and move only one the X axis 
            Vector3 moveDircX = new Vector3(moveDirc.x, 0f, 0).normalized;
            canMove = moveDirc.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDircX, moveDistance);

            if (canMove) {
                // can only move on the x 
                moveDirc = moveDircX;
            }
            else {
                // cannnot move on x-axis 
                // Attempt to only move on Z axis
                Vector3 moveDircz = new Vector3(0f, 0f, moveDirc.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDircz, moveDistance);

                if (canMove) {
                    // can only move on Z
                    moveDirc = moveDircz;
                }
                else {
                    // cannot move at all in any direction
                }
            }
        }
        if (canMove) {
            transform.position += moveDirc * moveDistance; 
        }
        
        isWalking = moveDirc != Vector3.zero;
        
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDirc,Time.deltaTime * rotationSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}