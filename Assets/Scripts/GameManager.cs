using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public const int GRID_SIZE_X = 4;
    public const int GRID_SIZE_Y = 4;
    public const float NODE_HEIGHT = 0.09f;

    private void Awake()
    {
        Instance = this;
    }
}