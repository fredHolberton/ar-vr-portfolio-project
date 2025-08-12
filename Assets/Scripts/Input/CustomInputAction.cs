using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputAction : MonoBehaviour
{
    public GameObject whiteLight;
    public InputActionReference customButton;

    private bool isGrabbed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        customButton.action.started += ButtonWasPressed;
        customButton.action.canceled += ButtonWasReleased;
        isGrabbed = false;
    }

    void ButtonWasPressed(InputAction.CallbackContext context)
    {
        if (isGrabbed) whiteLight.SetActive(true);
    }

    void ButtonWasReleased(InputAction.CallbackContext context)
    {
        whiteLight.SetActive(false);
    }

    public void FlashLightIsGrabbed()
    {
        isGrabbed = true;
    }

    public void FlashLightIsReleased()
    {
        isGrabbed = false;
    }
}
