using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MultiStateObjectComponent
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided!");
            _gameManager.WinGame();
        }
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                // disable victory condition in the past
                gameObject.SetActive(false);
                break;
            
            case TimeState.Present:
                // enable victory condition in the present
                gameObject.SetActive(true);
                break;
        }
    }
}
