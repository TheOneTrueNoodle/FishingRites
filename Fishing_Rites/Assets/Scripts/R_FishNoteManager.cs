using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FishNoteManager : MonoBehaviour
{
    public float NoteSpeed;
    [SerializeField] private float BoundingDistance = 7.5f;

    public GameObject NotePrefab;
    [SerializeField] public List<R_Note> Notes;
}
