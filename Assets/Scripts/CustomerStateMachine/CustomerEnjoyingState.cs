using UnityEngine;

public class CustomerEnjoyingState : CustomerBaseState
{
    public EconomyManager economyManager;
    public float chillTime;
    public override void EnterState(CustomerStateManager customer)
    {
        economyManager = EconomyManager.Instance;
        customer.transform.position = customer.placeToChill.transform.parent.position;
        customer.navMeshAgent.enabled = false;
        chillTime = 10f;
        economyManager.AddMoney(20, customer.transform.position);
        customer.properties.UpdateCustomerStateUI("State: Enjoying");
    }
    public override void UpdateState(CustomerStateManager customer)
    {
        chillTime -= Time.deltaTime;

        if (chillTime <= 0)
        {
            customer.placeToChill.isOccupied = false;
            customer.placeToChill = null;


            customer.SwitchState(customer.RoamingState);
        }

    }

    public override void OnCollisionEnter(CustomerStateManager customer, Collision collision)
    {

    }
    public override void OnTriggerEnter(CustomerStateManager customer, Collider collider)
    {

    }
}
