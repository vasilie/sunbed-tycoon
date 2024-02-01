using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 16.06f;
    
    [SerializeField]
    private Vector3 cursorOffset;

    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;

    [SerializeField]
    private GameObject groundParent;

    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;



    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();

    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab, groundParent.transform);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 && size.y > 0)
        {
            Debug.Log($"SOZE{size}");
            GameObject cell = cellIndicator.transform.GetChild(0).gameObject;
            Debug.Log($"SOZE{cell.name}");
            Vector3 poss =  new Vector3((float)size.x, 0.35f, (float)size.y);
            Debug.Log($"poss{poss}");
            cell.transform.localPosition = poss;
            cell.transform.localScale = new Vector3((float)size.x / 5f, 1f, (float)size.y / 5f);
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        if (previewObject.GetComponent<ConstructionObject>() != null) {
            previewObject.GetComponent<ConstructionObject>().enabled = false;
        }
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }

            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.3f;
        cellIndicatorRenderer.material.color = c;
        c.a = 0.6f;    
        previewMaterialInstance.color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = position;
    }

}
