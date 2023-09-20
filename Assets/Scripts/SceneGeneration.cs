using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGeneration : MonoBehaviour
{
    public LevelSO level;
    public GameObject sunbed;
    public GameObject startPosition;
    public GameObject parent;
    private Vector3 currentParentPosition;
    private Quaternion currentParentRotation;
    void Start()
    {
        currentParentPosition = parent.transform.position;
        currentParentRotation = parent.transform.rotation;
        parent.transform.position = new Vector3(0, 0, 0);
        parent.transform.rotation = Quaternion.identity;
        for (int x = 0; x < level.sizeX; x++)
        {
            for (int z = 0; z < level.sizeZ; z++)
            {
                GameObject bed = Instantiate(sunbed, new Vector3(startPosition.transform.position.x + level.offsetX * x, startPosition.transform.position.y, startPosition.transform.position.z + level.offsetZ * z), Quaternion.identity, parent.transform);
            }
        }
        parent.transform.position = currentParentPosition;
        parent.transform.rotation = currentParentRotation;
    }

    void Update()
    {

    }
}
