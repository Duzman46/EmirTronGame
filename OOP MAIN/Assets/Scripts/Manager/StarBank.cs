using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBank : MonoBehaviour
{
    public int bankStar;
    public static StarBank instance;
    public Text bankText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        bankStar = PlayerPrefs.GetInt("StarAmount", 0);
        bankText.text = "x " + bankStar.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        bankText.text = "x " + bankStar.ToString();
    }
    public void Collect(int starCollected)
    {
        bankStar += starCollected;
        bankText.text = "x " + bankStar.ToString();
        //DataManager.instance.CurrentStars(bankStar);
        //bankStar = PlayerPrefs.GetInt("StarAmount");
    }
}
