using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
   public void onStartClick()
    {
        SceneManager.LoadScene("Scene1");
    }

}
