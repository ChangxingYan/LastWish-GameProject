using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{


    public GameObject loadButton;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/StreamingAssets/save/player.cxy";
        if (File.Exists(path))
        {
            loadButton.SetActive(true);
        }
        else
        {
            loadButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PLayTheGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitTheGame()
    {

        Debug.Log("Quit");
        Application.Quit();
    }


}
