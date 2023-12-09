using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            { Debug.LogError("Game Manager is null!!"); }
            return _instance;
        }
    }

    public Player player { get; private set; }

    public bool HasKeyToCastle { get; set; }

    private void Awake()
    {
        _instance = this;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void PauseMenu()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("PauseMenu");
    }

    public void MainMenu()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player._isPlayerDead == true)
        { SceneManager.LoadScene("MainMenu"); }
    }
}
