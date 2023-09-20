using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]

public class CustomerSO : ScriptableObject
{
    public int adults;
    public int children;
    public int spendingLimit;
    public float decisionTime;
}
