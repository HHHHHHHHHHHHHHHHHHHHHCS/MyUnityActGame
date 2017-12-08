using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameManager : MonoBehaviour
{
    public static FightGameManager Instance
    {
        get;
        private set;
    }

    [SerializeField]
    private AudioClip winAudio, failAudio;

    private PlayerCloth playerCloth;
    private bool respawnEnd;

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

    public RespawnArea RespawnArea
    {
        get;
        private set;
    }



    private void Awake()
    {
        Instance = this;
        playerCloth = GetComponent<PlayerCloth>();
        Player = GameObject.FindWithTag(Tags.player).GetComponent<FightPlayer>();
        RespawnArea = GameObject.Find("RespawnArea").GetComponent<RespawnArea>();
        EnemyManager = GameObject.Find("EnemyManager").transform;

        InitPlayerModel();
        InitFightUI();

        RespawnArea.RespawnEnd += RespawnEnd;
    }

    private void Update()
    {
        if(respawnEnd && EnemyManager.childCount <=0)
        {
            WinGame();
        }
    }

    private void InitPlayerModel()
    {
        var saveData = PlayerModelFileManager.LoadPlayer();
        if (saveData != null)
        {
            playerCloth.ChangeBody(saveData);
        }
    }

    private void InitFightUI()
    {
        FightUIManager.Instance
            .InitJoystick(Player.JoystickBeginEvent, Player.JoystickDragEvent, Player.JoystickEndEvent)
            .InitAttackButton(Player.PlayNormalAttackButton, Player.PlayRangeAttackButton,Player.PlayGunAttackButton);
    }

    public void WinGame()
    {
        AudioSource.PlayClipAtPoint(winAudio, transform.position);
    }

    public void FailGame()
    {
        AudioSource.PlayClipAtPoint(failAudio, transform.position);
    }

    public void RespawnEnd()
    {
        respawnEnd = true;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
