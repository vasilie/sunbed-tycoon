using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public int sizeX;
    public int sizeZ;
    public int offsetX;
    public int offsetZ;
    public float customerSpawnTime;
}
