using System;
using UnityEngine;

public class GameBoostrap : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;


    private void Awake()
    {
        _gameManager.InitGame();
        Debug.Log("Запустил");
    }
}
