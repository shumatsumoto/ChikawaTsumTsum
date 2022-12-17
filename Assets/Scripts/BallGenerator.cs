using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    // Ballを生成
    [SerializeField] GameObject ballPrefab = default;
    // 画像の設定
    // ・設定する画像を用意
    [SerializeField] Sprite[] ballSprites = default;
    // ボムの生成
    // ・画像
    // ・ボムである目印をつける
    [SerializeField] Sprite bombSprite = default;

    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8f);
            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);
            // 画像の設定
            int ballID = Random.Range(0, ballSprites.Length); // 0,1,2,3,4:-1はボム

            // もしボムなら ballID = -1
            // それ以外なら 今までと同じ
            if (Random.Range(0, 100) < 30) // 30%の確率でtrueになる
            {
                ballID = -1;
                ball.GetComponent<SpriteRenderer>().sprite = bombSprite;
            }
            else
            {
                ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID];
            }
            ball.GetComponent<Ball>().id = ballID;
            yield return new WaitForSeconds(0.04f);
        }
    }
}