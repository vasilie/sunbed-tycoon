using UnityEngine;

public class CustomerEnjoyingState : CustomerBaseState
{
    public float chillTime;

    public CustomerEnjoyingState()
    {
        stateName = "Enjoying";
    }

    public override void EnterState(CustomerStateManager customer)
    {
        chillTime = 60f;
    }
    public override void UpdateState(CustomerStateManager customer)
    {
        chillTime -= Time.deltaTime;
        // Debug.Log(Mathf.Abs((customer.gameObject.transform.position - customer.placeToChill.transform.position).sqrMagnitude));

       if ((customer.gameObject.transform.position - customer.placeToChill.transform.position).sqrMagnitude > 80f ) {
            customer.desire = Desire.OccupySunbed;
            customer.moveTarget = customer.placeToChill.transform;
            customer.SwitchState(customer.MoveToPositionState);
        }
        if (chillTime <= 0)
        {
            customer.placeToChill.isOccupied = false;
            customer.hasTicket = false;
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
