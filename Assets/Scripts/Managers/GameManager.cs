using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// instance of the class ComputerVoicemanager
    /// </summary>
    public static GameManager instance;

    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    [SerializeField] private Animator mainDoorAnimator;
    [SerializeField] private Animator glassDoorAnimator;

    private XRBaseInteractor[] leftInteractors;
    private XRBaseInteractor[] rightInteractors;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        leftInteractors = leftController.GetComponentsInChildren<XRBaseInteractor>(includeInactive: true);
        rightInteractors = rightController.GetComponentsInChildren<XRBaseInteractor>(includeInactive: true);
    }

    public void BlockInteractions()
    {
        //SetInteractorsEnabled(leftInteractors, false);
        //SetInteractorsEnabled(rightInteractors, false);
        leftController.SetActive(false);
        rightController.SetActive(false);
    }

    public void AllowInteractions()
    {
        //SetInteractorsEnabled(leftInteractors, true);
        //SetInteractorsEnabled(rightInteractors, true);
        leftController.SetActive(true);
        rightController.SetActive(true);
    }

    public void LockMainDoor()
    {
        mainDoorAnimator.enabled = false;
    }

    public void UnlockMainDoor()
    {
        mainDoorAnimator.enabled = true;
    }

    public void LockGlassDoor()
    {
        glassDoorAnimator.enabled = false;
    }

    public void UnlockGlassDoor()
    {
        glassDoorAnimator.enabled = true;
    }

    private void SetInteractorsEnabled(XRBaseInteractor[] interactors, bool enabled)
    {
        foreach (var interactor in interactors)
        {
            if (interactor != null && interactor.gameObject.activeInHierarchy)
            {
                interactor.enabled = enabled;
            }
        }
    }
}
