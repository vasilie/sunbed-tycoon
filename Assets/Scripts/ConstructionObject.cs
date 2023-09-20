using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObject : MonoBehaviour
{
    public BuildableObject buildableObject;
    public GameObject selectionObject;
    public GameObject displayObject;
    public GameObject placingDisplayObject;
    private EconomyManager economyManager;
    private SceneManager sceneManager;

    public bool isHovered = false;
    public bool isPlaced = false;
    public bool isOccupied = false;
    public int id;

    public void Start()
    {
        selectionObject = transform.Find("SelectionObject").gameObject;
        displayObject = transform.Find("DisplayObject").gameObject;
        placingDisplayObject = transform.Find("PlacingDisplayObject").gameObject;
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
                buildableObject.objectState = ObjectState.Placed;
                economyManager.RemoveMoney(buildableObject.buildingPrice, transform.position);
                isPlaced = true;
                sceneManager.constructionObjectList.Add(this);
                UpdateDisplay();
            }
        }

    }

    public void UpdateDisplay()
    {
        if (buildableObject.objectState == ObjectState.Placed)
        {
            displayObject.SetActive(true);
            selectionObject.SetActive(false);
            placingDisplayObject.SetActive(false);
        }
        if (buildableObject.objectState == ObjectState.Placing)
        {
            displayObject.SetActive(false);
            placingDisplayObject.SetActive(true);
            selectionObject.SetActive(true);
        }
        if (buildableObject.objectState == ObjectState.None)
        {
            displayObject.SetActive(false);
            selectionObject.SetActive(true);
            placingDisplayObject.SetActive(false);
        }
    }
}
