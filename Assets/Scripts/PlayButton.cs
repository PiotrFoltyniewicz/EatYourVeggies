using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
