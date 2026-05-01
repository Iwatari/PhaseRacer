using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCompletion : MonoBehaviour
{
    [SerializeField] private string[] raceNames;

    [SerializeField] private GameObject[] objects;

    private void Start()
    {
        RaceResultTime.UnlockedRaceIndex = PlayerPrefs.GetInt("UnlockedRaceIndex", 0);

        for (int i = 0; i < raceNames.Length; i++)
        {
            if (i <= RaceResultTime.UnlockedRaceIndex)
            {
                objects[i].SetActive(true);
            }
            else
            {
                objects[i].SetActive(false);
            }
        }
    }
}
