using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameManager : MonoBehaviour
{
    FightPlayer player;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.player).GetComponent<FightPlayer>();
        InitFightUI();
    }

    void InitFightUI()
    {
        FightUIManager.Instance
            .InitJoystick(player.JoystickBeginEvent, player.JoystickDragEvent, player.JoystickEndEvent)
            .InitAttackButton(player.PlayNormalAttackButton, player.PlayRangeAttackButton);

    }


}
