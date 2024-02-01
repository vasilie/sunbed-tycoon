using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameMode
{
  Default,
  Building,
}

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneManager>();
                if (instance == null)
                {
                    // Create a new TimeManager GameObject if one doesn't exist
                    GameObject sceneManagerObject = new GameObject("SceneManager");
                    instance = sceneManagerObject.AddComponent<SceneManager>();
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

    public List<ConstructionObject> constructionObjectList = new List<ConstructionObject>();
    public Transform destinationA;
    public Transform destinationB;
    public Transform barPosition;
    public int id = 0;
    public TextMeshProUGUI textPrefab;
    // public enum

    public ConstructionObject GetUnocupiedConstructionObject()
    {
        for (int i = 0; i < constructionObjectList.Count; i++)
        {
            if (!constructionObjectList[i].isOccupied)
            {
                constructionObjectList[i].isOccupied = true;
                return constructionObjectList[i];
            }
        }
        return null;
    }

    public void SpawnMoneyInfo(Vector3 position, string text)
    {
        TextMeshProUGUI textObject = Instantiate(textPrefab, position, Quaternion.identity);
        // Set the text content
        textObject.text = "Hello, TextMeshPro!";
    }

    void Start()
    {

    }

    public int GetUniqueId()
    {
        id++;
        return id;
    }

}
