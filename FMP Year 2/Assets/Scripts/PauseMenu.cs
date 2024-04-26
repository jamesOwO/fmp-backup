using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public PlayerController playerController;
    public Button playButton;

    void Start()
    {
        Button playbtn = playButton.GetComponent<Button>();
        playbtn.onClick.AddListener(TaskOnClick);

    }

    void Update()
    {
        
    }
    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        playerController.menuActive = false;
        playerController.pauseMenu.SetActive(false);
    }
}
