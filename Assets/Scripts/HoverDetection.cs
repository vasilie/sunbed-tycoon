using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverDetection : MonoBehaviour
{
    private static HoverDetection instance;

    public static HoverDetection Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HoverDetection>();
                if (instance == null)
                {
                    // Create a new TimeManager GameObject if one doesn't exist
                    GameObject hoverDetectionObject = new GameObject("HoverDetection");
                    instance = hoverDetectionObject.AddComponent<HoverDetection>();
                }
            }
            return instance;
        }
    }

    private List<GameObject> objectsToHover = new List<GameObject>();
    private GameObject currentlyHoveredObject;
    private EconomyManager economyManager;

    void Start()
    {
        economyManager = EconomyManager.Instance;
    }

    public void AddObjectToHoverCheck(GameObject obj)
    {
        // Add the object to the list of objects to check for hover.
        if (!objectsToHover.Contains(obj))
        {
            objectsToHover.Add(obj);
        }
        // You can also perform other initialization here if needed.
    }

    public void RemoveObjectToHoverCheck(GameObject obj)
    {
        // Remove the object from the list when it's no longer needed.
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

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is in the list of objects to check for hover.
            if (objectsToHover.Contains(hitObject) && !hitObject.GetComponent<ConstructionObject>().isHovered)
            {
                if (currentlyHoveredObject != hitObject && currentlyHoveredObject != null)
                {
                    currentlyHoveredObject.GetComponent<ConstructionObject>().UnHover();
                }
                ConstructionObject constructionObject = hitObject.GetComponent<ConstructionObject>();
                Debug.Log(constructionObject);
                Debug.Log(hitObject.name);
                currentlyHoveredObject = hitObject;
                constructionObject.Hover();
                // Handle hover enter on the currently hovered object.
            }
            if (!objectsToHover.Contains(hitObject))
            {
                currentlyHoveredObject.GetComponent<ConstructionObject>().UnHover();
            }

        }
        else
        {
            ConstructionObject constructionObject = currentlyHoveredObject.GetComponent<ConstructionObject>();
            constructionObject.UnHover();
            currentlyHoveredObject = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                // Check if the hit object is the one you want to interact with
                if (objectsToHover.Contains(hitObject))
                {
                    currentlyHoveredObject.GetComponent<ConstructionObject>().Place();
                }
            }
        }
    }
}
