using System;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    private float spawnPointTimer;
    private float spawnPointTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;
    
    private void Update() {
        spawnPointTimer += Time.deltaTime;
        if (spawnPointTimer > spawnPointTimerMax) {
            spawnPointTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;
                
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player is empty-handed
            if (platesSpawnedAmount > 0) {
                // there is at least one plate
                platesSpawnedAmount--;
                // spawn in obj
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                // firing event 
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
