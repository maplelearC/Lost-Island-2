using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{

    public static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }
    public bool hasBorder=true;
    public bool isPause = false;
    public int score = 0;
    public int length = 0;
    public Text msgText;
    public Text scoreText;
    public Image pauseImage;
    public Sprite[] pauseSprites;
    public Text lengthText;
    public Image bgImage;
    private Color tempColor;
    

    void Awake()
    {
        _instance = this;
    }


    void Start()
    {
       if (PlayerPrefs.GetInt("border", 1)==0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }


        }
    }
    void Update()
    {
        switch (score/100)
        {
            case 0:
            case 1:
            case 2:
                break;
            case 3:
                ColorUtility.TryParseHtmlString("#CCEEFFFF",out tempColor);
                bgImage.color =tempColor;
                msgText.text = "½×¶Î" + 2;
                break;
            case 5:
            case 6:
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "½×¶Î" + 3;
                break;
            case 7:
            case 8:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "½×¶Î" + 4;
                break;
            case 9:
            case 10:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "½×¶Î" + 5;
                break;
            default:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "ÎÞ¾¡½×¶Î";
                break;
        }

    }
    public void UpdateUI(int s=5,int l=1)
    {
        score += 5;
        length += 1;
        scoreText.text = "µÃ·Ö:\n" + score;
        lengthText.text = "³¤¶È;\n" + length;
    }
    public void Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            Time.timeScale = 0;
            pauseImage.sprite = pauseSprites[1];

        }
        else
        {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprites[0];
        }
    }
    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
