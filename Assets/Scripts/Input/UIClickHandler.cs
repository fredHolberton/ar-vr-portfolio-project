using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class UIClickHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference uiClickAction;

    private void OnEnable()
    {
        uiClickAction.action.performed += OnUIClick;
        uiClickAction.action.Enable();
    }

    private void OnDisable()
    {
        uiClickAction.action.performed -= OnUIClick;
        uiClickAction.action.Disable();
    }

    private void OnUIClick(InputAction.CallbackContext context)
    {
        // Crée un événement de clic pour le système UI
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Mouse.current.position.ReadValue();

        // Envoie un rayon depuis la caméra
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Vérifie si un bouton UI a été cliqué
        if (results.Count > 0)
        {
            GameObject uiObject = results[0].gameObject;
            ExecuteEvents.Execute(uiObject, pointerData, ExecuteEvents.pointerClickHandler);
            Debug.Log("Bouton cliqué : " + uiObject.name);
        }
    }
}
