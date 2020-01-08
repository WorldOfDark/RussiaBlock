using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPause = true;
    private Shape currentShape = null;
    public Shape[] shapes;
    public Color[] colors;
    private Transform blockHolder;
    private Crtl crtl;

    void Start()
    {
        crtl = GetComponent<Crtl>();

        blockHolder = transform.Find("blockHolder").transform;
    }

    void Update()
    {
        if (isPause) { return; }
        if (currentShape == null)
        {
            SpawnShape();
        }
    }

    public void StartGame()
    {
        isPause = false;
        if (currentShape != null)
        {
            currentShape.Resume();
        }
    }

    public void PauseGame()
    {
        isPause = true;
        if (currentShape != null)
        {
            currentShape.Pause();
        }
    }

    public void SpawnShape()
    {
        int index = Random.Range(0,shapes.Length);
        int colorIndex = Random.Range(0,colors.Length);
        currentShape = Instantiate(shapes[index]);
        currentShape.transform.parent = blockHolder;
        currentShape.Init(colors[colorIndex],crtl,this);
    }

    public void ClearShape()
    {
        if (currentShape != null)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }

    public void SetCurrentShape()
    {
        currentShape = null;
        if (crtl.model.isUpdateScore)
        {
            crtl.view.UpdateGameUI(crtl.model.Score, crtl.model.HighScore);
            crtl.model.isUpdateScore = false;
        }
        foreach (Transform t in blockHolder)
        {
            if (t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
        if (crtl.model.IsGameOver())
        {
            PauseGame();
            crtl.view.ShowGameOverUI(crtl.model.Score);
        }
    }
}
