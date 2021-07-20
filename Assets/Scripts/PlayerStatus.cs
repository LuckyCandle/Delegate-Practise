using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public string playerName;
    public int killNum;
    public int deathNum;
    public int flagNum;
    public Sprite profile_sprite;
    public Sprite player_sprite;

    [HideInInspector]
    public string title;
    [HideInInspector]
    public string datetime;

    public static bool CompareKillNum(PlayerStatus player1, PlayerStatus player2) {
        return player1.killNum >= player2.killNum;
    }
    public static bool CompareDeathNum(PlayerStatus player1, PlayerStatus player2)
    {
        return player1.deathNum >= player2.deathNum;
    }
    public static bool CompareFlagNum(PlayerStatus player1, PlayerStatus player2)
    {
        return player1.flagNum >= player2.flagNum;
    }
}
