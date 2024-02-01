using UnityEngine;

public class CustomerOccupyPlaceState : CustomerBaseState
{
    public EconomyManager economyManager;

    public override void EnterState(CustomerStateManager customer)
    {
        economyManager = EconomyManager.Instance;
        customer.transform.position = customer.placeToChill.transform.position + new Vector3(3f, 3f, 3f);
        customer.navMeshAgent.enabled = false;
        economyManager.AddMoney(20, customer.transform.position);
        customer.properties.UpdateCustomerStateUI("State: Occupy Place");
        customer.SwitchState(customer.EnjoyingState);
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
