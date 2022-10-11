using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_NoteDetector : MonoBehaviour
{
    private R_FishNoteManager NoteManager;

    public bool Active;
    [SerializeField] private R_NoteObject CurrentNote;

    public float CloseRange = 1f;
    private bool NoteHeld;

    [SerializeField] private float NoteValue;

    [SerializeField] private GameObject BeatMarker;

    public KeyCode BeatPress;
    public KeyCode BeatPress2;

    private void Start()
    {
        NoteManager = FindObjectOfType<R_FishNoteManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Active == true)
        {
            if (collision.tag == "Note")
            {
                CurrentNote = collision.transform.parent.GetComponent<R_NoteObject>();

                if (Vector3.Distance(transform.position, CurrentNote.BottomNote.transform.position) <= CloseRange)
                {
                    NoteHeld = true;
                    NoteManager.IncreaseScore(NoteValue);
                }
            }
        }
    }

    public void Release()
    {
        if (CurrentNote != null)
        {
            NoteHeld = false;
            if (Vector3.Distance(transform.position, CurrentNote.TopNote.transform.position) <= CloseRange)
            {
                NoteManager.IncreaseScore(NoteValue);
                NoteManager.IncreaseMultiplier(0.5f);
                CurrentNote.NoteCompleted();
            }
            else
            {
                CurrentNote.NoteFailed();
            }

            CurrentNote = null;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(BeatPress) || Input.GetKeyDown(BeatPress2))
        {
            BeatMarker.gameObject.SetActive(false);
            Active = true;
        }
        else if (Input.GetKeyUp(BeatPress) || Input.GetKeyUp(BeatPress2))
        {
            if (Active == true)
            {
                Release();
            }
            BeatMarker.gameObject.SetActive(true);
            Active = false;
        }
    }
}
