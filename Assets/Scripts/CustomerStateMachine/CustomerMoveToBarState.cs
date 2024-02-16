using UnityEngine;

public class CustomerMovegToBarState : CustomerBaseState
{
  public string stateName = "MovegToBar State";
  public override void EnterState(CustomerStateManager customer)
  {
    Debug.Log("MoveToBarState");
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
