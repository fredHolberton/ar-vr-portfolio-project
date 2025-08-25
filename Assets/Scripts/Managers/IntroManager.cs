using UnityEngine;

public class IntroManager : MonoBehaviour
{
    /// <summary>
    /// instance of the class IntroManager
    /// </summary>
    public static IntroManager instance;

  
    [Header("Boat player")]
    [SerializeField] private GameObject boatPlayer = null;

    [SerializeField] private GameObject playerPerson = null;


    [Header("Animator")]
    [SerializeField] private Animator introCameraAnimator = null;


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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        introCameraAnimator.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        landAuthorText.SetActive(false);
        designText.SetActive(false);
        designAuthorText.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    void LateUpdate()
    {
        if (_BoatAndCameraAreSynchronized)
        {
            boatPlayer.transform.position = transform.position - _decalage;
            boatPlayer.transform.rotation = transform.rotation;
        }
    }

    public void PlayIntro()
    {
        Debug.Log("Play intro");
        introCameraAnimator.enabled = true;
        AmbianceMusicManager.instance.PlayIntro(0.1f);
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
        Debug.Log("Syncho off");

        // Display Land credits
        landText.SetActive(true);
        landAuthorText.SetActive(true);
    }

    public void StartCrimbing()
    {
        playerPerson.SetActive(false);
    }

    public void StopCrimbing()
    {
        // Display Station credits
        stationText.SetActive(true);
        stationAuthorText.SetActive(true);
    }

    public void OpenDoor()
    {
        MainDoorController.instance.OpenCloseDoor();

        // Display Design credits
        designText.SetActive(true);
        designAuthorText.SetActive(true);
    }

    public void CloseDoor()
    {
        MainDoorController.instance.OpenCloseDoor();
        MainDoorController.instance.SetDoorIsLocked(true);

        // Start to display the Credits canvas
        welcomeText.SetActive(true);
        titleText.SetActive(true);
    }

    public void EndOfIntro()
    {
        // End of animation:
        introCameraAnimator.enabled = false;
        
        // 1. Enable Credits canvas
        creditsCanvas.SetActive(false);

        // 2 call GameManager for starting game
        GameManager.instance.StartGame();
    }
}
