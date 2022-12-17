using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    // のこり
    // ・上から落とす:消した数だけ：済
    // ・スコア:消した数×100：済

    // ・ドラッグの時
    // 　・Ballを少し大きくする:Ballをリストに追加するとき大きくする：済
    // 　・色をかえる：済
    // ・弾けるエフェクト

    // ・パラメータ調節：済
    // ・音（リクエストがあれば）

    [SerializeField] BallGenerator ballGenerator = default;
    bool isDragging;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();
    Ball currentDraggingBall;
    int score;
    [SerializeField] Text scoreText = default;


    void Start()
    {
        score = 0;
        AddScore(0);
        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.initBallCount));
    }

    void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
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
        // マウスによるオブジェクトの判定
        // ・Ray
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Ball ball = hit.collider.GetComponent<Ball>();
            AddRemoveBall(ball);
            isDragging = true;
        }
    }
    void OnDragging()
    {
        // Debug.Log("ドラッグ中");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            // ・同じ種類&距離が近かったらListに追加
            //  ・何と？=>現在ドラッグしているオブジェクトと
            Ball ball = hit.collider.GetComponent<Ball>();

            // 同じ種類
            if (ball.id == currentDraggingBall.id)
            {
                // 距離が近い
                float distance = Vector2.Distance(ball.transform.position, currentDraggingBall.transform.position);
                if (distance < ParamsSO.Entity.ballDistance)
                {
                    AddRemoveBall(ball);
                }
            }
        }
    }
    void OnDragEnd()
    {
        int removeCount = removeBalls.Count;
        if (removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                Destroy(removeBalls[i].gameObject);
            }
            StartCoroutine(ballGenerator.Spawns(removeCount));
            AddScore(removeCount * ParamsSO.Entity.scorePoint);
        }
        // 全てのremoveBallのサイズを戻す
        for (int i = 0; i < removeCount; i++)
        {
            removeBalls[i].GetComponent<SpriteRenderer>().color = Color.white;
            removeBalls[i].transform.localScale = Vector3.one;
        }
        removeBalls.Clear();
        isDragging = false;
    }

    void AddRemoveBall(Ball ball)
    {
        currentDraggingBall = ball;
        if (removeBalls.Contains(ball) == false)
        {
            ball.transform.localScale = Vector3.one * 1.4f;
            ball.GetComponent<SpriteRenderer>().color = Color.yellow;
            removeBalls.Add(ball);
        }
    }
}