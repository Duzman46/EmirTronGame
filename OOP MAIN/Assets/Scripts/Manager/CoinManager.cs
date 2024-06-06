using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int bank;
    public Text bankText;

    public static CoinManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //bank = PlayerPrefs.GetInt("CoinAmount", 0);
        bankText.text = "x " + bank.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        bankText.text = "x " + bank.ToString();
    }
    public void Money(int coinCollected)
    {
        bank += coinCollected;
        bankText.text = "x " + bank.ToString();
        //DataManager.instance.CurrenntCoin(bank);
        //bank = PlayerPrefs.GetInt("CoinAmount");
    }
}
