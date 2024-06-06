using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Experience : MonoBehaviour
{
    public Image expImg;
    public Text levelText;
    public int currentLevel;
    public AudioSource levelUpAS;

    [HideInInspector]
    public float currentExperience;
    public float expToNextLevel;

    public static Experience instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        currentLevel = 1;
        levelText.text = currentLevel.ToString();
        currentExperience = PlayerPrefs.GetFloat("Experience", 0);
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);
    }

    // Update is called once per frame
    void Update()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        levelText.text = currentLevel.ToString();
    }
    public void expMod(float experience)
    {
        currentExperience += experience;
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);

        expImg.fillAmount = currentExperience / expToNextLevel;
        if (currentExperience>=expToNextLevel)
        {
            expToNextLevel += 30;
            currentExperience = 0;
            currentLevel++;
            levelText.text = currentLevel.ToString();
            AudioManager.instance.PlayAudio(levelUpAS);

            //currentLevel = PlayerPrefs.GetInt("CurrentLevel", currentLevel);
        }
        
    }

    void levelStatUp(float choosenStat)
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (choosenStat == 1)
            player.damage += 5;
        if (choosenStat == 2)
            player.speed += 0.5f;
        if (choosenStat == 3)
            PlayerHealth.instance.defense += 5;
        if (choosenStat == 4)
        {
            PlayerHealth.instance.maxHealth += 10;
            PlayerHealth.instance.currentHealth += 10;
        }
    }

    public void DataSave()
    {
        DataManager.instance.ExperinceData(currentExperience);
        DataManager.instance.ExperienceToNextLevel(expToNextLevel);
        DataManager.instance.LevelData(currentLevel);

        DataManager.instance.CurrentHealth(PlayerHealth.instance.currentHealth);
        PlayerHealth.instance.currentHealth = PlayerPrefs.GetFloat("CurrentHealth");
        DataManager.instance.MaxHealth(PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.maxHealth = PlayerPrefs.GetFloat("MaxHealth");

        currentExperience = PlayerPrefs.GetFloat("Experience");
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        DataManager.instance.CurrentStars(StarBank.instance.bankStar);
        StarBank.instance.bankStar = PlayerPrefs.GetInt("StarAmount");

        DataManager.instance.CurrenntCoin(CoinManager.instance.bank);
        CoinManager.instance.bank = PlayerPrefs.GetInt("CoinAmount");

        GameData.instance.ClearAllDataList();
        GameManagerTwo.Instance.GetComponent<Inventory>().InvertoryToData();
        GameData.instance.Save();
    }
}
