﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameManager : MonoBehaviour
{
    public static FightGameManager Instance
    {
        get;
        private set;
    }

    public FightPlayer Player
    {
        get;
        private set;
    }

    public Transform EnemyManager
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindWithTag(Tags.player).GetComponent<FightPlayer>();
        EnemyManager = GameObject.Find("EnemyManager").transform;
        InitFightUI();
    }

    void InitFightUI()
    {
        FightUIManager.Instance
            .InitJoystick(Player.JoystickBeginEvent, Player.JoystickDragEvent, Player.JoystickEndEvent)
            .InitAttackButton(Player.PlayNormalAttackButton, Player.PlayRangeAttackButton);

    }


}
