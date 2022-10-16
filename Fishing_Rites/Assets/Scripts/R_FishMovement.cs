using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FishMovement : MonoBehaviour
{
    public float LeftMaxX, RightMaxX;
    public float MaxMoveSpeed = 5f;
    public float MinMoveSpeed = 1f;
    private float currentMoveSpeed;

    private float NextPosX;
    private Vector3 NextPos;

    private void Start()
    {
        currentMoveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
        NextPosX = Random.Range(LeftMaxX, RightMaxX);
        NextPos = new Vector3(NextPosX, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(transform.position != NextPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, NextPos, currentMoveSpeed * Time.deltaTime);
        }
        else
        {
            currentMoveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
            NextPosX = Random.Range(LeftMaxX, RightMaxX);
            NextPos = new Vector3(NextPosX, transform.position.y, transform.position.z);
        }
    }
}
