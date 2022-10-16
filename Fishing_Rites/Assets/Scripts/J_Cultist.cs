using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Cultist : MonoBehaviour
{
    public GameObject textbox;

    void OnTriggerEnter2D(Collider2D col)
    {
        textbox.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        textbox.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        textbox.SetActive(false);
    }
}
