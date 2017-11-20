using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameManager : MonoBehaviour
{
    FightPlayer player;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.player).GetComponent<FightPlayer>();
        var joystick = GameObject.Find(FightName.joystick).GetComponent<JoystickEvent>();
        joystick.beginControl.AddListener(player.JoystickBeginEvent);
        joystick.controlling.AddListener(player.JoystickDragEvent);
        joystick.endControl.AddListener(player.JoystickEndEvent);
    }


}
