using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class GlassDoorController : MonoBehaviour
{
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


    /* position of room divider */
    private float roomSeparator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorIsOpened = false;
        roomSeparator = GameObject.Find("GlassDoor").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCloseDoor()
    {
        if (anim != null)
        {
            doorIsOpened = !doorIsOpened;
            anim.SetBool("character_nearby", doorIsOpened);

            /* Determinate where is the player in order to allow him to teleport into MainRoom and/or StorageRoom */
            storageRoom.GetComponent<TeleportationArea>().enabled = (doorIsOpened || xrOrigin.transform.position.z < roomSeparator);
            mainRoom.GetComponent<TeleportationArea>().enabled = (doorIsOpened || xrOrigin.transform.position.z > roomSeparator);
        }
        else
        {
            ComputerVoiceManager.instance.SayGlassDoorIsLocked(0.5f);
        }
        
    }
    
}
