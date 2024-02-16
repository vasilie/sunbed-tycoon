using UnityEngine;
using TMPro;

public enum CustomerType
{
  Default
}

public class CustomerProperties
{
  public CustomerType type = CustomerType.Default;
  public int currentMoney = 10;
  public int satisfactionLevel = 100;
  public float thirstLevel = Random.Range(1f, 100f);
  public float hungerLevel = Random.Range(1f, 100f);
  public bool isHungry = false;
  public bool isThirsty = false;
  public float currentlifeTickTime = 4f;
  public float initialLifeTickTime = 4f;
  public CustomerStateManager Customer { get; set; }
  public TextMeshPro hungryText;
  public TextMeshPro thirstyText;
  public TextMeshPro stateText;
  public TextMeshPro previousStateText;
  public float thirstRate = 3.2f;
  public float hungerRate = 2.1f;
  public bool hasBuyed = false;

  public CustomerProperties(CustomerStateManager customer)
  {
    Customer = customer;
    hungryText = Customer.transform.Find("Billboard/HungryMeter").GetComponent<TextMeshPro>();
    thirstyText = Customer.transform.Find("Billboard/ThirstyMeter").GetComponent<TextMeshPro>();
    stateText = Customer.transform.Find("Billboard/State").GetComponent<TextMeshPro>();
    previousStateText = Customer.transform.Find("Billboard/PreviousState").GetComponent<TextMeshPro>();
  }


  public void UpdatePropertiesUI()
  {
    hungryText.text = "Hunger Level: " + hungerLevel;
    thirstyText.text = "Thirst Level: " + thirstLevel;
  }

  public void UpdateCustomerStateUI(CustomerStateManager customer)
  {
    stateText.text = "State: " + customer.currentState.stateName;
    previousStateText.text = "Last State: " + customer.previousState.stateName;
  }

  public void Update()
  {
    if (currentlifeTickTime <= 0)
    {
      if (thirstLevel > 0) {
        thirstLevel -= thirstRate;
      }      
      
      if (hungerLevel > 0) {
        hungerLevel -= hungerRate;
      }
    

      if (thirstLevel <= 10f)
      {
        isThirsty = true;
      } else {
        isThirsty = false;
      }
      if (hungerLevel <= 10f)
      {
        isHungry = true;
      } else {
        isHungry = false;
      }
      UpdatePropertiesUI();
      currentlifeTickTime = initialLifeTickTime;

    }
    if (isThirsty == true) {
      hasBuyed = false;
      Debug.Log("isThirsty: " + isThirsty);
      Customer.previousMoveTarget = Customer.moveTarget; 
      ChangeDesire(Desire.Buy);
      Customer.moveTarget = Customer.barPosition.transform;
      Customer.SwitchState(Customer.MoveToPositionState);
    }
    currentlifeTickTime -= Time.deltaTime;
  }

  public void Eat(float value)
  {
    if (hungerLevel + value > 100f)
    {
      hungerLevel = 100f;
    }
    else
    {
      hungerLevel += value;
    }
  }

  public void ChangeDesire(Desire desire) {
    if (desire != Customer.desire){
      if (Customer.previousDesire != desire) {
        Customer.previousDesire = Customer.desire;
      }
      Customer.desire = desire;
    }
  }

  public void Drink(float value)
  {
    if (thirstLevel + value > 100f)
    {
      thirstLevel = 100f;
    }
    else
    {
      thirstLevel += value;
    }
  }

}