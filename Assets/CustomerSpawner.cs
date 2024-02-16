using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelSO levelSo;
    private float remainingSpawnTime;
    public int maxCustomers = 20;
    public GameObject customer;
    private EconomyManager economyManager;

    void Start()
    {
        remainingSpawnTime = levelSo.customerSpawnTime;
        economyManager = EconomyManager.Instance;
        SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        remainingSpawnTime -= Time.deltaTime;
        if (remainingSpawnTime <= 0 && economyManager.population < maxCustomers)
        {
          SpawnCustomer();
        }
    }

    void SpawnCustomer() {
        Instantiate(customer, transform.position, Quaternion.identity);
        economyManager.AddObjectToPopulation(1);
        remainingSpawnTime = levelSo.customerSpawnTime;
    }
}
