using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Main UI")]
    /// <summary>
    /// Object canvas to be desabled during into 
    /// </summary>
    [SerializeField] private GameObject mainCanvas = null;


    [Header("Boat player")]
    [SerializeField] private GameObject boatPlayer = null;

    [SerializeField] private GameObject playerPerson = null;


    [Header("Intro")]
    [SerializeField] private Animator introCameraAnimator = null;

    [SerializeField] private Animator doorAnimator = null;

    [Header("Credits")]
    [SerializeField] private GameObject creditsCanvas = null;
    [SerializeField] private GameObject welcomeText = null;
    [SerializeField] private GameObject titleText = null;
    [SerializeField] private GameObject musicsText = null;
    [SerializeField] private GameObject musicAuthorText = null;
    [SerializeField] private GameObject soundsText = null;
    [SerializeField] private GameObject soundAuthorText = null;
    [SerializeField] private GameObject boatText = null;
    [SerializeField] private GameObject boatAuthorText = null;
    [SerializeField] private GameObject stationText = null;
    [SerializeField] private GameObject stationAuthorText = null;
    [SerializeField] private GameObject landText = null;
    [SerializeField] private GameObject landAuthorText = null;
    [SerializeField] private GameObject designText = null;
    [SerializeField] private GameObject designAuthorText = null;

    private bool _BoatAndCameraAreSynchronized = false;
    private Vector3 _decalage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.SetActive(false);
        _BoatAndCameraAreSynchronized = false;
        _decalage = new Vector3(0.343f, 0.167f, 0.067f);
        playerPerson.SetActive(true);
        welcomeText.SetActive(false);
        titleText.SetActive(false);
        musicsText.SetActive(false);
        musicAuthorText.SetActive(false);
        soundsText.SetActive(false);
        soundAuthorText.SetActive(false);
        boatText.SetActive(false);
        boatAuthorText.SetActive(false);
        stationText.SetActive(false);
        stationAuthorText.SetActive(false);
        landText.SetActive(false);
        landAuthorText.SetActive (false);
        designText.SetActive(false);
        designAuthorText.SetActive(false);
        creditsCanvas.SetActive(true);


        GameManager.instance.BlockInteractions();

        introCameraAnimator.enabled = true;
        AmbianceMusicManager.instance.PlayIntro(0.1f);
        //EndOfIntro();
    }

    void LateUpdate()
    {
        if (_BoatAndCameraAreSynchronized)
        {
            boatPlayer.transform.position = transform.position - _decalage;
            boatPlayer.transform.rotation = transform.rotation;
        }
    }


    public void SynchroniseBoatAndCamera()
    {
        _BoatAndCameraAreSynchronized = true;

        // Display Music credits
        musicsText.SetActive(true);
        musicAuthorText.SetActive(true);
        
        Debug.Log("Syncho on");
    }

    public void DisplaySoundCredits()
    {
        // Display Sound credits
        soundsText.SetActive(true);
        soundAuthorText.SetActive(true);
    }

    public void DisplayBoatCredits()
    {
        // display Boat Credits
        boatText.SetActive(true);
        boatAuthorText.SetActive(true);
    }

    public void DesynchronizeBoatAndcamera()
    {
        _BoatAndCameraAreSynchronized = false;
        boatPlayer.GetComponent<AudioSource>().Pause();

        // Display Station credits
        stationText.SetActive(true);
        stationAuthorText.SetActive(true);

        Debug.Log("Syncho off");
    }

    public void StartCrimbing()
    {
        playerPerson.SetActive(false);

        // Display land credits
        landText.SetActive(true);
        landAuthorText.SetActive(true);  
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("character_nearby", true);

        // Display Design credits
        designText.SetActive(true);
        designAuthorText.SetActive(true);
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool("character_nearby", false);

        // Start to display the Credits canvas
        welcomeText.SetActive(true);
        titleText.SetActive(true);
    }

    public void EndOfIntro()
    {
        // End of animation:
        // 1. Enable Credits canvas
        creditsCanvas.SetActive(false);
        
        // 2. start of game rules computer voice
        Debug.Log("Start of the presentation of the rules");
        ComputerVoiceManager.instance.SayGameRules(0.5f, onRulesCompleted);
    }

    private void onRulesCompleted()
    {
        Debug.Log("End of the presentation of the rules");
        // 1. Display UI
        mainCanvas.SetActive(true);

        // 2. unlock game
        GameManager.instance.AllowInteractions();

    }
}
