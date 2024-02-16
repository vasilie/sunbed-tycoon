using UnityEngine;

public class CustomerMakeTradeState : CustomerBaseState
{
  public string stateName = "MakeTrade State";
  public override void EnterState(CustomerStateManager customer)
  {
    Debug.Log("CustomerMakeTradeState");

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
