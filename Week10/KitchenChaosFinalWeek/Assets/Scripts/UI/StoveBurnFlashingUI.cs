using System;
using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;
    
    private const string IS_FLASHING = "IsFlashing"; 

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounterOnOnProgressChanged;
        // set to hide at start 
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounterOnOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}
