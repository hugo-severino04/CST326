using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class OrbitOnMiddleClick : MonoBehaviour
{
    public CinemachineCamera orbitCamera;
    public PlayerInput playerInput;
    
    public float orbitSensitivityX = 0.1f;
    public float orbitSensitivityY = 0.1f;

    private InputAction lookAction;
    private InputAction orbitAction;
    private CinemachineOrbitalFollow orbitalFollow;
    private float horizontalValue;
    private float verticalValue;

    void Awake()
    {
        lookAction = playerInput.actions["Look"];
        orbitAction = playerInput.actions["Orbit"];
        orbitalFollow = orbitCamera.GetComponentInChildren<CinemachineOrbitalFollow>();
        horizontalValue = orbitalFollow.HorizontalAxis.Value;
        verticalValue = orbitalFollow.VerticalAxis.Value;
    }

    void Update()
    {
        if (orbitAction != null && orbitAction.IsPressed())
        {
            Vector2 delta = lookAction.ReadValue<Vector2>();
            horizontalValue += delta.x * orbitSensitivityX;
            verticalValue += delta.y * orbitSensitivityY;
        }

        orbitalFollow.HorizontalAxis.Value = horizontalValue;
        orbitalFollow.VerticalAxis.Value = verticalValue;
    }
}