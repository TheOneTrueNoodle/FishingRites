using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FishingRodScript : MonoBehaviour
{
    [SerializeField] private float BoundingDistance = 7.5f;

    [SerializeField] private GameObject BeatMarker;
    [SerializeField] private R_NoteDetector BeatMarkerPressed;

    public KeyCode BeatPress;
    public KeyCode BeatPress2;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(BeatPress) || Input.GetKeyDown(BeatPress2))
        {
            BeatPressed();
        }
        else if(Input.GetKeyUp(BeatPress) || Input.GetKeyUp(BeatPress2))
        {
            if(BeatMarkerPressed.Active == true)
            {
                BeatMarkerPressed.Release();
            }
            BeatReleased();
        }

        float mouseX = cam.ScreenToWorldPoint(Input.mousePosition).x;
        if(mouseX > -BoundingDistance && mouseX < BoundingDistance)
        {
            transform.position = new Vector3(mouseX, transform.position.y, transform.position.z);
        }
        else if(mouseX < -BoundingDistance)
        {
            transform.position = new Vector3(-BoundingDistance, transform.position.y, transform.position.z);
        }
        else if(mouseX > BoundingDistance)
        {
            transform.position = new Vector3(BoundingDistance, transform.position.y, transform.position.z);
        }
    }

    private void BeatPressed()
    {
        BeatMarker.gameObject.SetActive(false);
        BeatMarkerPressed.Active = true;
    }

    private void BeatReleased()
    {
        BeatMarker.gameObject.SetActive(true);
        BeatMarkerPressed.Active = false;
    }
}
