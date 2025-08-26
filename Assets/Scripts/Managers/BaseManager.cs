using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager instance = null;

    [SerializeField] private Animator baseImmersionController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        baseImmersionController.enabled = false;
    }

    public void PlayBaseImmersion()
    {
        Debug.Log("Play base immersion");
        baseImmersionController.enabled = true;
    }

    public void EndOfAnimation()
    {
        GameManager.instance.Exit();
    }
}
