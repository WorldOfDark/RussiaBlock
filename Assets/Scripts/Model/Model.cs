using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public const int NORMAL_ROWS = 20;
    public const int MAX_ROWS = 23;
    public const int MAX_COLUMNS = 10;

    [SerializeField]
    public Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];

    private int score;
    public int Score { get { return score; } }
    [SerializeField]
    private int highScore;
    public int HighScore { get { return highScore; } }
    [SerializeField]
    private int numberGames;
    public int NumberGames { get { return numberGames; } }

    public bool isUpdateScore = false;

    private void Awake()
    {
        LoadScore();
    }

    public bool IsValidMapPostion(Transform t)
    {
        foreach (Transform child in t)
        {
            if (!child.CompareTag("block")) continue;
            Vector2 pos = Vector3Unitl.GetIntPos(child.position);
            if (!IsInMapPostion(pos)) return false;
            if (map[(int)pos.x, (int)pos.y] != null) return false;
        }

        return true;
    }

    public bool IsGameOver()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j,i] != null)
                {
                    numberGames++;
                    SaveScore();
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsInMapPostion(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }

    public bool PlaceShpae(Transform t)
    {
        foreach (Transform child in t)
        {
            if (!child.CompareTag("block")) continue;
            Vector2 pos = Vector3Unitl.GetIntPos(child.position);
            map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }

    private bool CheckMap()
    {
        int count = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = isRowFull(i);
            if (isFull)
            {
                count++;
                DelteRow(i);
                MoveDownRowsAbove(i + 1);
                i--;
            }
        }
        if (count > 0)
        {
            score += (count * 100);
            if (score > highScore)
            {
                highScore = score;
            }
            isUpdateScore = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i,row] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void DelteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    private void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }

    private void MoveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i,row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void LoadScore()
    {
        numberGames = PlayerPrefs.GetInt("numberGames", 0);
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("numberGames",numberGames);
        PlayerPrefs.SetInt("highScore",highScore);
    }

    public void Restart()
    {
        for (int i = 0; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j,i] != null)
                {
                    Destroy(map[j,i].gameObject);
                    map[j, i] = null;
                }
            }
        }

        score = 0;
    }

    public void ClearScore()
    {
        score = 0;
        highScore = 0;
        numberGames = 0;
        SaveScore();
    }
}
