using UnityEngine;

public class CustomerTradingState : CustomerBaseState
{
  public EconomyManager economyManager;

  public CustomerTradingState()
  {
    stateName = "Trading";
  }

  public override void EnterState(CustomerStateManager customer)
  {
    economyManager = EconomyManager.Instance;
    if (!customer.properties.hasBuyed && customer.vendorInventory.CheckIfItemCanBeSold(customer.desiredItem))
    {
      customer.vendorInventory.SellItem(customer.desiredItem, 1);  
      customer.inventory.BuyItem(customer.desiredItem, 1);
      economyManager.AddMoney(customer.desiredItem.item.price, customer.transform.position);
      customer.properties.hasBuyed = true;
      customer.properties.Drink(100f);
      customer.properties.ChangeDesire(customer.previousDesire);
      customer.moveTarget = customer.previousMoveTarget;
      customer.SwitchState(customer.previousState);
    }



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
