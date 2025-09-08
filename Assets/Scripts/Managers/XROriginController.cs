using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class XROrigineController : MonoBehaviour
{
    [Header("Camera movement")]
    [SerializeField] private CharacterController characterController;
    /// <summary>
    /// "Mouse Delta" action reference
    /// </summary>
    [SerializeField] private InputActionReference mouseDeltaAction;

    /// <summary>
    /// "Left Click" action reference
    /// </summary>
    [SerializeField] private InputActionReference leftClickAction;

    /// <summary>
    /// "Right Click" action reference for teleportation
    /// </summary>
    [SerializeField] private InputActionReference rightClickAction;

    /// <summary>
    /// Up keyboard action to move forward
    /// </summary>
    [SerializeField] private InputActionReference upAction;

    /// <summary>
    /// Down keyboard action to move back
    /// </summary>
    [SerializeField] private InputActionReference downAction;

    /// <summary>
    /// Left keyboard action to move left
    /// </summary>
    [SerializeField] private InputActionReference leftAction;

    /// <summary>
    /// Right keyboard action to move right
    /// </summary>
    [SerializeField] private InputActionReference rightAction;

    [SerializeField] private InputActionReference mousePositionAction;

    /// <summary>
    /// Rotation speed of the XR Camera
    /// </summary>
    [SerializeField] private float rotationSpeed = 3.0f;
    
    /// <summary>
    /// Move speed of the XR Camera
    /// </summary>
    [SerializeField] private float moveSpeed = 3.0f;

    [Header("Teleportation")]

    /// <summary>
    /// XR Rig of the XR Camera
    /// </summary>
    [SerializeField] private XROrigin xrOrigin;
    /// <summary>
    /// Teleportation layer mask
    /// </summary>
    [SerializeField] private LayerMask teleportLayerMask;

    /// <summary>
    /// Teleportation marker prefab
    /// </summary>
    [SerializeField] private GameObject teleportMarkerPrefab;

    /// <summary>
    /// Teleportation max distance
    /// </summary>
    [SerializeField] private float maxTeleportDistance = 10f;


    // Mouse move
    private Vector2 mouseDelta;

    // Mouse left button state
    private bool isLeftMouseButtonPressed;
    private bool isRightMouseButtonPressed;
    private GameObject teleportMarker;
    private Vector3? teleportDestination;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        teleportMarker = Instantiate(teleportMarkerPrefab);
        teleportMarker.SetActive(false);
    }

    private void OnEnable()
    {
        // Active l'action et abonne la méthode de callback
        mouseDeltaAction.action.performed += OnMouseDelta;
        leftClickAction.action.performed += OnLeftClickPressed;
        leftClickAction.action.canceled += OnLeftClickReleased;
        rightClickAction.action.performed += OnRightClickPressed;
        rightClickAction.action.canceled += OnRightClickReleased;

        mouseDeltaAction.action.Enable();
        leftClickAction.action.Enable();
        rightClickAction.action.Enable();
        upAction.action.Enable();
        downAction.action.Enable();
        leftAction.action.Enable();
        rightAction.action.Enable();
        mousePositionAction.action.Enable();
    }

    private void OnDisable()
    {
        // Désabonne la méthode de callback et désactive l'action
        mouseDeltaAction.action.performed -= OnMouseDelta;
        leftClickAction.action.performed -= OnLeftClickPressed;
        leftClickAction.action.canceled -= OnLeftClickReleased;
        rightClickAction.action.performed -= OnRightClickPressed;
        rightClickAction.action.canceled -= OnRightClickReleased;

        mouseDeltaAction.action.Disable();
        leftClickAction.action.Disable();
        rightClickAction.action.Disable();
        upAction.action.Disable();
        downAction.action.Disable();
        leftAction.action.Disable();
        rightAction.action.Disable();
        mousePositionAction.action.Disable();
    }

    private void OnMouseDelta(InputAction.CallbackContext context)
    {
        // Récupère le déplacement de la souris
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void OnLeftClickPressed(InputAction.CallbackContext context)
    {
        isLeftMouseButtonPressed = true;
    }

    private void OnLeftClickReleased(InputAction.CallbackContext context)
    {
        isLeftMouseButtonPressed = false;
        mouseDelta = Vector2.zero; // Réinitialise le déplacement
    }

    private void OnRightClickPressed(InputAction.CallbackContext context)
    {
        isRightMouseButtonPressed = true;
        UpdateTeleportMarker();
    }

    private void OnRightClickReleased(InputAction.CallbackContext context)
    {
        isRightMouseButtonPressed = false;
        if (teleportDestination.HasValue)
        {
            Teleport(teleportDestination.Value);
            teleportDestination = null;
        }
        teleportMarker.SetActive(false);
    }

    private void UpdateTeleportMarker()
    {
        Vector2 mousePosition = mousePositionAction.action.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxTeleportDistance, teleportLayerMask))
        {
            teleportMarker.SetActive(true);
            teleportMarker.transform.position = hit.point;
            teleportDestination = hit.point;
        }
        else
        {
            teleportMarker.SetActive(false);
            teleportDestination = null;
        }
    }

    private void Teleport(Vector3 destination)
    {
        // save the camera orientation
        Quaternion cameraRotation = mainCamera.transform.rotation;

        // do the teleportation
        xrOrigin.transform.position = destination;

        // reset the initial camera orientation
        mainCamera.transform.rotation = cameraRotation;
    }

    private void Update()
    {
        // Applique la rotation en fonction du déplacement de la souris
        if (isLeftMouseButtonPressed && mouseDelta.magnitude > 0.1f)
        {
            // Rotation horizontale (autour de l'axe Y)
            float horizontalRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, horizontalRotation, Space.World);

            // Rotation verticale (autour de l'axe X)
            float verticalRotation = mouseDelta.y * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, -verticalRotation, Space.Self);

            // Réinitialise le déplacement de la souris
            mouseDelta = Vector2.zero;
        }

        // Déplacement au clavier (construction manuelle du Vector2)
        float x = 0f;
        float y = 0f;

        if (rightAction.action.IsPressed()) x += 1f;
        if (leftAction.action.IsPressed()) x -= 1f;
        if (upAction.action.IsPressed()) y += 1f;
        if (downAction.action.IsPressed()) y -= 1f;

        Vector2 moveInput = new Vector2(x, y).normalized;

        if (moveInput.magnitude > 0.1f)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            moveDirection = transform.TransformDirection(moveDirection);
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Mise à jour du marqueur de téléportation
        if (isRightMouseButtonPressed)
        {
            UpdateTeleportMarker();
        }
    }
}
