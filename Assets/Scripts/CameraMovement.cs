using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float sensitivity = .4f; // SerializeField lets us adjust value from editor window

    public Transform camTransform;

    InputAction look;
    InputAction startLook;

    private Vector2 mouseDelta = Vector2.zero;
    private Vector2 mouseLockingPos = Vector2.negativeInfinity; // Vector2.zero cannot work becase maybe the cursor must be locked at (0, 0)
    private bool shouldLook = false;
    private float pitch, pitchVel, yaw, yawVel = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.LookAt(camTransform);

        // Assign input actions from global InputSystem
        look = InputSystem.actions.FindAction("Look");
        startLook = InputSystem.actions.FindAction("StartLook");

        // Assign actions to input events
        look.performed += ctx =>
        {
            if (shouldLook)
                mouseDelta = ctx.ReadValue<Vector2>();
            else
                mouseDelta = Vector2.zero;
        };
        look.canceled += ctx => mouseDelta = Vector2.zero;

        startLook.performed += _ =>
        {
            shouldLook = true;
            mouseLockingPos = Mouse.current.position.ReadValue();
        };
        startLook.canceled += _ =>
        {
            shouldLook = false;
            mouseLockingPos = Vector2.negativeInfinity;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDelta != Vector2.zero) // Only rotate if there's mouse movement
        {
            Vector2 scaledMouseDelta = mouseDelta * sensitivity;
            yaw = Mathf.SmoothDamp(yaw, yaw + scaledMouseDelta.x, ref yawVel, 0.1f);
            pitch = math.clamp(Mathf.SmoothDamp(pitch, pitch - scaledMouseDelta.y, ref pitchVel, 0.1f), -40f, 80f); // Mathf.SmoothDamp interpolates smoothly
            camTransform.rotation = Quaternion.Euler(0, yaw, pitch);
        }
        if (shouldLook)
            Mouse.current.WarpCursorPosition(mouseLockingPos);
    }
}
