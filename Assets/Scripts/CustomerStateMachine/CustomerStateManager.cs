using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Desire {
    OccupySunbed,
    Buy
}

public class CustomerStateManager : MonoBehaviour
{
    public CustomerBaseState currentState;
    public CustomerBaseState previousState = null;
    public CustomerRoamingState RoamingState = new CustomerRoamingState();
    public CustomerDecidingState DecidingState = new CustomerDecidingState();
    public CustomerEnjoyingState EnjoyingState = new CustomerEnjoyingState();
    public CustomerTradingState TradingState = new CustomerTradingState();
    public CustomerOccupyPlaceState OccupyPlace = new CustomerOccupyPlaceState();
    public CustomerMoveToPositionState MoveToPositionState = new CustomerMoveToPositionState();
    public NavMeshAgent navMeshAgent;
    public Transform destinationA;
    public Transform destinationB;
    public Transform barPosition;
    public Transform moveTarget;
    public Transform previousMoveTarget;
    public Desire desire;
    public Desire previousDesire;
    public InventorySlot desiredItem;
    public CustomerSO customer;
    public float decisionTime;
    public SceneManager sceneManager;
    public ConstructionObject placeToChill;
    public CustomerProperties properties;
    public InventoryComponent inventory;
    public bool hasTicket = false;
  
    public InventoryComponent vendorInventory;
    // Start is called before the first frame update
    void Start()
    {

        properties = new CustomerProperties(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<InventoryComponent>();
        vendorInventory = GetComponent<InventoryComponent>();
        currentState = RoamingState;
        sceneManager = SceneManager.Instance;
        destinationA = sceneManager.destinationA;
        destinationB = sceneManager.destinationB;
        barPosition = sceneManager.barPosition;
        decisionTime = customer.decisionTime;
        currentState.EnterState(this);
        properties.UpdateCustomerStateUI(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        properties.Update();
    }

    public bool GetPlaceToChill()
    {
        placeToChill = sceneManager.GetUnocupiedConstructionObject();
        // Debug.Log(placeToChill);
        if (placeToChill)
        {
            return true;
        }
        return false;
    }

    public void SwitchState(CustomerBaseState state)
    {
        if (currentState != MoveToPositionState) {
            previousState = currentState;
        }
        currentState = state;
        properties.UpdateCustomerStateUI(this);
        state.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }
}
