using UnityEngine;
using UnityEngine.AI;

public abstract class CustomerBaseState
{
    public string stateName;
    public abstract void EnterState(CustomerStateManager customer);

    public abstract void UpdateState(CustomerStateManager customer);

    public abstract void OnCollisionEnter(CustomerStateManager customer, Collision collision);

    public abstract void OnTriggerEnter(CustomerStateManager customer, Collider collider);
}
