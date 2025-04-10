using UnityEngine;
using UnityEngine.AI;
using System;

public class NavMeshStuff : MonoBehaviour, IKitchenObjectParent
{
    public Transform kitchenPoint;
    public Transform deliveryPoint;
    public GameObject plateVisual;
    
    public ClearCounter targetClearCounter;
    public DeliveryCounter deliveryCounter; // Add reference to delivery counter

    private NavMeshAgent agent;
    private bool hasFood;
    private bool isNearKitchen;
    private KitchenObject kitchenObject;
    public Transform plateHolderTransform;

    void Start()
    {
        
        // Check all required components
        agent = GetComponent<NavMeshAgent>();
        GameInput.Instance.OnInteractAction += HandleInteract;
        GoToKitchen();
    }

    private void OnDestroy()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnInteractAction -= HandleInteract;
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (HasKitchenObject())
            {
                // If we have food, try to deliver it
                if (deliveryCounter != null)
                {
                    // Try to deliver the plate
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        // Only accepts Plates
                        DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                        GetKitchenObject().DestroySelf();
                    }
                }
                GoToKitchen();
            }
            else
            {
                // If we don't have food, try to pick up from counter
                if (targetClearCounter != null && targetClearCounter.HasKitchenObject())
                {
                    // Pick up the object from the counter
                    targetClearCounter.GetKitchenObject().SetKitchenObjectParent(this);
                    GoToDeliveryPoint();
                }
                else
                {
                    // If no food on counter, go back to kitchen
                    GoToKitchen();
                }
            }
        }
    }

    private void HandleInteract(object sender, EventArgs e)
    {
        if (!hasFood && isNearKitchen)
        {
            TryPickUpFromCounter();
            GoToDeliveryPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupSpot"))
        {
            isNearKitchen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickupSpot"))
        {
            isNearKitchen = false;
        }
    }

    void GoToKitchen()
    {
        if (kitchenPoint != null)
        {
            agent.SetDestination(kitchenPoint.position);
        }
    }

    void GoToDeliveryPoint()
    {
        if (deliveryPoint != null)
        {
            agent.SetDestination(deliveryPoint.position);
        }
    }

    private void TryPickUpFromCounter()
    {
        if (targetClearCounter.HasKitchenObject())
        {
            KitchenObject kitchenObject = targetClearCounter.GetKitchenObject();
            if (!hasFood)
            {
                kitchenObject.SetKitchenObjectParent(this);
                hasFood = true;
            }
            else
            {
                Debug.Log("Robot already has food, cannot pick up");
            }
        }
        else
        {
            Debug.Log("Counter has no kitchen object to pick up");
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return plateHolderTransform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}