using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    public int cherries = 0;
    public int health = 5;
    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI highScore;
    public GameObject[] live;
    public GameObject levelTitle, levelText;
    public TextMeshProUGUI levelInput;
    public GameObject completeLevel;
    //public GameObject ResumePanel; //resume

    public static PermanentUI perm;
    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Reset()
    {
        health = 5;
        cherries = 0;
        Invoke("healthReSet", 1f);
        cherryText.text = cherries.ToString();
    }
    private void healthReSet()
    {
        live[0].gameObject.SetActive(true);
        live[1].gameObject.SetActive(true);
        live[2].gameObject.SetActive(true);
        live[3].gameObject.SetActive(true);
        live[4].gameObject.SetActive(true);
    }
}