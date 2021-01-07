using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private TimeManager Time;
    [SerializeField] private GameObject plr;
    [SerializeField] private string SceneName;
    private void Start()
    {
        Time = FindObjectOfType<TimeManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PermanentUI.perm.completeLevel.gameObject.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            plr.gameObject.SetActive(false);
            Time.ResumeTime();
            //PermanentUI.perm.ResumePanel.gameObject.SetActive(true); //resume

            if(SceneManager.GetActiveScene().buildIndex == 2) //last scene will delay 3seconds
            {
                Invoke("Restart", 3f);
            }
            else
            {
                Invoke("Restart", 1f);
            }
            
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneName);
    }
}
