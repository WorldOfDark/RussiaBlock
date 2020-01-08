using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// 缩小
    /// </summary>
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(15.0f, 0.5f);
        mainCamera.transform.DOLocalMoveY(10f, 1.0f);
    }

    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(13f,0.5f);
        mainCamera.transform.DOLocalMoveY(12f,0.5f);
    }
}
