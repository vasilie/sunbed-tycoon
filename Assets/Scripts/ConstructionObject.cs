using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObject : MonoBehaviour
{
    public BuildableObject buildableObject;
    public GameObject selectionObject;
    public GameObject displayObject;
    public GameObject placingDisplayObject;
    public GameObject constructionObject;
    private EconomyManager economyManager;
    private SceneManager sceneManager;

    public bool isHovered = false;
    public bool isPlaced = false;
    public bool isOccupied = false;
    public bool isConstructed = false;
    public bool isBeingConstructed = false;
    public float buildTime = 10f;
    public int id;

    public void Start()
    {
        selectionObject = transform.Find("SelectionObject").gameObject;
        displayObject = transform.Find("DisplayObject").gameObject;
        placingDisplayObject = transform.Find("PlacingDisplayObject").gameObject;
        constructionObject = transform.Find("ConstructionObject").gameObject;
        economyManager = EconomyManager.Instance;
        sceneManager = SceneManager.Instance;
        id = sceneManager.GetUniqueId();
    }

    public void Hover()
    {
        if (!isHovered)
        {
            buildableObject.objectState = ObjectState.Placing;
            Debug.Log("HOVERERERD");
            isHovered = true;
            UpdateDisplay();
        }
    }

    public void UnHover()
    {
        if (isHovered && buildableObject.objectState != ObjectState.Placed)
        {
            buildableObject.objectState = ObjectState.None;
            isHovered = false;
            UpdateDisplay();
        }
    }

    public void Place()
    {
        if (economyManager.currentMoney - buildableObject.buildingPrice >= 0)
        {
            if (buildableObject.objectState != ObjectState.Placed)
            {
                buildableObject.objectState = ObjectState.Constructing;
                economyManager.RemoveMoney(buildableObject.buildingPrice, transform.position);
                isPlaced = true;
                StartConstructing();
                UpdateDisplay();
            }
        }

    }

    public void StartConstructing()
    {
        isBeingConstructed = true;
    }

    public void UpdateDisplay()
    {
        if (buildableObject.objectState == ObjectState.Placed)
        {
            displayObject.SetActive(true);
            selectionObject.SetActive(false);
            placingDisplayObject.SetActive(false);
            constructionObject.SetActive(false);
        }
        if (buildableObject.objectState == ObjectState.Placing)
        {
            if (!isConstructed && !isBeingConstructed){
                displayObject.SetActive(false);
                selectionObject.SetActive(true);
            }
            if (!isBeingConstructed && !isConstructed){
                placingDisplayObject.SetActive(true);
            }
            
        }
        if (buildableObject.objectState == ObjectState.Constructing)
        {
            displayObject.SetActive(false);
            selectionObject.SetActive(true);
            placingDisplayObject.SetActive(false);
            constructionObject.SetActive(true);
        }        
        if (buildableObject.objectState == ObjectState.None)
        {
            selectionObject.SetActive(true);
            placingDisplayObject.SetActive(false);
        }
    }

    public void Update()
    {
        
        Debug.Log(buildTime);
        Debug.Log(buildableObject.objectState);
        if (isBeingConstructed && buildTime > 0)
        {
            buildTime -= 10 * Time.deltaTime;
        }

        if (buildTime <= 0) {
            if (!isConstructed){
                sceneManager.constructionObjectList.Add(this);
                buildableObject.objectState = ObjectState.Placed;
                isConstructed = true;
                UpdateDisplay();
            }
            
        }
    }
}
