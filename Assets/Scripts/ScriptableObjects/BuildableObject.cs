using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildableObject : ScriptableObject
{
  public GameObject prefab;
  public int buildingPrice;
  public ObjectState objectState;
  [TextArea(15, 20)]
  public string decription;
}
