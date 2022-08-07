using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        // Debug.Log("height----" + Screen.currentResolution.height + "     " + "width----" + Screen.currentResolution.width);
        string systemStr = SystemInfo.deviceModel;
        //if(Screen.currentResolution.height == 1792 && Screen.currentResolution.width == 828)
        //{
        //    SceneManager.LoadSceneAsync("tmp_lm_X");
        //}
        //else
            //SceneManager.LoadSceneAsync("tmp_lm");
        if(systemStr.Equals("iPhone10,3") || systemStr.Equals("iPhone10,6") || systemStr.Equals("iPhone11,8") || systemStr.Equals("iPhone11,2") || systemStr.Equals("iPhone11,6"))
        {
            SceneManager.LoadSceneAsync("tmp_lm_X");
        }
        else
            SceneManager.LoadSceneAsync("tmp_lm");
           
	}
	
}
