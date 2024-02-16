using UnityEngine;
using UnityEngine.AI;

public class CustomerRoamingState : CustomerBaseState
{
    public float decisionTimeRemaining;
    public Transform currentDestination;

    public CustomerRoamingState()
    {
        stateName = "Roaming";
    }

    public override void EnterState(CustomerStateManager customer)
    {
        customer.navMeshAgent.enabled = true;
        decisionTimeRemaining = customer.decisionTime;
        currentDestination = customer.destinationA;
        Debug.Log(stateName);
    }

    public override void UpdateState(CustomerStateManager customer)
    {
        customer.navMeshAgent.destination = currentDestination.position;
        decisionTimeRemaining -= Time.deltaTime;


        if (decisionTimeRemaining <= 0)
        {
            if (customer.GetPlaceToChill())
            {
                customer.desire = Desire.OccupySunbed;
                customer.moveTarget = customer.placeToChill.transform;
                customer.SwitchState(customer.MoveToPositionState);
            }
            else
            {
                decisionTimeRemaining = customer.decisionTime;
            }
        }
    }

    public override void OnCollisionEnter(CustomerStateManager customer, Collision collision)
    {

    }
    public override void OnTriggerEnter(CustomerStateManager customer, Collider collider)
    {
        if (collider.gameObject.tag == "Destination" && currentDestination.position == collider.gameObject.transform.position)
        {
            ToggleDestination(customer);
        }
    }

    public void ToggleDestination(CustomerStateManager customer)
    {
        if (currentDestination == customer.destinationA)
        {
            currentDestination = customer.destinationB;
        }
        else
        {
            currentDestination = customer.destinationA;
        }
    }
}
