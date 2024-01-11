using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualisation;

    [SerializeField]
    private GameObject placementParent;

    [SerializeField]
    private GameObject buildingsUIPanel;

    private GridData floorData, furnitureData;

    private Renderer previewRenderer;
    private List<GameObject> placedGameObjects = new List<GameObject>();

    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        furnitureData = new GridData();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No id found {ID}");
            return;
        }

        gridVisualisation.SetActive(true);
        buildingsUIPanel.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        buildingsUIPanel.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    public void ToggleBuildingsUIPanel()
    {
        if (buildingsUIPanel.activeSelf)
        {
            buildingsUIPanel.SetActive(false);
        }
        else
        {
            buildingsUIPanel.SetActive(true);
        }

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex) {
        Debug.Log($"Checking {gridPosition}");
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
        Debug.Log(selectedData);
        Debug.Log(selectedObjectIndex);
        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI()) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        if (placementValidity == false)
        {
            return;
        }
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab, placementParent.transform);
        newObject.transform.position = grid.CellToWorld(gridPosition) - new Vector3(0f, -13.55f, 1.85f);
        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count - 1);
    }

    private void Update()
    {
        if (selectedObjectIndex < 0) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
       
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition) - new Vector3(0f, 0, 1.85f);
    }
}
