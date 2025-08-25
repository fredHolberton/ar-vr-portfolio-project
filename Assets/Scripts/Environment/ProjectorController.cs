using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    /// <summary>
    /// Particule system to activate/desactivate
    /// </summary>
    [SerializeField] private GameObject particuleSystem = null;


    private bool particuleSystemActivated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particuleSystem.SetActive(false);
        particuleSystemActivated = false;
    }

    public void LightOnLightOff()
    {
        particuleSystem.SetActive(!particuleSystem.activeSelf);
        particuleSystemActivated = !particuleSystemActivated;

        if (particuleSystemActivated)
        {
            GameManager.instance.TheProjectorIsOn();
        }
        else
        {
            GameManager.instance.TheProjectorIsOff();
        }
    }
}
