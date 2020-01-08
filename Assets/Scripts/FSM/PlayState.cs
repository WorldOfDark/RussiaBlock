using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Play;
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        crtl.view.ShowGameUI(crtl.model.Score,crtl.model.HighScore);
        crtl.gameManager.StartGame();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        crtl.view.HideGameUI();
        crtl.view.ShowRestartButton();
        crtl.gameManager.PauseGame();
    }

    public void OnPauseButtonClick()
    {
        crtl.audioManager.PlayCursor();
        fsm.PerformTransition(Transition.PauseButtonClick);
    }

    public void OnRestartButtonClick()
    {
        crtl.view.HideGameOverUI();
        crtl.model.Restart();
        crtl.gameManager.StartGame();
        crtl.view.UpdateGameUI(0,crtl.model.HighScore);
    }
}
