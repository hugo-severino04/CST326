using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class MiddleMouseInput : MonoBehaviour, AxisState.IInputAxisProvider
{
    public PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction orbitAction;
    private Vector2 orbitDelta;

    private bool isOrbiting = false;

    void Awake()
    {
        lookAction = playerInput.actions["Look"];
        orbitAction = playerInput.actions["Orbit"];
    }

    void Update()
    {
        isOrbiting = orbitAction.IsPressed();
        orbitDelta = isOrbiting ? lookAction.ReadValue<Vector2>() : Vector2.zero;
    }

    public float GetAxisValue(int axis)
    {
        return axis switch
        {
            0 => orbitDelta.x,  
            1 => orbitDelta.y,
            _ => 0
        };
    }
}