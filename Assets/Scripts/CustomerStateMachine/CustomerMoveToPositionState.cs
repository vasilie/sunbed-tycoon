using UnityEngine;

public class CustomerMoveToPositionState : CustomerBaseState
{
  public override void EnterState(CustomerStateManager customer)
  {
    customer.navMeshAgent.enabled = true;
    customer.properties.UpdateCustomerStateUI("State: Move to position");
  }

  public override void UpdateState(CustomerStateManager customer)
  {
    customer.navMeshAgent.destination = customer.moveTarget.position;
  }
// customer.placeToChill.transform.position
  public override void OnCollisionEnter(CustomerStateManager customer, Collision collision)
  {

  }

  public override void OnTriggerEnter(CustomerStateManager customer, Collider collider)
  {
    if (customer.desire == Desire.OccupySunbed) {
      if (collider.gameObject.tag == "PlaceToOccupy" && collider.gameObject.transform.position == customer.placeToChill.transform.position)
      {
        customer.SwitchState(customer.OccupyPlace);
      }
    }
    if (customer.desire == Desire.Buy) {
      Debug.Log("TAG" +collider.gameObject.tag);
      if (collider.gameObject.tag == "Bar")
      {
        customer.vendorInventory = collider.gameObject.GetComponent<InventoryComponent>();
        customer.previousMoveTarget = customer.moveTarget;
        customer.SwitchState(customer.TradingState);
      }
    }

  }
}
