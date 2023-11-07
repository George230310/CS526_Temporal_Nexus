using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public GameObject EndGamePanel;
    public GameObject WinGamePanel;
    
    public static GameManager Instance { get; private set; }
    public GameObject closestEnemyInPetRange;
    public GameHUD gameHUD;

    private List<MultiStateEnemy> _enemies;
    public GameObject player;

    public LevelAnalyticsCollect levelAnalyticsSubmit;
    private float _elapsedTime;

    [SerializeField] private float petRange;

    public bool isPetPickedUp;

    public bool isMiddleDoorOpened;

    private void Awake()
    {
        // singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _enemies = FindObjectsOfType<MultiStateEnemy>(true).ToList();
        player = GameObject.FindGameObjectWithTag("Player");
        gameHUD = GameObject.FindGameObjectWithTag("GameHUD").GetComponent<GameHUD>();
    }

    public void RemoveEnemyFromList(MultiStateEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        // find the closest enemy
        float minDist = float.MaxValue;
        MultiStateEnemy closestComp = null;
        foreach (var comp in _enemies)
        {
            float newDistance = Vector3.Distance(comp.gameObject.transform.position, player.transform.position);
            if (newDistance < minDist)
            {
                closestComp = comp;
                minDist = newDistance;
            }
        }
        
        if (minDist < petRange && closestComp)
        {
            closestEnemyInPetRange = closestComp.gameObject;
        }
        else
        {
            closestEnemyInPetRange = null;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        
        AnalyticsEventSystem.TriggerOnPetCollect(isPetPickedUp);
        AnalyticsEventSystem.TriggerOnOpenDoor(isMiddleDoorOpened);

        if (EndGamePanel)
        {
            EndGamePanel.SetActive(true);
        }

        if (levelAnalyticsSubmit)
        {
            levelAnalyticsSubmit.UpdateTime(_elapsedTime);
            levelAnalyticsSubmit.Send();
        }
        
        Debug.Log(_elapsedTime);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        
        AnalyticsEventSystem.TriggerOnPetCollect(isPetPickedUp);
        AnalyticsEventSystem.TriggerOnOpenDoor(isMiddleDoorOpened);

        if (WinGamePanel)
        {
            WinGamePanel.SetActive(true);
        }

        if (levelAnalyticsSubmit)
        {
            levelAnalyticsSubmit.UpdateTime(_elapsedTime);
            levelAnalyticsSubmit.UpdateComplete();
            levelAnalyticsSubmit.Send();
        }

        Debug.Log(_elapsedTime);
    }
    
    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
