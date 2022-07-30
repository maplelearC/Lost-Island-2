using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{
    public static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }
    public int xlimit = 19;
    public int ylimit = 11;
    public int xoffset = 6;
    public GameObject foodPrefab;
    public GameObject RewardPrefab;
    public Sprite[] foodSprites;
    private Transform Foodholder;

    void Awake()
    {
        _instance = this; 
    }
    void Start()
    {
        Foodholder = GameObject.Find("FoodHolder").transform;
        MakeFood(false);
    }
    public void MakeFood(bool isReward)
    {
        int index = Random.Range(0, foodSprites.Length);
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite = foodSprites[index];
        food.transform.SetParent(Foodholder, false);
        int x = Random.Range(-xlimit + xoffset, xlimit);
        int y = Random.Range(-ylimit, ylimit);
        food.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        if (isReward)
        {
            GameObject Reward = Instantiate(RewardPrefab);
            Reward.transform.SetParent(Foodholder, false);
            x = Random.Range(-xlimit + xoffset, xlimit);
            y = Random.Range(-ylimit, ylimit);
            Reward.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }
    }
}
