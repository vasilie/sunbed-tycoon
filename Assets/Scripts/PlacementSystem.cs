using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator;

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

    [SerializeField]
    private PreviewSystem preview;

    private GridData floorData, furnitureData;

    private List<GameObject> placedGameObjects = new List<GameObject>();

    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private EconomyManager economyManager;

    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        furnitureData = new GridData();
        economyManager = EconomyManager.Instance;
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
        preview.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        buildingsUIPanel.SetActive(false);
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    public void ToggleBuildingsUIPanel()
    {
        if (buildingsUIPanel.activeSelf)
        {
            buildingsUIPanel.SetActive(false);
            StopPlacement();
        }
        else
        {
            buildingsUIPanel.SetActive(true);
        }

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI()) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        bool haveEnoughMoney = database.objectsData[selectedObjectIndex].Price <= economyManager.currentMoney;
        Debug.Log($"mani{haveEnoughMoney}");
        if (placementValidity == false || haveEnoughMoney == false )
        {
            return;
        }
        
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab, placementParent.transform);
        Vector3 placementPosition = grid.CellToWorld(gridPosition) - new Vector3(0f, -13.55f, 1.85f);
        newObject.transform.position = placementPosition;
        economyManager.RemoveMoney(database.objectsData[selectedObjectIndex].Price, placementPosition);
        // Debug.Log($"Placement position in place structure {placementPosition}");
        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count - 1);
        preview.UpdatePosition(placementPosition, false);
    }

    private void Update()
    {
        if (selectedObjectIndex < 0) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (lastDetectedPosition != gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

            mouseIndicator.transform.position = mousePosition - new Vector3(0f, -13.55f, 1.85f);
            preview.UpdatePosition(grid.CellToWorld(gridPosition) - new Vector3(0f, -13.55f, 1.85f), placementValidity);
            lastDetectedPosition = gridPosition;
        }

    }
}
