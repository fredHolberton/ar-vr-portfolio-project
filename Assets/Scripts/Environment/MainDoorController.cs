using UnityEngine;

public class MainDoorController : MonoBehaviour
{
    public static MainDoorController instance;

    /// <summary>
    /// Animator that manages the opening/closing of the main door 
    /// </summary>
    [SerializeField] private Animator anim = null;

    /* State of the door */
    private bool doorIsOpened;

    /* State of the locked door */
    private bool doorIsLocked;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        doorIsOpened = false;
        doorIsLocked = false;
    }

    public void OpenCloseDoor()
    {
        if (anim != null && !doorIsLocked)
        {
            doorIsOpened = !doorIsOpened;
            anim.SetBool("character_nearby", doorIsOpened);

            if (doorIsOpened && GameManager.instance.GetGameStarted() && !GameManager.instance.GetGameFinished())
            {
                GameManager.instance.GameWon();
            }
        }
        else
        {
            ComputerVoiceManager.instance.SayMainDoorIsLocked(1.0f);
        }

    }

    public void SetDoorIsLocked(bool value)
    {
        doorIsLocked = value;
    }

    public bool GetDoorIsLocked()
    {
        return doorIsLocked;
    }
}
