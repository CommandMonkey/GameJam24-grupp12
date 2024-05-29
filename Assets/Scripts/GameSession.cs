using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{
    public enum GameState
    {
        Cutscene,
        Running,
        Paused
    }


    public Player Player;
    public static GameSession Instance { get; private set; }
    public GameState State { get; private set; } 

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        InitializeReferences();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeReferences()
    {
        Player = FindObjectOfType<Player>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}
