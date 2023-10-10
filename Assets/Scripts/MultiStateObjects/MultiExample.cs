using UnityEngine;

public class MultiExample : MultiStateObjectComponent
{
    public GameObject smallCircle; // For the past
    public GameObject bigCircle;   // For the present

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                smallCircle.SetActive(true);
                bigCircle.SetActive(false);
                break;

            case TimeState.Present:
                smallCircle.SetActive(false);
                bigCircle.SetActive(true);
                break;
        }
    }
}