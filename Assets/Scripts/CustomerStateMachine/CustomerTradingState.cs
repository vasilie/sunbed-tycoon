using UnityEngine;

public class CustomerTradingState : CustomerBaseState
{
  public override void EnterState(CustomerStateManager customer)
  {
    Debug.Log("CustomerTradingState");
    customer.properties.UpdateCustomerStateUI("Making trade");
    customer.inventory.BuyItem(customer.vendorInventory.SellItem(customer.desiredItem, 1));
    customer.properties.Drink(100f);
    customer.desire = customer.previousDesire;
    customer.SwitchState(customer.previousState); 
    customer.moveTarget = customer.previousMoveTarget;

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
