using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerModelBase;
using UnityEngine.SceneManagement;

public class SelectMenuManager : MonoBehaviour
{
    private PlayerCloth playerCloth;

    private void Awake()
    {
        playerCloth = GetComponent<PlayerCloth>();
        InitFind();
        InitPlayerModel();
    }

    void InitFind()
    {
        var uiroot = GameObject.Find(SelectMenuName.uiRooot).transform;
        uiroot.Find(SelectMenuName.headUp).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.head, true); });
        uiroot.Find(SelectMenuName.headDown).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.head, false); });
        uiroot.Find(SelectMenuName.handUp).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.hand, true); });
        uiroot.Find(SelectMenuName.handDown).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.hand, false); });
        uiroot.Find(SelectMenuName.footUp).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.foot, true); });
        uiroot.Find(SelectMenuName.footDown).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.foot, false); });
        uiroot.Find(SelectMenuName.upperbodyUp).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.upperbody, true); });
        uiroot.Find(SelectMenuName.upperbodyDown).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.upperbody, false); });
        uiroot.Find(SelectMenuName.lowerbodyUp).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.lowerbody, true); });
        uiroot.Find(SelectMenuName.lowerbodyDown).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeBody(ChangeBodyType.lowerbody, false); });
        uiroot.Find(SelectMenuName.blueButton).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeColor(PlayerColor.Blue); });
        uiroot.Find(SelectMenuName.cyanButton).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeColor(PlayerColor.Cyan); });
        uiroot.Find(SelectMenuName.greenButton).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeColor(PlayerColor.Green); });
        uiroot.Find(SelectMenuName.purpleButton).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeColor(PlayerColor.Purple); });
        uiroot.Find(SelectMenuName.redButton).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeColor(PlayerColor.Red); });
        uiroot.Find(SelectMenuName.whiteButton).GetComponent<Button>()
            .onClick.AddListener(() => { playerCloth.ChangeColor(PlayerColor.White); });
        uiroot.Find(SelectMenuName.playButton).GetComponent<Button>()
            .onClick.AddListener(() => { SavePlayerInfo(); SceneManager.LoadScene("Fight"); });

    }

    void InitPlayerModel()
    {
        var saveData = PlayerModelFileManager.LoadPlayer();
        if (saveData != null)
        {
            playerCloth.ChangeBody(saveData);
        }
    }

    void SavePlayerInfo()
    {
        PlayerModelFileManager.SavePlayer(playerCloth.PlayerModelInfo);
    }

}
