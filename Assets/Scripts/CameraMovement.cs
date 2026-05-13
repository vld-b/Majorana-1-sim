using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float sensitivity = .4f; // SerializeField lets us adjust value from editor window

    public Transform camRotationTransform;
    public Transform camTransform;

    InputAction look;
    InputAction startLook;
    InputAction zoom;

    private Vector2 mouseDelta = Vector2.zero;
    private Vector2 mouseLockingPos = Vector2.negativeInfinity; // Vector2.zero cannot work becase maybe the cursor must be locked at (0, 0)
    private bool shouldLook = false;
    private float pitch, pitchVel, yaw, yawVel = 0f;
    private float zoomVal = 0f;
    private int zoomLevel = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.LookAt(camRotationTransform);

        // Assign input actions from global InputSystem
        look = InputSystem.actions.FindAction("Look");
        startLook = InputSystem.actions.FindAction("StartLook");
        zoom = InputSystem.actions.FindAction("Zoom");

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

        zoom.performed += ctx =>
        {
            zoomVal = ctx.ReadValue<Vector2>().y;
        };

        zoom.canceled += ctx =>
        {
            zoomVal = 0f;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDelta != Vector2.zero) // Only rotate if there's mouse movement
        {
            Vector2 scaledMouseDelta = mouseDelta * sensitivity;
            yaw = Mathf.SmoothDamp(yaw, yaw + scaledMouseDelta.x, ref yawVel, .1f);
            pitch = math.clamp(Mathf.SmoothDamp(pitch, pitch - scaledMouseDelta.y, ref pitchVel, .1f), -40f, 80f); // Mathf.SmoothDamp interpolates smoothly
            camRotationTransform.rotation = Quaternion.Euler(0, yaw, pitch);
        }
        if (shouldLook)
            Mouse.current.WarpCursorPosition(mouseLockingPos);
        if (zoomVal > 0f && zoomLevel > 1)
        {
            camTransform.position += camTransform.forward;
            --zoomLevel;
        } else if (zoomVal < 0f && zoomLevel < 6)
        {
            camTransform.position -= camTransform.forward;
            ++zoomLevel;
        }
    }
}
