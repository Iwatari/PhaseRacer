using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
{
    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    [SerializeField] private GameObject playerResultPanel;
    [SerializeField] private Text playerRecordTime;
    [SerializeField] private Text playerCurrentTime;
    private void Start()
    {
        playerResultPanel.SetActive(false);

        raceResultTime.ResultUpdated += OnUpdateResults;
    }

    private void OnDestroy()
    {
        raceResultTime.ResultUpdated -= OnUpdateResults;
    }

    private void OnUpdateResults()
    {
        playerResultPanel.SetActive(true);

        playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsoluteRecord());
        playerCurrentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime); 
    }
}
