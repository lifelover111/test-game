using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public Player player;
    [SerializeField] Transform retryButton;
    [SerializeField] Transform winMessage;

    private void Awake()
    {
        instance = this;
    }

    public void Win()
    {
        winMessage.gameObject.SetActive(true);
    }

    public void Defeat()
    {
        retryButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
