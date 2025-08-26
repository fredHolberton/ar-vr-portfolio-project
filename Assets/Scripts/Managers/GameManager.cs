using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// instance of the class ComputerVoicemanager
    /// </summary>
    public static GameManager instance;

    [Header("Phase skippers")]
    [SerializeField] private bool skipIntroPhase = false;
    [SerializeField] private bool  skipGlassDoorPhase = false;
    [SerializeField] private bool  skipChessBoardPhase = false;
    [SerializeField] private bool  skipGameRulePhase = false;
    [SerializeField] private bool  skipMainCameraPositionPhase = false;


    [Header("Controller")]
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;


    [Header("Main Camera")]
    [SerializeField] private GameObject xrOrigin;


    [Header("Main UI")]
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private bool exitButtonEnable;


    [Header("Projector")]
    [SerializeField] private XRSimpleInteractable projectorXRInteractable;


    [Header("Console")]
    [SerializeField] private GameObject consoleScreen;
    [SerializeField] private XRSimpleInteractable consoleInteractable;


    [Header("Origami")]
    [SerializeField] private GameObject origami;


    [Header("Alarm lights")]
    [SerializeField] private GameObject[] alarmLights;


    private XRBaseInteractor[] leftInteractors;
    private XRBaseInteractor[] rightInteractors;
    private int mouvedObjectNumber = 0;

    private bool gameStarted = false;
    private bool gameFinished = false;
    private CountdownTimer countdownTimer;

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
        //Initialization settings
        countdownTimer = GetComponent<CountdownTimer>();
        countdownTimer.enabled = false;

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

        BlockInteractions();
        UnlockMainDoor();
        if (!skipGlassDoorPhase) LockGlassDoor();
        if (!skipChessBoardPhase) LockProjector();

        SetAlarmLights(false);

        if (consoleScreen.activeSelf) consoleScreen.SetActive(false);
        if (consoleInteractable.enabled == true) consoleInteractable.enabled = false;

        if (origami.activeSelf) origami.SetActive(false);

        // Playing intro or Starting game
        if (!skipIntroPhase)
        {
            IntroManager.instance.PlayIntro();
        }
        else
        {
            if (!skipMainCameraPositionPhase)
            {
                xrOrigin.transform.position = new Vector3(1622.72f, 153.388f, 1492.845f);
                xrOrigin.transform.rotation = Quaternion.identity;
            }

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

    private void LockMainDoor()
    {
        MainDoorController.instance.SetDoorIsLocked(true);
    }

    private void UnlockMainDoor()
    {
        MainDoorController.instance.SetDoorIsLocked(false);
    }

    private void LockGlassDoor()
    {
        GlassDoorController.instance.SetDoorIsLocked(true);
    }

    private void UnlockGlassDoor()
    {
        GlassDoorController.instance.SetDoorIsLocked(false);
    }

    private void LockProjector()
    {
        projectorXRInteractable.enabled = false;
    }

    public void ChessboardComplete()
    {
        // The chessboard is complete: 
        // 1. Allow the projector to turn on
        projectorXRInteractable.enabled = true;

        // 2. Launch the computer voice to announce the information to the player
        ComputerVoiceManager.instance.SayChessboardComplete(1.0f);
    }

    public void StartGame()
    {
        // 1. Lock the main door
        LockMainDoor();

        if (!skipGameRulePhase)
        {
            // 2. start of game rules computer voice
            Debug.Log("Start of the presentation of the rules");
            ComputerVoiceManager.instance.SayGameRules(1.0f, onRulesCompleted);
        }
        else
        {
            onRulesCompleted();
        }
    }

    public void IncrementMovedObjectNumber()
    {
        mouvedObjectNumber += 1;
        //if (mouvedObjectNumber == minMouvedObjectNumber)
        //{
            // The glass door may be unlocked
            //SoundManager.instance.PlayDoorOpenSound(1.0f);
            //UnlockGlassDoor();
            //ComputerVoiceManager.instance.SayGlassDoorIsUnLocked(2.0f);
        //}
    }

    public void TheGlassDoorMustBeUnlocked()
    {
        if (GlassDoorController.instance.GetDoorIsLocked())
        {
            // The glass door may be unlocked
            SoundManager.instance.PlayDoorUnlockSound(1.0f);
            UnlockGlassDoor();
            ComputerVoiceManager.instance.SayGlassDoorIsUnLocked(2.0f);
        }
    }

    public void TheFlashLightIsOn()
    {
        if (!consoleScreen.activeSelf) consoleScreen.SetActive(true);
        consoleInteractable.enabled = true;
        SoundManager.instance.PlayConsoleScreenDisplaySound(1.0f);
    }

    public void TheFlashLightIsOff()
    {
        if (consoleScreen.activeSelf) consoleScreen.SetActive(false);
        consoleInteractable.enabled = false;
    }
    public void StartAlarmMode()
    {
        // Set the alarm red light on
        SetAlarmLights(true);

        // déclencher l'alarme
        SoundManager.instance.PlayAlarmSound(0.8f);
    }

    public void TheProjectorIsOn()
    {
        if (!origami.activeSelf) origami.SetActive(true);
    }

    public void TheProjectorIsOff()
    {
        if (origami.activeSelf) origami.SetActive(false);
    }

    public void TheMainDoorMustBeUnlocked()
    {
        if (MainDoorController.instance.GetDoorIsLocked())
        {
            // The main door may be unlocked
            SoundManager.instance.PlayDoorUnlockSound(1.0f);
            UnlockMainDoor();
            ComputerVoiceManager.instance.SayMainDoorIsUnLocked(2.0f);
        }
    }

    public void TheMainDoorMustBeLocked()
    {
        if (!MainDoorController.instance.GetDoorIsLocked())
        {
            // The main door may be unlocked
            SoundManager.instance.PlayMainDoorLockSound(1.0f);
            LockMainDoor();
            ComputerVoiceManager.instance.SayMainDoorIsLocked(2.0f);
        }
    }

    public void GameLost()
    {
        gameFinished = true;

        // Say the defeat message
        ComputerVoiceManager.instance.SayDefeatMessage(2.0f, onDefeatMessageCompleate);
    }

    private void onDefeatMessageCompleate()
    {
        // Start the defeat musique
        AmbianceMusicManager.instance.PlayDefeatMusique(0.5f);

        // Play Base immersion animation
        BaseManager.instance.PlayBaseImmersion();
    }

    public void GameWon()
    {
        gameFinished = true;

        // Stop alarm sound if it is playing
        SoundManager.instance.StopSound();
        // Set the alarm red light off
        SetAlarmLights(false);

        // Start the victory musique
        AmbianceMusicManager.instance.PlayVictoryMusique(0.5f);

        // Say the victory message
        ComputerVoiceManager.instance.SayVictoryMessage(2.0f);
    }

    public bool GetGameStarted()
    {
        return gameStarted;
    }

    public bool GetGameFinished()
    {
        return gameFinished;
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
        gameStarted = true;
        countdownTimer.enabled = true;

        // 2. unlock game interactions
        // TODO Allow this method to work properly
        AllowInteractions();

        // 3. start the game ambiance music
        AmbianceMusicManager.instance.PlayAmbianceMusic(0.2f);
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

    private void SetAlarmLights(bool value)
    {
        foreach (GameObject alarmLight in alarmLights)
        {
            alarmLight.SetActive(value);
        }
    }
}
