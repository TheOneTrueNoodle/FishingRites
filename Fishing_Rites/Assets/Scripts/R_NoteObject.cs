using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_NoteObject : MonoBehaviour
{
    public float length;
    public float WaitTime;
    public Vector3 StartPos;
    [SerializeField] private GameObject TopNote;

    private void Start()
    {
        gameObject.transform.position = StartPos;
        TopNote.transform.position = new Vector3(StartPos.x, StartPos.y + length, StartPos.z);  
    }
}
