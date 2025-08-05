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

    /* State of the door */
    private bool doorIsOpened;

    /* Animator */
    private Animator anim;

    /* position of room divider */
    private float roomSeparator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorIsOpened = false;
        anim = GetComponent<Animator>();
        roomSeparator = GameObject.Find("GlassDoor").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCloseDoor()
    {
        doorIsOpened = !doorIsOpened;
        anim.SetBool("character_nearby", doorIsOpened);

        /* Determinate where is the player in order to allow him to teleport into MainRoom and/or StorageRoom */
        storageRoom.GetComponent<TeleportationArea>().enabled = (doorIsOpened || xrOrigin.transform.position.z < roomSeparator);
        mainRoom.GetComponent<TeleportationArea>().enabled = (doorIsOpened || xrOrigin.transform.position.z > roomSeparator);
    }
    
}
