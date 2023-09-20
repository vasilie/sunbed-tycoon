using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    private TimeManager timeManager;
    private float rotationAngle = 0f;
    private float offsetAngle = 270f; // This is to match rotation to the current time, at 12 noon directional light should point straight down
    private float movementAngle = 360f / 1440f;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = TimeManager.Instance;

    }

    void Update()
    {
        rotationAngle = movementAngle * timeManager.currentTimeOfDay;
        transform.rotation = Quaternion.Euler(new Vector3(rotationAngle + offsetAngle, -104f, 12f));
    }

}
