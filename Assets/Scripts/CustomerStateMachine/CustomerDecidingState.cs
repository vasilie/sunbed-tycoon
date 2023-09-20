using UnityEngine;

public class CustomerDecidingState : CustomerBaseState
{
    public override void EnterState(CustomerStateManager customer)
    {
        Debug.Log("Decision state");
    }
    public override void UpdateState(CustomerStateManager customer)
    {

    }
    public override void OnCollisionEnter(CustomerStateManager customer, Collision collision)
    {

    }

    public override void OnTriggerEnter(CustomerStateManager customer, Collider collider)
    {

    }
}
