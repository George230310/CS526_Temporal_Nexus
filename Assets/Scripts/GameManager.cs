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

    private List<EnemyMovement> _enemies;
    public GameObject player;

    public LevelOne levelOneSubmit;
    private float _elapsedTime;

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
        _enemies = FindObjectsOfType<EnemyMovement>(true).ToList();
        player = GameObject.FindGameObjectWithTag("Player");
        gameHUD = GameObject.FindGameObjectWithTag("GameHUD").GetComponent<GameHUD>();
    }

    public void RemoveEnemyFromList(EnemyMovement enemy)
    {
        _enemies.Remove(enemy);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        // find the closest enemy
        float minDist = float.MaxValue;
        EnemyMovement closestComp = null;
        foreach (var comp in _enemies)
        {
            float newDistance = Vector3.Distance(comp.gameObject.transform.position, player.transform.position);
            if (newDistance < minDist)
            {
                closestComp = comp;
                minDist = newDistance;
            }
        }
        
        if (minDist < 30.0f && closestComp)
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

        if (EndGamePanel)
        {
            EndGamePanel.SetActive(true);
        }
        
        levelOneSubmit.UpdateTime(_elapsedTime);
        levelOneSubmit.Send();
        Debug.Log(_elapsedTime);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;

        if (WinGamePanel)
        {
            WinGamePanel.SetActive(true);
        }
        
        levelOneSubmit.UpdateTime(_elapsedTime);
        levelOneSubmit.UpdateComplete();
        levelOneSubmit.Send();
        Debug.Log(_elapsedTime);
    }
    
    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
