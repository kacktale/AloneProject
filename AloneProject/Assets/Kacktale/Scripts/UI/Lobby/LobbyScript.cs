using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviour
{
    public Button startButton;
    public TextMeshProUGUI subtitleText;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(675, 508, FullScreenMode.FullScreenWindow);
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        subtitleText.transform.Rotate(0,0,0.3f);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
