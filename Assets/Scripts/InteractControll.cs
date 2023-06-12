using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractControll : MonoBehaviour
{

    public GameObject Alarm;
    public GameObject shop;
    private bool ArchontNear = false;
    private bool paused = false;
    private bool BossDoorNear = false;

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
            SceneManager.LoadScene(3);
        }
    }

    public void shopwindow()
    {
        shop.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closeShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Archont"))
        {
            Alarm.SetActive(true);
            ArchontNear = true;
        }
        else if (other.CompareTag("BossDoor"))
        {
            Alarm.SetActive(true);
            BossDoorNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Archont"))
        {
            Alarm.SetActive(false);
            ArchontNear = false;
        }
        else if (other.CompareTag("BossDoor"))
        {
            Alarm.SetActive(false);
            BossDoorNear = false;
        }
    }

    public void productUpgrade(GameObject button)
    {
        string name = button.name;
        switch (name)
        {
            case "UnlockDash":
                if (PlayerPrefs.GetInt("points") >= 50)
                {
                    gameObject.GetComponent<HeroMovement>().DashAccses = true;
                    gameObject.GetComponent<HeroMovement>().SetPoint(10);
                    Transform temp_parent = button.transform.parent;
                    temp_parent.gameObject.SetActive(false);
                }
                break;
            case "UnlockDoubleJump":
                if (PlayerPrefs.GetInt("points") >= 50)
                {
                    gameObject.GetComponent<HeroMovement>().SetPoint(10);
                    gameObject.GetComponent<HeroMovement>().DoubleJumpAccses = true;
                    Transform temp_parent = button.transform.parent;
                    temp_parent.gameObject.SetActive(false);
                }
                break;
            case "BuyHeal":
                if (PlayerPrefs.GetInt("points") >= 10 && GetComponent<HeroMovement>().health < 100)
                {
                    GetComponent<HeroMovement>().health += 10;
                    gameObject.GetComponent<HeroMovement>().SetPoint(10);
                }
                break;
            case "UpgradeDamage":
                if (PlayerPrefs.GetInt("points") >= 12)
                {
                    gameObject.GetComponent<HeroMovement>().damage += 5;
                    gameObject.GetComponent<HeroMovement>().SetPoint(12);
                    if (gameObject.GetComponent<HeroMovement>().damage == 100)
                    {
                        Transform temp_parent = button.transform.parent;
                        temp_parent.gameObject.SetActive(false);
                    }
                }
                break;
        }
    }
}
