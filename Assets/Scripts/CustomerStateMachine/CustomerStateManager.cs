using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerStateManager : MonoBehaviour
{
    CustomerBaseState currentState;
    public CustomerRoamingState RoamingState = new CustomerRoamingState();
    public CustomerDecidingState DecidingState = new CustomerDecidingState();
    public CustomerEnjoyingState EnjoyingState = new CustomerEnjoyingState();
    public CustomerMoveToPositionState MoveToPositionState = new CustomerMoveToPositionState();
    public NavMeshAgent navMeshAgent;
    public Transform destinationA;
    public Transform destinationB;
    public CustomerSO customer;
    public float decisionTime;
    public SceneManager sceneManager;
    public ConstructionObject placeToChill;
    public CustomerProperties properties;
    public Inventory inventory;
    public InventorySlot desiredItem;
    public Inventory vendorInventory;
    // Start is called before the first frame update
    void Start()
    {

        properties = new CustomerProperties(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<InventoryComponent>().inventory;
        currentState = RoamingState;
        sceneManager = SceneManager.Instance;
        destinationA = sceneManager.destinationA;
        destinationB = sceneManager.destinationB;
        decisionTime = customer.decisionTime;
        currentState.EnterState(this);
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
        Debug.Log(placeToChill);
        if (placeToChill)
        {
            return true;
        }
        return false;
    }

    public void SwitchState(CustomerBaseState state)
    {
        currentState = state;
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
