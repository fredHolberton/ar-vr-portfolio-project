using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    /// <summary>
    /// Particule system to activate/desactivate
    /// </summary>
    [SerializeField] private GameObject particuleSystem = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particuleSystem.SetActive(false);
    }

    public void LightOnLightOff()
    {
        particuleSystem.SetActive(!particuleSystem.activeSelf);
    }
}
