using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public Text timeCount;
    public static float time;
    [SerializeField] private GameObject TimeTitle, TimeCountTitle;
    [SerializeField] private Text lastTime; //finished time
    [SerializeField] private Text hTime; //high score time text
    [SerializeField] private GameObject finished;
    public bool resumeBool = false;
    public void Start()
    {
        timeCount.text = time.ToString("F2");
        DieTime();
        if(SceneManager.GetActiveScene().name != "Die")
        {
            hTime.text = PlayerPrefs.GetFloat("highTm").ToString("F2"); //highscore
        }

    }
    public void Update()
    {
        if(resumeBool)
        {
            time += Time.deltaTime;
            timeCount.text = time.ToString("F2");
            
        }
    }
    public void ResumeTime() //resume in house
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            resumeBool = false;
            Debug.Log("resume");

        }
        
        else if(SceneManager.GetActiveScene().name == "Die")
        {
            resumeBool = false;
            Debug.Log("resume");
        } 
        else if(SceneManager.GetActiveScene().buildIndex == 2) //imporatant deacrive time and time counter
        {
            TimeTitle.gameObject.SetActive(false);
            TimeCountTitle.gameObject.SetActive(false);
            //add a finished time
            finished.gameObject.SetActive(true);
            lastTime.text = time.ToString("F2");
            HighScore();
        }
        else
        {
            resumeBool = true;
        }
    }
    public void ResetTime() //falling reset time
    {
        time = 0f;
        timeCount.text = time.ToString();
    }
    private void DieTime()
    {
        if(SceneManager.GetActiveScene().name == "Die")
        {
            resumeBool = false;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            resumeBool = true;
        }
        
    }
    public void HighScore()
    {
        if(PlayerPrefs.GetFloat("highTm") == 0f)
        {
            PlayerPrefs.SetFloat("highTm", time);
            hTime.text = time.ToString("F2");
        }
        if(PlayerPrefs.GetFloat("highTm") > time)
        {
            PlayerPrefs.SetFloat("highTm", time);
            hTime.text = time.ToString("F2");
        }
    }

}
