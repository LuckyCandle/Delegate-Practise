using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerStatus[] playerStatuses;

    [HideInInspector]
    public string topKillName, topDeathName, topFlagName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        topKillName = getTopName((_player) => _player.killNum);
        topDeathName = getTopName((_player) => _player.deathNum);
        topFlagName = getTopName((_player) => _player.flagNum);

        UIManager.instance.UpdatePlayerTitle(topKillName,"<color=orange>³¬Éñ£¡£¡£¡</color>",Logger.Log);
        UIManager.instance.UpdatePlayerTitle(topDeathName, "<color=green>³¬¹í£¡£¡£¡</color>", Logger.Log);
        UIManager.instance.UpdatePlayerTitle(topFlagName, "<color=blue>Öú¹¥Íõ£¡£¡£¡</color>", Logger.Log);
    }

    public  string getTopName(Func<PlayerStatus,int> func) {
        int bestRecord = 0;
        string topName = "";
        foreach (var player in playerStatuses) 
        {
            int tempNum = func(player);
            if (tempNum >= bestRecord) {
                bestRecord = tempNum;
                topName = player.playerName;
            }
            
        }
        return topName;
    }

    public void BubbleSort<T>(T[] _playerStatus, Func<T,T,bool> func) {
        for (int i = 0; i < _playerStatus.Length; i++) {
            for (int j = 0; j < _playerStatus.Length - 1 - i; j++) {
                if (func(_playerStatus[j], _playerStatus[j + 1])) {
                    T temp = _playerStatus[j];
                    _playerStatus[j] = _playerStatus[j + 1];
                    _playerStatus[j + 1] = temp;
                }
            }
        }
    }

}
