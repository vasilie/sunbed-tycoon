using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeHovered : MonoBehaviour
{
    private HoverDetection hoverDetection;
    // Start is called before the first frame update
    void Start()
    {
        hoverDetection = HoverDetection.Instance;
        hoverDetection.AddObjectToHoverCheck(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
