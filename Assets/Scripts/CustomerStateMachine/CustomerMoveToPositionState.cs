using UnityEngine;

public class CustomerMoveToPositionState : CustomerBaseState
{
  public override void EnterState(CustomerStateManager customer)
  {
    customer.properties.UpdateCustomerStateUI("State: Move to position");
  }

  public override void UpdateState(CustomerStateManager customer)
  {
    customer.navMeshAgent.destination = customer.placeToChill.transform.parent.position;
  }

  public override void OnCollisionEnter(CustomerStateManager customer, Collision collision)
  {

  }

  public override void OnTriggerEnter(CustomerStateManager customer, Collider collider)
  {
    if (collider.gameObject.tag == "PlaceToOccupy" && collider.gameObject.transform.position == customer.placeToChill.transform.parent.position)
    {
      customer.SwitchState(customer.EnjoyingState);
    }
  }
}
