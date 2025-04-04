using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {
    
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator _animator;

    private const string OPEN_CLOSE = "OpenClose";

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e) {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
