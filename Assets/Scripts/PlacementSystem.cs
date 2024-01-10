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

    private void Start() {
        StopPlacement();
    }

    public void StartPlacement(int ID) {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex < 0) {
            Debug.LogError($"No id found {ID}");
            return;
        }
        
        gridVisualisation.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void StopPlacement() {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void PlaceStructure() {
        if (inputManager.IsPointerOverUI()) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab, placementParent.transform);
        newObject.transform.position = grid.CellToWorld(gridPosition) - new Vector3(0f, -13.55f, 1.85f);
    }

    private void Update(){
        if (selectedObjectIndex < 0) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition) - new Vector3(0f, 0, 1.85f);
    }
}
