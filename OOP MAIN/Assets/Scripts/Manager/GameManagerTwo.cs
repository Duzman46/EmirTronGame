using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTwo : MonoBehaviour
{
    public static GameManagerTwo Instance;

    private void Awake()
    {
        Instance = this;
    }
}
