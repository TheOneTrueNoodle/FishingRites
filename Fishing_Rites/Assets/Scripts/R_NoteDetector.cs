using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_NoteDetector : MonoBehaviour
{
    public bool Active;
    private R_NoteObject CurrentNote;

    public float CloseRange = 1f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Active)
        {
            if (collision.tag == "Note")
            {
                CurrentNote = collision.transform.parent.GetComponent<R_NoteObject>();

                if (Vector3.Distance(transform.position, CurrentNote.BottomNote.transform.position) <= CloseRange)
                {
                    
                }
            }
        }
    }

    public void Release()
    {
        if(CurrentNote != null)
        {
            if (Vector3.Distance(transform.position, CurrentNote.TopNote.transform.position) <= CloseRange)
            {

            }

            CurrentNote = null;
        }
    }
}
