using UnityEngine;

public class IntroManager : MonoBehaviour
{

    /// <summary>
    /// Object canvas to be desabled during into 
    /// </summary>
    [SerializeField] private GameObject mainCanvas = null;

    [SerializeField] private GameObject boatPlayer = null;

    [SerializeField] private Animator introBoatAnimator = null;

    [SerializeField] private Animator introCameraAnimator = null;

    private bool _BoatAndCameraAreSynchronized = false;
    private Vector3 _decalage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.SetActive(false);
        _BoatAndCameraAreSynchronized = false;
        _decalage = new Vector3(0.343f, 0.167f, 0.067f);

        introBoatAnimator.enabled = false;
        introCameraAnimator.enabled = true;
    }

    void LateUpdate()
    {
        if (_BoatAndCameraAreSynchronized)
        {
            transform.position = boatPlayer.transform.position + _decalage;
            transform.rotation = boatPlayer.transform.rotation;
        }
    }


    public void SynchoniseBoatAndCamera()
    {
        _BoatAndCameraAreSynchronized = true;
        introBoatAnimator.enabled = true;
        introCameraAnimator.enabled = false;
        Debug.Log("Syncho on");
    }
}
