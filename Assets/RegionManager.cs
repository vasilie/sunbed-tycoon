using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private GameObject regionPrefab;

    [SerializeField]
    private GameObject groundParent;

    [SerializeField]
    private Vector2Int regionSize;

    [SerializeField]
    private Vector2Int regionGridSize;

    [SerializeField]
    private Vector3Int startingPosition;

    private Vector3 offset = new Vector3(-23f - 0.03f, -22.199303f - 14f + 3.430853f, 31.85f + 0.42636f - 0.56f);



    void Start()
    {
        for (int x = startingPosition.x; x < regionGridSize.x + startingPosition.x; x++)
        {
            for (int z = startingPosition.z; z < regionGridSize.y + startingPosition.z; z++)
            {
                Vector3 regionSegmentPositionposition = grid.CellToWorld(new Vector3Int(x, 0, z)) - offset ;
                GameObject regionSegment = Instantiate(regionPrefab, gameObject.transform);
                regionSegment.transform.localPosition = new Vector3(regionSegmentPositionposition.x, 14f, regionSegmentPositionposition.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
