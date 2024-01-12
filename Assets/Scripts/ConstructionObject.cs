using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
  None,
  Placed,
  Placing,
  Constructing,
}

public class ConstructionObject : MonoBehaviour
{
    public GameObject selectionObject;
    public GameObject displayObject;
    public GameObject placingDisplayObject;
    public GameObject constructionObject;
    private EconomyManager economyManager;
    private SceneManager sceneManager;
    private ObjectState state;

    public bool isHovered = false;
    public bool isPlaced = false;
    public bool isOccupied = false;
    public bool isConstructed = false;
    public bool isBeingConstructed = false;
    public float buildTime = 10f;
    public int id;


   

    // public void Hover()
    // {
    //     if (!isHovered)
    //     {
    //         state = ObjectState.Placing;
    //         Debug.Log("HOVERERERD");
    //         isHovered = true;
    //         UpdateDisplay();
    //     }
    // }

    // public void UnHover()
    // {
    //     if (isHovered && state != ObjectState.Placed)
    //     {
    //         state = ObjectState.None;
    //         isHovered = false;
    //         UpdateDisplay();
    //     }
    // }

    public void Place()
    {
        Debug.Log("PLACED MATORE>?");

        if (state != ObjectState.Placed)
        {
            state = ObjectState.Constructing;
            // economyManager.RemoveMoney(buildableObject.buildingPrice, transform.position);
            StartConstructing();
            UpdateDisplay();
        }
    }

    public void StartConstructing()
    {
        isBeingConstructed = true;
    }

    public void UpdateDisplay()
    {
        // Debug.Log("UPDATE DISPLAY");
        if (state == ObjectState.Placed)
        {
            displayObject.SetActive(true);
            selectionObject.SetActive(false);
            placingDisplayObject.SetActive(false);
            constructionObject.SetActive(false);
        }
        if (state == ObjectState.Placing)
        {
            if (!isConstructed && !isBeingConstructed){
                displayObject.SetActive(false);
                selectionObject.SetActive(true);
            }
            if (!isBeingConstructed && !isConstructed){
                placingDisplayObject.SetActive(true);
            }
            
        }
        if (state == ObjectState.Constructing)
        {
            displayObject.SetActive(false);
            selectionObject.SetActive(true);
            placingDisplayObject.SetActive(false);
            constructionObject.SetActive(true);
        }        
        if (state == ObjectState.None)
        {
            selectionObject.SetActive(true);
            placingDisplayObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (isBeingConstructed && buildTime > 0)
        {
            buildTime -= 10 * Time.deltaTime;
        }

        if (buildTime <= 0) {
            if (!isConstructed){
                sceneManager.constructionObjectList.Add(this);
                state = ObjectState.Placed;
                isConstructed = true;
                UpdateDisplay();
            }
            
        }
    }
    public void Start() {
        Debug.Log("Start os mpt wprlomg");
        // selectionObject = transform.Find("SelectionObject").gameObject;
        // displayObject = transform.Find("DisplayObject").gameObject;
        // placingDisplayObject = transform.Find("PlacingDisplayObject").gameObject;
        // constructionObject = transform.Find("ConstructionObject").gameObject;
        economyManager = EconomyManager.Instance;
        sceneManager = SceneManager.Instance;
        isBeingConstructed = true;
        state = ObjectState.Constructing;
        UpdateDisplay();
    }
}

