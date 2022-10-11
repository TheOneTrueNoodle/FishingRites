using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_NoteObject : MonoBehaviour
{
    private R_FishNoteManager NoteManager;

    [Header("Spawn Values")]
    public float length;
    public float speed;

    [Header("Position Values")]
    public Vector3 StartPos;
    public GameObject BottomNote;
    public GameObject TopNote;
    private Vector3 NextPos;
    public LineRenderer LineRenderer;

    public bool NoteHeld;

    private void Start()
    {
        NoteManager = FindObjectOfType<R_FishNoteManager>();

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

        if(BottomNote.transform.position.y < -4 && NoteHeld == false)
        {
            NoteFailed();
        }
    }

    private void FixedUpdate()
    {
        if (NoteHeld == true)
        {
            NoteManager.IncreaseScore(1f);
        }
    }

    public void NoteCompleted()
    {
        Destroy(gameObject);
    }

    public void NoteFailed()
    {
        NoteManager.ResetMultiplier();
        Destroy(gameObject);
    }
}
