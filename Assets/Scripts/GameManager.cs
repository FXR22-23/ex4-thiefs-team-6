using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject alarm;
    [SerializeField] private GameObject guard;
    public void TriggerAlarm()
    {
        alarm.SetActive(true);
        guard.GetComponent<NPCMove>().Chase();
    }
}