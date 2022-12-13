using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator = default;
    bool isDragging;

    void Start()
    {
        StartCoroutine(ballGenerator.Spawns(40));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 右クリックを押し込んだ時
            OnDragBigin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 右クリックを離した時
            OnDragEnd();
        }
        else if (isDragging)
        {
            OnDragging();
        }
    }

    void OnDragBigin()
    {
        Debug.Log("ドラッグ開始");
        // マウスによるオブジェクトの判定
        // ・Ray
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Debug.Log("Ballにhitしたよ！");
        }
    }
    void OnDragging()
    {
        // Debug.Log("ドラッグ中");
    }
    void OnDragEnd()
    {
        Debug.Log("ドラッグ終了");
    }
}