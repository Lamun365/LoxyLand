using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;
//using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    
    
    //public TextMeshProUGUI CherryCount;

    private void Start()
    {
        //PermanentUI.perm.cherryText.text = PermanentUI.perm.cherries.ToString("0");
        

        
    }
    private void Update()
    {
        //Debug.Log(PermanentUI.perm.cherryText.text);
    } 

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SceneChanger( int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }
}
