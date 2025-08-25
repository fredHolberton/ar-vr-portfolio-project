using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class GlassDoorController : MonoBehaviour
{
    public static GlassDoorController instance;
    /// <summary>
    /// Storage room in the floor
    /// </summary>
    [SerializeField] private GameObject storageRoom = null;

    /// <summary>
    /// Main room in the floor
    /// </summary>
    [SerializeField] private GameObject mainRoom = null;

    /// <summary>
    /// XR Origin to know the player's position
    /// </summary>
    [SerializeField] private GameObject xrOrigin = null;

    /// <summary>
    /// Animator that manages the opening/closing of the glass door 
    /// </summary>
    [SerializeField] private Animator anim = null;

    /* State of the door */
    private bool doorIsOpened;

    /* State of the locked door */
    private bool doorIsLocked;


    /* position of room divider */
    private float roomSeparator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        doorIsOpened = false;
        doorIsLocked = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomSeparator = GameObject.Find("GlassDoor").transform.position.z;
    }

    public void OpenCloseDoor()
    {
        if (anim != null && !doorIsLocked)
        {
            doorIsOpened = !doorIsOpened;
            anim.SetBool("character_nearby", doorIsOpened);

            /* Determinate where is the player in order to allow him to teleport into MainRoom and/or StorageRoom */
            storageRoom.GetComponent<TeleportationArea>().enabled = (doorIsOpened || xrOrigin.transform.position.z < roomSeparator);
            mainRoom.GetComponent<TeleportationArea>().enabled = (doorIsOpened || xrOrigin.transform.position.z > roomSeparator);
        }
        else
        {
            ComputerVoiceManager.instance.SayGlassDoorIsLocked(1.0f);
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
