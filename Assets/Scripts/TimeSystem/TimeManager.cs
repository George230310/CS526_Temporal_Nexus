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
            else if (interactionTarget && !interactionTarget.isInteractable)
            {
                // The object was previously interactable but anymore.
                _playerController.ToggleInteractionPrompt(false);
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
        yield return new WaitForSeconds(0.1f);

        
        ChangeCurrentGlobalTimeState(newTimeState);
        
        
        yield return new WaitForSeconds(0.1f);
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
