using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter {

    [FormerlySerializedAs("cutKitchenObjectSO")] [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
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

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // then we will visibly cut it if kitchen object there
            // we are getting the output before destroying and spawning it in
            KitchenObjectSO outputKichenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKichenObjectSO, this);
        }
    }

    // this is allowing us to find out which recipe to use 
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
