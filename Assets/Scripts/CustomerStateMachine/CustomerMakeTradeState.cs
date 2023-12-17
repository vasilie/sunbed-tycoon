using UnityEngine;

public class CustomerMakeTradeState : CustomerBaseState
{
  public override void EnterState(CustomerStateManager customer)
  {
    Debug.Log("CustomerMakeTradeState");
    customer.properties.UpdateCustomerStateUI("Making trade");
    // customer.vendorInventory.BuyItem(customer.desiredItem);

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
