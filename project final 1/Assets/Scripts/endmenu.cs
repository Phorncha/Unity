using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endmenu : MonoBehaviour
{
 public void onbackclick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
