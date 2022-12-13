using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    // Ballを生成
    // ・BallのPrefabを用意
    // ・Instantiateしてやる
    [SerializeField] GameObject ballPrefab = default;

    private void Start()
    {
        StartCoroutine(Spawns(40));
    }

    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-0.2f,0.2f),8f);
            Instantiate(ballPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(0.04f);
        }
    }
}