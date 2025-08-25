using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChessBoadController : MonoBehaviour
{
    [SerializeField] private GameObject bishopLight = null;
    [SerializeField] private GameObject knightLight = null;
    [SerializeField] private GameObject pawnDark = null;
    [SerializeField] private GameObject rookDark = null;
    [SerializeField] private GameObject[] chessPieces = null;
    [SerializeField] private Sprite bishopLightImage = null;
    [SerializeField] private Sprite knightLightImage = null;
    [SerializeField] private Sprite pawnDarkImage = null;
    [SerializeField] private Sprite rookDarkImage = null;

    [SerializeField] private TextMeshProUGUI nbFoundPiecesText = null;
    private int speed = 45;

    private int nbFoundPieces;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nbFoundPiecesText.text = "0";
        nbFoundPieces = 0;
        for (int i = 0; i < 4; i++)
        {
            chessPieces[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnAround()
    {
        transform.Rotate(Vector3.up * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "BishopLight" || other.gameObject.name == "KnightLight" || other.gameObject.name == "PawnDark" || other.gameObject.name == "RookDark")
        {

            if (other.gameObject.name == "BishopLight")
            {
                other.gameObject.SetActive(false);
                bishopLight.SetActive(true);
                chessPieces[nbFoundPieces].GetComponent<Image>().sprite = bishopLightImage;
            }
            else if (other.gameObject.name == "KnightLight")
            {
                other.gameObject.SetActive(false);
                knightLight.SetActive(true);
                chessPieces[nbFoundPieces].GetComponent<Image>().sprite = knightLightImage;

            }
            else if (other.gameObject.name == "PawnDark")
            {
                other.gameObject.SetActive(false);
                pawnDark.SetActive(true);
                chessPieces[nbFoundPieces].GetComponent<Image>().sprite = pawnDarkImage;
            }
            else if (other.gameObject.name == "RookDark")
            {
                other.gameObject.SetActive(false);
                rookDark.SetActive(true);
                chessPieces[nbFoundPieces].GetComponent<Image>().sprite = rookDarkImage;
            }
            chessPieces[nbFoundPieces].SetActive(true);
            nbFoundPieces += 1;

            if (nbFoundPieces == 4)
            {
                GameManager.instance.ChessboardComplete();
            }
                
        }
        

        nbFoundPiecesText.text = string.Format("{0}", nbFoundPieces);
    }
}
