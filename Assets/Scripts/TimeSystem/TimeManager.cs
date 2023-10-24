using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TimeState
{
    Present,
    Past
}

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private List<MultiStateObjectComponent> _multiStateObjectComponents;
    public TimeState CurrentGlobalTimeState { get; private set; }
    
    // target that can be interacted by the player
    public MultiStateObjectComponent interactionTarget = null;

    private GameObject _player;
    private PlayerController _playerController;
    private GameHUD _gameHUD;
    private bool _isTimeTransitionExecuting;

    [SerializeField] private float timeTransitionDuration;
    [SerializeField] private float timeTravelCost;

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


    // Start is called before the first frame update
    private void Start()
    {
        // initialize current global time state to be present
        CurrentGlobalTimeState = TimeState.Present;
        
        // get all multi-state object components
        _multiStateObjectComponents = FindObjectsOfType<MultiStateObjectComponent>(true).ToList();
        
        // call state change to present on all multi-state objects
        foreach (var multiState in _multiStateObjectComponents)
        {
            multiState.OnTimeStateChange(CurrentGlobalTimeState);
        }
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _gameHUD = GameObject.FindGameObjectWithTag("GameHUD").GetComponent<GameHUD>();
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        // find the closest interactable multi-state object
        float minDist = float.MaxValue;
        MultiStateObjectComponent closestComp = null;
        foreach (var comp in _multiStateObjectComponents)
        {
            float newDistance = Vector3.Distance(comp.gameObject.transform.position, _player.transform.position);
            if (newDistance < minDist)
            {
                closestComp = comp;
                minDist = newDistance;
            }
        }
        
        // if the closest multi-state object is close enough, set it to be the player's interaction target
        if (minDist < 7.0f)
        {
            interactionTarget = closestComp;
            if (interactionTarget && interactionTarget.isInteractable)
            {
               _playerController.ToggleInteractionPrompt(true);
            }
        }
        else
        {
            if (interactionTarget)
            {
                _playerController.ToggleInteractionPrompt(false);
            }
            
            interactionTarget = null;
        }
    }

    public void TimeTransition(TimeState newTimeState)
    {
        if (_isTimeTransitionExecuting)
        {
            return;
        }
        
        _isTimeTransitionExecuting = true;
        StartCoroutine(ExecuteTimeTransition(newTimeState));
    }

    private IEnumerator ExecuteTimeTransition(TimeState newTimeState)
    {
        yield return new WaitForSeconds(0.2f);
        
        if (newTimeState == TimeState.Past)
        {
            _gameHUD.SetTextPrompt("50 years ago, same location...");
        }
        else if (newTimeState == TimeState.Present)
        {
            _gameHUD.SetTextPrompt("Back to the present...");
        }

        _gameHUD.ToggleBlackOverlay(true);
        float half = timeTransitionDuration / 2.0f;
        
        _gameHUD.FadeBlackOverlay(timeTransitionDuration);
        yield return new WaitForSeconds(half);
        
        ChangeCurrentGlobalTimeState(newTimeState);
        
        yield return new WaitForSeconds(half);
        
        _gameHUD.ToggleBlackOverlay(false);
        
        yield return new WaitForSeconds(0.2f);
        _isTimeTransitionExecuting = false;
    }

    private void ChangeCurrentGlobalTimeState(TimeState newTimeState)
    {
        // if the new time state is different, change global time state and call on time state change on each multi-state objects
        if (newTimeState != CurrentGlobalTimeState)
        {
            CurrentGlobalTimeState = newTimeState;
            foreach (var multiState in _multiStateObjectComponents)
            {
                multiState.OnTimeStateChange(CurrentGlobalTimeState);
            }
            
            // deduct player health
            _player.GetComponent<HealthComponent>().TakeDamage(timeTravelCost);
        }
    }

    public void RemoveMultiStateObject(MultiStateObjectComponent comp)
    {
        _multiStateObjectComponents.Remove(comp);
    }

    public void AddMultiStateObject(MultiStateObjectComponent comp)
    {
        _multiStateObjectComponents.Add(comp);
    }
}
