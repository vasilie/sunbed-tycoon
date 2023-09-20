using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private TimeManager timeManager;
    private EconomyManager economyManager;
    public Text timeText;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = TimeManager.Instance;
        economyManager = EconomyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = timeManager.timeInString;
        moneyText.text = "$" + economyManager.currentMoney.ToString();
    }
}
