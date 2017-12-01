using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FightUIManager : MonoBehaviour
{
    private static FightUIManager _instance;

    public static FightUIManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.Find(FightName.uiRooot)
                    .GetComponent<FightUIManager>()._InitFightUI();
            }
            return _instance;
        }
    }

    private JoystickEvent joystick;
    private Button normalAttack, rangeAttack,gunAttack;

    public FightUIManager _InitFightUI()
    {
        if (!_instance)
        {
            var uiRoot = transform;
            joystick = uiRoot.Find(FightName.joystick).GetComponent<JoystickEvent>();
            normalAttack = uiRoot.Find(FightName.normalAttack).GetComponent<Button>();
            rangeAttack = uiRoot.Find(FightName.rangeAttack).GetComponent<Button>();
            gunAttack = uiRoot.Find(FightName.gunAttack).GetComponent<Button>();
        }
        return this;
    }

    public FightUIManager InitJoystick(UnityAction beginEvent, UnityAction<Vector3> dragEvent, UnityAction endEvent)
    {
        joystick.beginControl.AddListener(beginEvent);
        joystick.controlling.AddListener(dragEvent);
        joystick.endControl.AddListener(endEvent);
        return this;
    }

    public FightUIManager InitAttackButton(UnityAction normalEvent, UnityAction rangeEvent)
    {
        normalAttack.onClick.AddListener(normalEvent);
        rangeAttack.onClick.AddListener(rangeEvent);
        return this;
    }

    public void SetRangeAttack(bool tf)
    {
        rangeAttack.interactable = tf;
    }

    public void SetGunAttack(bool tf)
    {
        gunAttack.interactable = tf;
    }
}
