using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    private static EconomyManager instance;
    public static EconomyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EconomyManager>();
                if (instance == null)
                {
                    // Create a new TimeManager GameObject if one doesn't exist
                    GameObject economyManagerObject = new GameObject("EconomyManager");
                    instance = economyManagerObject.AddComponent<EconomyManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        // Ensure there's only one instance
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame updat
    public int currentMoney;
    public int population = 0;
    public EconomySO economy;
    public GameObject greenTextPrefab;
    public GameObject redTextPrefab;
    public int profit = 0;

    void Start()
    {
        currentMoney = economy.startingMoney;
    }

    public void AddObjectToPopulation(int value)
    {
        population += value;
    } 
    
    public void RemoveFromPopulation(int value)
    {
        population += value;
    }   

    public void AddMoney(int value, Vector3 position)
    {
        currentMoney += value;
        if (position != null)
        {
            SpawnMoneyInfo(position, value.ToString(), true);
        }
    }

    public void SpawnMoneyInfo(Vector3 position, string text, bool addition)
    {
        GameObject textObject;
        if (addition)
        {
            textObject = Instantiate(greenTextPrefab, position, Quaternion.identity);
            textObject.GetComponentInChildren<TextMeshPro>().text = "+$" + text;

        }
        else
        {
            textObject = Instantiate(redTextPrefab, position, Quaternion.identity);
            textObject.GetComponentInChildren<TextMeshPro>().text = "-$" + text;
        }
    }

    public void RemoveMoney(int value, Vector3 position)
    {
        if (currentMoney - value >= 0)
        {
            currentMoney -= value;
            Debug.Log("REMOVING MANI");
            if (position != null)
            {
                SpawnMoneyInfo(position, value.ToString(), false);
            }
        }
        else
        {
            Debug.Log("No more money");
        }
    }

}
