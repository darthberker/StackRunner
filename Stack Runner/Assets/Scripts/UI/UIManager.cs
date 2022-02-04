using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject playButton;
    public GameObject levelText;
    public GameObject coinText;
    public GameObject upgradeButtonText;
    public GameObject upgradeCostText;
    public GameObject endPanelCoinText;
    public GameObject endPanel;
    public static bool gameEnd;
    public static bool check;
    private int upgradeCost;
    public static bool upgradeDone;
    [SerializeField] private GameObject character;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        
        
    }
    void Start()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.GetInt("CurrentLevel")-1 != y )
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel")-1);
        }
        Application.targetFrameRate = 60;
        animator = character.GetComponent<Animator>();
        upgradeCost = 25 + 25* PlayerPrefs.GetInt("UpgradeCount");
        /*
        PlayerPrefs.SetInt("GoldCount", 0); //to reset gold
        PlayerPrefs.SetInt("UpgradeCount", 0); //to reset upgrade
        PlayerPrefs.SetInt("CurrentLevel", 1); // to reset current level
        */
        if (!PlayerPrefs.HasKey("UpgradeCount"))
        {
            PlayerPrefs.SetInt("UpgradeCount", 0);
        }
        if (!PlayerPrefs.HasKey("GoldCount"))
        {
            PlayerPrefs.SetInt("GoldCount", 0);
        }
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        if (PlayerPrefs.GetInt("UpgradeCount") < 5)
        {
            upgradeButtonText.GetComponent<TextMeshProUGUI>().text = "Upgrade " + PlayerPrefs.GetInt("UpgradeCount").ToString() + "/5";
            upgradeCostText.GetComponent<TextMeshProUGUI>().text = "Upgrade Cost: " + upgradeCost;
        }
        else
        {
            upgradeCostText.GetComponent<TextMeshProUGUI>().text = " ";
            upgradeButtonText.GetComponent<TextMeshProUGUI>().text = "Upgrade Max";
        }
        coinText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("GoldCount").ToString();
        levelText.GetComponent<TextMeshProUGUI>().text = "Level " + PlayerPrefs.GetInt("CurrentLevel").ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CollectController.coinTake)
        {
            coinText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("GoldCount").ToString();
            CollectController.coinTake = false;
        }
        if (check)
        {
            PlayerPrefs.SetInt("GoldCount", PlayerPrefs.GetInt("GoldCount") + CollectController.coinCount);
            check = false;
        }
        if (gameEnd)
        {

            endPanel.SetActive(true);
            endPanelCoinText.GetComponent<TextMeshProUGUI>().text = CollectController.coinCount.ToString() + " coin collected";
            
            coinText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("GoldCount").ToString();
            
        }
        else
        {
            endPanel.SetActive(false);
        }
    }

    public void touchToPlay()
    {
        ForwardMovement.start = true;
        upgradeButton.SetActive(false);
        playButton.SetActive(false);

    }
    
    public void upgrade()
    {
        if (PlayerPrefs.GetInt("UpgradeCount")<5 &&PlayerPrefs.GetInt("GoldCount")>=upgradeCost)
        {
            
            PlayerPrefs.SetInt("GoldCount", PlayerPrefs.GetInt("GoldCount") - upgradeCost);
            coinText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("GoldCount").ToString();
            PlayerPrefs.SetInt("UpgradeCount", PlayerPrefs.GetInt("UpgradeCount") + 1);
            upgradeCost += 25;
            upgradeDone = true;
            if (PlayerPrefs.GetInt("UpgradeCount") < 5)
            {
                upgradeButtonText.GetComponent<TextMeshProUGUI>().text = "Upgrade " + PlayerPrefs.GetInt("UpgradeCount").ToString() + "/5";
                upgradeCostText.GetComponent<TextMeshProUGUI>().text = "Upgrade Cost: " + upgradeCost ;
            }
            else
            {
                upgradeCostText.GetComponent<TextMeshProUGUI>().text =  " ";
                upgradeButtonText.GetComponent<TextMeshProUGUI>().text = "Upgrade Max";
            }
            
        }
    }

    public void ReloadScene()
    {
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
        ForwardMovement.start = false;
        animator.SetBool("Replay", true);
        gameEnd = false;
        CollectController.coinCount = 0;
    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        if (PlayerPrefs.GetInt("CurrentLevel") > 2)
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel")-1);
        
        gameEnd = false;
        CollectController.coinCount =0;
    }

}
