using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Transform pivot;

    private bool isPause = false;

    private bool isSpeedUp = false;

    private float timer = 0.0f;

    private float stepTime = 0.8f;

    private float multiple = 15;

    private float normalTime = 0.8f;

    private Crtl crtl;

    private GameManager gameManager;

    private void Awake()
    {
        pivot = transform.Find("pivot");
    }

    private void Update()
    {
        if (isPause)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer > stepTime)
        {
            timer = 0.0f;
            Fall();
        }

        InputControl();
    }

    public void Init(Color color,Crtl crtl,GameManager gameManager)
    {
        foreach (Transform t in transform)
        {
            if (t.CompareTag("block"))
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        this.crtl = crtl;
        this.gameManager = gameManager;
    }

    private void Fall()
    {
        Vector3 pos = transform.position;
        pos -= new Vector3(0,1,0);
        transform.position = pos;
        if (crtl.model.IsValidMapPostion(transform) == false)
        {
            pos += new Vector3(0, 1, 0);
            transform.position = pos;
            isPause = true;
            bool isClear = crtl.model.PlaceShpae(transform);
            if (isClear)
            {
                crtl.audioManager.PlayLineClear();
            }
            gameManager.SetCurrentShape();
            return;
        }
        crtl.audioManager.PlayDrop();
    }

    private void InputControl()
    {
        MoveLeftAndRight();
        RotateForward();
        SpeedUp();
    }

    void MoveLeftAndRight()
    {
        float h = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            h = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = 1;
        }
        if (h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (crtl.model.IsValidMapPostion(transform) == false)
            {
                pos.x -= h;
                transform.position = pos;
            }
            else
            { crtl.audioManager.PlayControl(); }
        }
    }

    void RotateForward()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (crtl.model.IsValidMapPostion(transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            { crtl.audioManager.PlayControl(); }
        }
    }

    void SpeedUp()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isSpeedUp = true;
            stepTime = normalTime / multiple;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isSpeedUp = false;
            stepTime = normalTime;
        }
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }
}
