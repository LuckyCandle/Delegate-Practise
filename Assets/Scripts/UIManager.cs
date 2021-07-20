using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("===== Left UI =====")]
    public GameObject[] slots;

    [Header("===== Right UI =====")]
    public Image playerImage;
    public Text playerName;
    public Text title;
    public Text status;

    [Header("===== Order Image =====")]
    public Sprite AscendingSprite;
    public Sprite DscendingSprite;
    public Image[] orderImages;

    [HideInInspector]
    public int currentIndex; //µ±Ç°Ò³Êý

    private bool isAscending01, isAscending02, isAscending03;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateLeftUI();
        UpdateRightUI();
        ChangeAlphaAndParticle();
    }

    public void UpdateLeftUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            PlayerStatus currentPlayer = GameManager.instance.playerStatuses[i];
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = currentPlayer.profile_sprite;
            slots[i].transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}/{1}/{2}", currentPlayer.killNum, currentPlayer.deathNum, currentPlayer.flagNum);
            slots[i].transform.GetChild(2).GetComponent<Text>().text = currentPlayer.datetime;
        }
    }

    public void UpdateRightUI()
    {
        PlayerStatus playerStatus = GameManager.instance.playerStatuses[currentIndex];
        playerImage.sprite = playerStatus.player_sprite;
        playerName.text = playerStatus.playerName;
        status.text = string.Format("{0}/{1}/{2}", playerStatus.killNum, playerStatus.deathNum, playerStatus.flagNum);
        title.text = playerStatus.title;
    }

    public void UpdatePlayerTitle(string _topName,string _title, Func<PlayerStatus,string> func)
    {
        foreach (var _player in GameManager.instance.playerStatuses)
        {
            if (_player.playerName == _topName) {
                _player.title += _title;
                _player.datetime = func(_player);
            }
        }
    }

    public void Reverse() {
        Array.Reverse(GameManager.instance.playerStatuses);
    }

    public void OnClickKillNumSort() {
        isAscending02 = false;
        isAscending03 = false;

        isAscending01 = !isAscending01;
        if (isAscending01)
        {
            GameManager.instance.BubbleSort<PlayerStatus>(GameManager.instance.playerStatuses, PlayerStatus.CompareKillNum);
            UpdateOrderButton(0, AscendingSprite);
        }
        else {
            Reverse();
            UpdateOrderButton(0, DscendingSprite);
        }     
        UpdateLeftUI();
        UpdateRightUI();
    }

    public void OnClickDeathNumSort()
    {
        isAscending01 = false;
        isAscending03 = false;

        isAscending02 = !isAscending02;
        if (isAscending02)
        {
            GameManager.instance.BubbleSort<PlayerStatus>(GameManager.instance.playerStatuses, PlayerStatus.CompareDeathNum);
            UpdateOrderButton(1, AscendingSprite);
        }
        else
        {
            Reverse();
            UpdateOrderButton(1, DscendingSprite);
        }
        UpdateLeftUI();
        UpdateRightUI();
    }

    public void OnClickFlagNumSort()
    {
        isAscending02 = false;
        isAscending01 = false;

        isAscending03 = !isAscending03;
        if (isAscending03)
        {
            GameManager.instance.BubbleSort<PlayerStatus>(GameManager.instance.playerStatuses, PlayerStatus.CompareFlagNum);
            UpdateOrderButton(2, AscendingSprite);
        }
        else
        {
            Reverse();
            UpdateOrderButton(2, DscendingSprite);
        }
        UpdateLeftUI();
        UpdateRightUI();
    }

    public void UpdateOrderButton(int _index, Sprite _sprite) {
        for (int i = 0; i < orderImages.Length; i++) {
            if (i == _index)
            {
                orderImages[i].gameObject.SetActive(true);
                orderImages[i].sprite = _sprite;
            }
            else {
                orderImages[i].gameObject.SetActive(false);
            }
        }
    }


    #region //»»Ò³Æ÷
    public void NextPage() {
        currentIndex++;
        if (currentIndex > slots.Length - 1) {
            currentIndex = 0;
        }
        UpdateRightUI();
        ChangeAlphaAndParticle();
    }

 
    public void PreviousPage() {
        currentIndex--;
        if (currentIndex < 0) {
            currentIndex = slots.Length - 1;
        }
        UpdateRightUI();
        ChangeAlphaAndParticle();
    }

    private void ChangeAlphaAndParticle() {
        for (int i = 0; i < slots.Length; i++)
        {
            PlayerStatus currentPlayer = GameManager.instance.playerStatuses[i];
            if (currentIndex == i)
            {
                slots[i].GetComponent<CanvasGroup>().alpha = 1;
                slots[i].transform.GetChild(0).GetComponentInChildren<ParticleSystem>().Play();
            }
            else {
                slots[i].GetComponent<CanvasGroup>().alpha = 0.25f;
                slots[i].transform.GetChild(0).GetComponentInChildren<ParticleSystem>().Stop();
            }
        }
    }
    #endregion
}
