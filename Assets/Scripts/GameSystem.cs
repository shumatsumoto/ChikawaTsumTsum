using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator = default;
    void Start()
    {
        StartCoroutine(ballGenerator.Spawns(40));
    }

    void Update()
    {

    }
}