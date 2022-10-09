using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FishNoteManager : MonoBehaviour
{
    [SerializeField] private float BoundingDistance = 7.5f;

    public GameObject NotePrefab;
    [SerializeField] public List<R_Note> Notes;

    public Queue<IEnumerator> NoteQueue = new Queue<IEnumerator>();

    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());

        foreach(R_Note Note in Notes)
        {
            NoteQueue.Enqueue(SpawnNote(Note));
        }
    }

    private IEnumerator SpawnNote(R_Note Note)
    {
        yield return new WaitForSeconds(Note.WaitTime);
        GameObject NewNote = Instantiate(NotePrefab);
        NewNote.GetComponent<R_NoteObject>().length = Note.Length;
        NewNote.GetComponent<R_NoteObject>().speed = Note.NoteSpeed;
        NewNote.GetComponent<R_NoteObject>().StartPos = transform.position;
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (NoteQueue.Count > 0)
                yield return StartCoroutine(NoteQueue.Dequeue());
            yield return null;
        }
    }
}
