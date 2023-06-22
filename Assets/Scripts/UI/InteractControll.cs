using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractControll : MonoBehaviour
{

    public GameObject Alarm;
    public GameObject shop;
    public TextMeshProUGUI PriceHeal;
    int pricehp;
    public TextMeshProUGUI PriceDamage;
    int pricedm = 10;
    public TextMeshProUGUI DamageInfo;
    int countdm = 0;
    private bool ArchontNear = false;
    private bool paused = false;
    private bool BossDoorNear = false;
    bool exitnear = false;
    bool flowersnear = false;
    GameObject flower;
    [SerializeField] public AudioSource ShopAudio;
    [SerializeField] private AudioSource DashShopAudio;
    [SerializeField] private AudioSource DoobleJumpAudio;
    [SerializeField] private AudioSource DamageShopAudio;
    [SerializeField] private AudioSource HealAudio;
    [SerializeField] private AudioSource KnifeShopAudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ArchontNear)
        {
            shopwindow();
        }
        else if (Input.GetKeyDown(KeyCode.E) && BossDoorNear)
        {
            Camera.main.GetComponent<SaveLoadScript>().SaveGame();
            SceneManager.LoadScene(4);
        }
        else if (Input.GetKeyDown(KeyCode.E) && exitnear)
        {
            SceneManager.LoadScene(5);
        }
        else if (Input.GetKeyDown(KeyCode.E) && flowersnear)
        {
            flower.GetComponent<light>().useflower();
        }
    }

    public void shopwindow()
    {
        shop.SetActive(true);
        ShopAudio.Play();
        pricehp = Mathf.RoundToInt(((100 - gameObject.GetComponent<HeroMovement>().health) * 0.5f));
        PriceHeal.text = pricehp.ToString();
        PriceDamage.text = pricedm.ToString();
        DamageInfo.text = "Damage + 10 (" + countdm.ToString() + "/5)";
        Time.timeScale = 0f;
    }

    public void closeShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Archont":
                {
                    Alarm.SetActive(true);
                    ArchontNear = true;
                    break;
                }
            case "BossDoor":
                {
                    Alarm.SetActive(true);
                    BossDoorNear = true;
                    break;
                }
            case "exit":
                {
                    Alarm.SetActive(true);
                    exitnear = true;
                    break;
                }
            case "flowers":
                {
                    flower = other.gameObject;
                    Alarm.SetActive(true);
                    flowersnear = true;
                    break;
                }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Archont":
                {
                    Alarm.SetActive(false);
                    ArchontNear = false;
                    break;
                }
            case "BossDoor":
                {
                    Alarm.SetActive(false);
                    BossDoorNear = false;
                    break;
                }
            case "exit":
                {
                    Alarm.SetActive(false);
                    exitnear = false;
                    break;
                }
            case "flowers":
                {
                    Alarm.SetActive(false);
                    flowersnear = false;
                    break;
                }
        }
    }

    public void productUpgrade(GameObject button)
    {
        string name = button.name;
        switch (name)
        {
            case "BuyHeal":
                if (PlayerPrefs.GetInt("points") >= pricehp && GetComponent<HeroMovement>().health < 100)
                {
                    GetComponent<HeroMovement>().health = 100;
                    HealAudio.Play();
                    gameObject.GetComponent<HeroMovement>().SetPoint(pricehp);
                    pricehp = 0;
                    PriceHeal.text = pricehp.ToString();
                }
                break;
            case "UpgradeDamage":
                if (PlayerPrefs.GetInt("points") >= pricedm)
                {
                    gameObject.GetComponent<HeroMovement>().damage += 10;
                    DamageShopAudio.Play();
                    gameObject.GetComponent<HeroMovement>().SetPoint(pricedm);
                    countdm += 1;
                    pricedm += 5;
                    PriceDamage.text = pricedm.ToString();
                    DamageInfo.text = "Damage + 10 (" + countdm.ToString() + "/5)";
                    if (gameObject.GetComponent<HeroMovement>().damage == 70)
                    {
                        Transform temp_parent = button.transform.parent;
                        ShopSort(temp_parent);
                    }
                }
                break;
            case "UnlockDoubleJump":
                if (PlayerPrefs.GetInt("points") >= 0)
                {
                    gameObject.GetComponent<HeroMovement>().SetPoint(0);
                    gameObject.GetComponent<HeroMovement>().DoubleJumpAccses = true;
                    DoobleJumpAudio.Play();
                    Transform temp_parent = button.transform.parent;
                    ShopSort(temp_parent);
                }
                break;
            case "UnlockDash":
                if (PlayerPrefs.GetInt("points") >= 50)
                {
                    gameObject.GetComponent<HeroMovement>().DashAccses = true;
                    DashShopAudio.Play();
                    gameObject.GetComponent<HeroMovement>().SetPoint(50);
                    Debug.Log(button);
                    Transform temp_parent = button.transform.parent;
                    ShopSort(temp_parent);
                }
                break;
            case "BuyKnife":
                if (PlayerPrefs.GetInt("points") >= 50)
                {
                    gameObject.GetComponent<HeroMovement>().SetPoint(50);
                    gameObject.GetComponent<HeroMovement>().ThrowAccses = true;
                    KnifeShopAudio.Play();
                    Transform temp_parent = button.transform.parent;
                    Destroy(temp_parent.gameObject);
                }
                break;
        }
    }

    private void ShopSort(Transform child)
    {
        int n;
        for (n = 0; n < shop.transform.childCount; n++)
        {
            if (shop.transform.GetChild(n).name == child.name)
                break;
        }
        for (int i = shop.transform.childCount - 1; i >= n; i--)
        {
            Transform temp_child1 = shop.transform.GetChild(i);
            Transform temp_child2 = shop.transform.GetChild(i - 1);
            temp_child1.position = temp_child2.position;

        }
        Destroy(shop.transform.GetChild(n).gameObject);
    }
}
