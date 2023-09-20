using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelSO levelSo;
    private float remainingSpawnTime;
    public GameObject customer;

    void Start()
    {
        remainingSpawnTime = levelSo.customerSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingSpawnTime -= Time.deltaTime;
        if (remainingSpawnTime <= 0)
        {
            Instantiate(customer, transform.position, Quaternion.identity);
            remainingSpawnTime = levelSo.customerSpawnTime;
        }
    }
}
