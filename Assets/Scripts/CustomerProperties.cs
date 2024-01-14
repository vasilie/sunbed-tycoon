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
  public float thirstLevel = 100f;
  public float hungerLevel = 100f;
  public bool isHungry = false;
  public bool isThirsty = false;
  public float currentlifeTickTime = 10f;
  public float initialLifeTickTime = 10f;
  public CustomerStateManager Customer { get; set; }
  public TextMeshPro hungryText;
  public TextMeshPro thirstyText;
  public TextMeshPro stateText;
  public float thirstRate = 6.6f;
  public float hungerRate = 5.6f;

  public CustomerProperties(CustomerStateManager customer)
  {
    Customer = customer;
    hungryText = Customer.transform.Find("Billboard/HungryMeter").GetComponent<TextMeshPro>();
    thirstyText = Customer.transform.Find("Billboard/ThirstyMeter").GetComponent<TextMeshPro>();
    stateText = Customer.transform.Find("Billboard/State").GetComponent<TextMeshPro>();
  }


  public void UpdatePropertiesUI()
  {
    hungryText.text = "Hunger Level: " + hungerLevel;
    thirstyText.text = "Thirst Level: " + thirstLevel;
  }

  public void UpdateCustomerStateUI(string text)
  {
    stateText.text = text;
  }

  public void Update()
  {
    if (currentlifeTickTime <= 0)
    {
      thirstLevel -= thirstRate;
      hungerLevel -= hungerRate;

      if (thirstLevel <= 10f)
      {
        isThirsty = true;
      }
      if (hungerLevel <= 10f)
      {
        isHungry = true;
      }
      UpdatePropertiesUI();
      currentlifeTickTime = initialLifeTickTime;

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