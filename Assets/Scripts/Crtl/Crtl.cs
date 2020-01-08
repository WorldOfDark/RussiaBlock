using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crtl : MonoBehaviour
{
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    [HideInInspector]
    public CameraManager cameraManager;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public AudioManager audioManager;
    public FSMSystem fsm;

    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("view").GetComponent<View>();
        cameraManager = GetComponent<CameraManager>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        MakeFSM();
    }

    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (var item in states)
        {
            fsm.AddState(item,this);
        }

        MenuState s = GetComponentInChildren<MenuState>();
        fsm.SetCurrentState(s);
    }
}
