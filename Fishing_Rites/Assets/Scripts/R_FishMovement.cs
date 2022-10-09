using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FishMovement : MonoBehaviour
{
    public float LeftMaxX, RightMaxX;
    public float MoveSpeed;

    private float NextPosX;
    private Vector3 NextPos;

    private void Start()
    {
        NextPosX = Random.Range(LeftMaxX, RightMaxX);
        NextPos = new Vector3(NextPosX, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(transform.position != NextPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, NextPos, MoveSpeed * Time.deltaTime);
        }
        else
        {
            NextPosX = Random.Range(LeftMaxX, RightMaxX);
            NextPos = new Vector3(NextPosX, transform.position.y, transform.position.z);
        }
    }
}
