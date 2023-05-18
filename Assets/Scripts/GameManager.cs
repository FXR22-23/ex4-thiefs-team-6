using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject alarm;
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject gameplayMusic;
    [SerializeField] private GameObject chaseMusic;
    [SerializeField] private GameObject winMusic;
    [SerializeField] private GameObject loseMusic;
    
    public void TriggerAlarm()
    {
        alarm.SetActive(true);
        guard.GetComponent<NPCMove>().Chase();
    }

    public void TurnOnChaseMusic()
    {
        gameplayMusic.SetActive(false);
        chaseMusic.SetActive(true);
    }
    public void TurnOnWinMusic()
    {
        chaseMusic.SetActive(false);
        winMusic.SetActive(true);
    }
    public void TurnOnLoseMusic()
    {
        chaseMusic.SetActive(false);
        loseMusic.SetActive(true);
    }
    
}