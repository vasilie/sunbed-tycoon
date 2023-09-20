using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buildable Object", menuName = "Construction/BeachObject")]
public class BeachObject : BuildableObject
{

  public void Awake()
  {
    objectState = ObjectState.None;
  }
}
