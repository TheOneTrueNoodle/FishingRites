using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FishingRodScript : MonoBehaviour
{
    [SerializeField] private float BoundingDistance = 7.5f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        float mouseX = cam.ScreenToWorldPoint(Input.mousePosition).x;
        if(mouseX > -BoundingDistance && mouseX < BoundingDistance)
        {
            transform.position = new Vector3(mouseX, transform.position.y, transform.position.z);
        }
        else if(mouseX < -BoundingDistance)
        {
            transform.position = new Vector3(-BoundingDistance, transform.position.y, transform.position.z);
        }
        else if(mouseX > BoundingDistance)
        {
            transform.position = new Vector3(BoundingDistance, transform.position.y, transform.position.z);
        }
    }
}
