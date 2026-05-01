using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    [SerializeField] private GameObject infoPanel;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) == true)
        {
            raceStateTracker.LaunchPreparationStarted();
            infoPanel.SetActive(false);
        }
    }
}
