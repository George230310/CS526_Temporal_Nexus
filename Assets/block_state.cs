using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_state : MultiStateEnemy
{
    // Start is called before the first frame update
    [SerializeField] Transform past;
    [SerializeField] Transform present;
    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                this.gameObject.transform.position=past.position;
                break;

            case TimeState.Present:
                this.gameObject.transform.position = present.position;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
