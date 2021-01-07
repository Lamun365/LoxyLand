using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fall : MonoBehaviour
{
    //private TimeManager timeSet;
    private void Start()
    {
        //timeSet = FindObjectOfType<TimeManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //timeSet.ResumeTime(); //falling Reset time
            PermanentUI.perm.Reset();
            SceneManager.LoadScene("Die");
        }
    }
}
