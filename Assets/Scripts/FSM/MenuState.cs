using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Menu;
        AddTransition(Transition.StartButtonClick,StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        crtl.view.ShowMenu();
        crtl.cameraManager.ZoomIn();
    }

    public override void DoBeforeLeaving()
    {
        crtl.view.HideMenu();
        crtl.cameraManager.ZoomOut();
    }

    public void OnStartButtonClick()
    {
        crtl.audioManager.PlayCursor();
        fsm.PerformTransition(Transition.StartButtonClick);
    }

    public void OnSettingButtonClick()
    {
        crtl.audioManager.PlayCursor();
        crtl.view.SetSettingUIActive(true);
    }

    public void OnRankButtonClick()
    {
        crtl.audioManager.PlayCursor();
        crtl.view.ShowRankUI(crtl.model.Score,crtl.model.HighScore,crtl.model.NumberGames);
    }

    public void OnDestoryButtonClick()
    {
        crtl.model.ClearScore();
        OnRankButtonClick();
    }

    public void OnRestartButtonClick()
    {
        crtl.audioManager.PlayCursor();
        crtl.model.Restart();
        crtl.gameManager.ClearShape();
        fsm.PerformTransition(Transition.StartButtonClick);
    }

    public void OnOtherButtonClick()
    {
        crtl.view.HideRankUI();
        crtl.view.SetSettingUIActive(false);
    }
}
