using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    public static string SaveMark = "_player_best_time";

    public event UnityAction ResultUpdated;

    public static int UnlockedRaceIndex;


    [SerializeField] private float goldTime;

    private float playerRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayerRecordTime => playerRecordTime;
    public float CurrentTime => currentTime;
    public bool RecordWasSet => playerRecordTime != 0;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Awake()
    {
        Load();
    }
    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
        if (UnlockedRaceIndex == 0) goldTime = 120;
        if (UnlockedRaceIndex == 1) goldTime = 150;
        if (UnlockedRaceIndex == 2) goldTime = 150;
        if (UnlockedRaceIndex == 3) goldTime = 100;
    }
    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }


    private void OnRaceCompleted()
    {
        float absoluteRecord = GetAbsoluteRecord();

        if(raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
        {
            playerRecordTime = raceTimeTracker.CurrentTime;

            if (raceTimeTracker.CurrentTime < goldTime)
            {
                UnlockedRaceIndex++;
            }

            Save();
        }

        currentTime = raceTimeTracker.CurrentTime;

        ResultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if(playerRecordTime < goldTime && playerRecordTime != 0)
            return playerRecordTime;
        else
            return goldTime;
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);

        if (PlayerPrefs.HasKey("UnlockedRaceIndex"))
        {
            UnlockedRaceIndex = PlayerPrefs.GetInt("UnlockedRaceIndex", 0);
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, PlayerRecordTime);

        PlayerPrefs.SetInt("UnlockedRaceIndex", UnlockedRaceIndex);

    }
}
