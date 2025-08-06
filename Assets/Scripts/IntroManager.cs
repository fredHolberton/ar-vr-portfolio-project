using UnityEngine;

public class IntroManager : MonoBehaviour
{
    /// <summary>
    /// Object canvas to be desabled during into 
    /// </summary>
    [SerializeField] private GameObject mainCanvas = null;

    [SerializeField] private GameObject boatPlayer = null;

    [SerializeField] private Animator introCameraAnimator = null;

    [SerializeField] private Animator doorAnimator = null;

    private bool _BoatAndCameraAreSynchronized = false;
    private Vector3 _decalage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.SetActive(false);
        _BoatAndCameraAreSynchronized = false;
        _decalage = new Vector3(0.343f, 0.167f, 0.067f);

        introCameraAnimator.enabled = true;
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
        //introBoatAnimator.enabled = true;
        //introCameraAnimator.enabled = false;
        Debug.Log("Syncho on");
    }

    public void DesynchronizeBoatAndcamera()
    {
        _BoatAndCameraAreSynchronized = false;
        Debug.Log("Syncho off");
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("character_nearby", true);
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool("character_nearby", false);
    }

    public void EndOfIntro()
    {
        // End of animation and start of player welcome
    }
}
