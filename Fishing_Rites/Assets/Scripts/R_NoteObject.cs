using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_NoteObject : MonoBehaviour
{
    public float length;
    public float speed;
    public Vector3 StartPos;
    [SerializeField] private GameObject TopNote;
    private Vector3 NextPos;

    public LineRenderer LineRenderer;

    private void Start()
    {
        gameObject.transform.position = StartPos;
        NextPos = new Vector3(StartPos.x, -6f, StartPos.z);
        TopNote.transform.position = new Vector3(StartPos.x, StartPos.y + length, StartPos.z);

        Vector3[] LinePositions = new Vector3[2] { new Vector3(0,0,0), TopNote.transform.position - transform.position};

        LineRenderer.SetPositions(LinePositions);
    }

    private void Update()
    {
        if (transform.position != NextPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, NextPos, speed * Time.deltaTime);
        }
    }
}
