using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartMenuControl : MonoBehaviour
{
    // Start is called before the first frame update

   public int loadInt = 0;
  


    public void ToggleStart()
    {
        loadInt = 0;
    }

    public void ToggleLoad()
    {
        loadInt = 1;
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("loadInt", loadInt);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        loadInt = PlayerPrefs.GetInt("loadInt");
    }

}
