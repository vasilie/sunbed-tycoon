using UnityEngine;

public class CustomerMoveToBarState : CustomerBaseState
{
  public override void EnterState(CustomerStateManager customer)
  {
    Debug.Log("MoveToBarState");
    customer.properties.UpdateCustomerStateUI("Moving to Bar");
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
