using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Tree : MultiStateObjectComponent
{
    [SerializeField] private float plantTreeCost;

    private enum TreeState
    {
        UNPLANTED,
        PLANTED,
        GROWN,
        CUT,
        DEAD
    }

    private TreeState pastState;

    private TreeState presentState;

    public GameObject UnplantedTree;

    public GameObject PlantedTree;

    public GameObject GrownTree;

    public GameObject CutTree;

    public GameObject DeadTree;


    private void Start()
    {
        pastState = TreeState.UNPLANTED;
        presentState = TreeState.DEAD;

        SetCorrectTree();
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                if (pastState == TreeState.PLANTED)
                {
                    isInteractable = true;
                }
                else
                {
                    isInteractable = true;
                }
                break;

            case TimeState.Present:
                if (presentState != TreeState.CUT && pastState == TreeState.PLANTED)
                {
                    presentState = TreeState.GROWN;
                }

                if (presentState == TreeState.CUT)
                {
                    isInteractable = false;
                }
                else
                {
                    isInteractable = true;
                }

                break;
        }

        SetCorrectTree();
    }

    private void SetCorrectTree()
    {
        UnplantedTree.SetActive(false);
        PlantedTree.SetActive(false);
        GrownTree.SetActive(false);
        CutTree.SetActive(false);
        DeadTree.SetActive(false);

        if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Past)
        {
            if (pastState == TreeState.UNPLANTED)
            {
                UnplantedTree.SetActive(true);
            }
            else if (pastState == TreeState.PLANTED)
            {
                PlantedTree.SetActive(true);
            }
        }
        else if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Present)
        {
            if (presentState == TreeState.UNPLANTED)
            {
                UnplantedTree.SetActive(true);
            }
            if (presentState == TreeState.GROWN)
            {
                GrownTree.SetActive(true);
            }
            else if (presentState == TreeState.CUT)
            {
                CutTree.SetActive(true);
            }
            else if (presentState == TreeState.DEAD)
            {
                DeadTree.SetActive(true);
            }
        }
    }

    public override void OnInteract()
    {
        if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Past)
        {
            if (pastState == TreeState.UNPLANTED)
            {
                GameManager.Instance.gameHUD.optionDescription.text = "Wanna plant a tree here?";
                GameManager.Instance.gameHUD.yesButton.onClick.AddListener(PlantTree);
                GameManager.Instance.gameHUD.PresentInteractionMessageAndOptions(plantTreeCost);
            }
        }

        if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Present)
        {
            if (presentState == TreeState.GROWN)
            {
                GameManager.Instance.gameHUD.optionDescription.text = "You chopped down this tree...";
                GameManager.Instance.gameHUD.PresentInteractionMessageOnly();
                CutDownTree();
            }
            else
            {
                if (GameManager.Instance)
                {
                    GameManager.Instance.gameHUD.optionDescription.text = "The land looks very fertile here. What if I plant something here in the past?";
                    GameManager.Instance.gameHUD.PresentInteractionMessageOnly();
                }
            }
        }
    }

    private void PlantTree()
    {
        pastState = TreeState.PLANTED;
        isInteractable = false;
        SetCorrectTree();
    }

    private void CutDownTree()
    {
        presentState = TreeState.CUT;
        isInteractable = false;
        SetCorrectTree();
    }
}

