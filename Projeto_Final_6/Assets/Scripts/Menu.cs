using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject playBtn;

    public void StartGame()
    {
        menuPanel.SetActive(false);
        Time.timeScale= 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
       // SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        Time.timeScale = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
