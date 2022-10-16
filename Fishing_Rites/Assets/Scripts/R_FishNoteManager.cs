using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_FishNoteManager : MonoBehaviour
{
    public GameObject NotePrefab;
    [SerializeField] public List<R_Note> Notes;

    public Queue<IEnumerator> NoteQueue = new Queue<IEnumerator>();

    [Header("Score Variables")]
    public int Score;
    public float Multiplier;
    [Header("Score Displays")]
    public TMP_Text ScoreDisplay;
    public TMP_Text MultiplierDisplay;

    public GameObject FishSpriteParent;
    public string FishMusic;

    private void Start()
    {
        StartCoroutine(StartCoordinator());

      /*  foreach (R_Note Note in Notes)
        {
            NoteQueue.Enqueue(SpawnNote(Note));
        }*/

        ScoreDisplay.text = "Score: " + Score.ToString();
        MultiplierDisplay.text = "x" + Multiplier.ToString();
    }

    public void IncreaseScore(float Num)
    {
        Score += (int)(Num * Multiplier);
        ScoreDisplay.text = "Score: " + Score.ToString();
    }

    public void IncreaseMultiplier(float Num)
    {
        Multiplier += Num;
        MultiplierDisplay.text = "x" + Multiplier.ToString();
    }

    public void ResetMultiplier()
    {
        Multiplier = 1f;
        MultiplierDisplay.text = "x" + Multiplier.ToString();
    }

    public void LoadFish(R_Fish Fish)
    {
        FishMusic = Fish.FishMusic;
        GameObject FishCharacter = Instantiate(Fish.FishToFightPrefab);
        FishCharacter.transform.parent = FishSpriteParent.transform;
        FishCharacter.transform.localPosition = Vector3.zero;


        Notes.Clear();
        for(int i = 0; i < Fish.Notes.Count; i++)
        {
            Notes.Add(Fish.Notes[i]);
        }

        foreach (R_Note Note in Notes)
        {
            NoteQueue.Enqueue(SpawnNote(Note));
        }
    }

    private IEnumerator SpawnNote(R_Note Note)
    {
        GameObject NewNote = Instantiate(NotePrefab);
        NewNote.GetComponent<R_NoteObject>().length = Note.Length;
        NewNote.GetComponent<R_NoteObject>().speed = Note.NoteSpeed;
        NewNote.GetComponent<R_NoteObject>().StartPos = transform.position;
        yield return new WaitForSeconds(Note.WaitTime);
    }

    IEnumerator StartCoordinator()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(CoroutineCoordinator());
        FindObjectOfType<AudioManager>().Play(FishMusic);
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
