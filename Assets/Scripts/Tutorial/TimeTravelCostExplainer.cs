using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelCostExplainer : MonoBehaviour
{
    private bool _isExplained;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && GameManager.Instance.player.GetComponent<PlayerController>().tutorialTimeTravelEnabled)
        {
            Invoke(nameof(ExplainTimeTravelCost), 4.2f);
        }
    }

    private void ExplainTimeTravelCost()
    {
        if (!_isExplained)
        {
            GameManager.Instance.gameHUD.optionDescription.text = "Each time travel costs you 10 HP. If HP drops to 0, you die.";
            GameManager.Instance.gameHUD.PresentInteractionMessageOnly();
            _isExplained = true;
        }
    }
}
