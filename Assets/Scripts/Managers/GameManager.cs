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

    [Header("Intro")]
    [SerializeField] private bool playIntro = true;

    [Header("Controller")]
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;

    [Header("Door Animators")]
    [SerializeField] private Animator mainDoorAnimator;
    [SerializeField] private Animator glassDoorAnimator;

    [Header("Main Camera")]
    [SerializeField] private GameObject xrOrigin;

    [Header("Main UI")]
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private bool exitButtonEnable;

    [SerializeField] private int minMouvedObjectNumber = 4;

    private XRBaseInteractor[] leftInteractors;
    private XRBaseInteractor[] rightInteractors;

    private int mouvedObjectNumber = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        leftInteractors = leftController.GetComponentsInChildren<XRBaseInteractor>(includeInactive: true);
        rightInteractors = rightController.GetComponentsInChildren<XRBaseInteractor>(includeInactive: true);
    }

    void Start()
    {
        if (exitButtonEnable)
        {
            if (!exitButton.activeSelf) exitButton.SetActive(true);
        }
        else
        {
            if (exitButton.activeSelf) exitButton.SetActive(false);
        }

        mouvedObjectNumber = 0;

        mainCanvas.SetActive(false);
        // TODO Allow this method to work properly
        //BlockInteractions();
        UnlockMainDoor();
        LockGlassDoor();

        if (playIntro)
        {
            IntroManager.instance.PlayIntro();
        }
        else
        {
            xrOrigin.transform.position = new Vector3(1622.72f, 153.388f, 1492.845f);
            xrOrigin.transform.rotation = Quaternion.identity;
            StartGame();
        }
    }

    public void BlockInteractions()
    {
        //SetInteractorsEnabled(leftInteractors, false);
        //SetInteractorsEnabled(rightInteractors, false);
        leftController.SetActive(false);
        rightController.SetActive(false);
        Debug.Log("Interactions blocked");
    }

    public void AllowInteractions()
    {
        //SetInteractorsEnabled(leftInteractors, true);
        //SetInteractorsEnabled(rightInteractors, true);
        leftController.SetActive(true);
        rightController.SetActive(true);
        Debug.Log("Interactions allowed");
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

    public void StartGame()
    {
        // 1. Lock the main door
        LockMainDoor();
        
        // 2. start of game rules computer voice
        Debug.Log("Start of the presentation of the rules");
        ComputerVoiceManager.instance.SayGameRules(0.5f, onRulesCompleted);
    }

    public void IncrementMovedObjectNumber()
    {
        mouvedObjectNumber += 1;
        if (mouvedObjectNumber == minMouvedObjectNumber)
        {
            // The glass door may be unlocked
            UnlockGlassDoor();
            ComputerVoiceManager.instance.SayGlassDoorIsUnLocked(0.5f);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void onRulesCompleted()
    {
        Debug.Log("End of the presentation of the rules");
        // 1. Display UI
        mainCanvas.SetActive(true);

        // 2. unlock game interactions
        // TODO Allow this method to work properly
        // AllowInteractions();

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
