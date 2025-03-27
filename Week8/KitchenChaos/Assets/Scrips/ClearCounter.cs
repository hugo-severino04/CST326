using UnityEngine;

public class ClearCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object
            if (player.HasKitchenObject()) {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // player not carring anything
            }
        } else {
            // there is a kitchen object 
            if (player.HasKitchenObject()) {
                // player is carrying something 
            } else {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
