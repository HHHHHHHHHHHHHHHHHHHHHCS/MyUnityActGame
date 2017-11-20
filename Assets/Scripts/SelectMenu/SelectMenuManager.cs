using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerModelBase;

public class SelectMenuManager : MonoBehaviour
{


    [SerializeField]
    private SkinnedMeshRenderer headRenderer;
    [SerializeField]
    private SkinnedMeshRenderer handRenderer;
    [SerializeField]
    private SkinnedMeshRenderer footRenderer;
    [SerializeField]
    private SkinnedMeshRenderer upperbodyRenderer;
    [SerializeField]
    private SkinnedMeshRenderer lowerbodyRenderer;
    [SerializeField]
    private Mesh[] headMeshArray;
    [SerializeField]
    private Mesh[] handMeshArray;
    [SerializeField]
    private Mesh[] footMeshArray;
    [SerializeField]
    private Mesh[] upperbodyMeshArray;
    [SerializeField]
    private Mesh[] lowerbodyMeshArray;

    private PlayerModel playerModelInfo = new PlayerModel();


    private readonly Color purple = new Color(0.627451f, 0.12549f, 0.941176f);

    private void Awake()
    {
        InitFind();
        InitPlayerModel();
    }

    void InitFind()
    {
        var uiroot = GameObject.Find(SelectMenuName.uiRooot).transform;
        uiroot.Find(SelectMenuName.headUp).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.head, true); });
        uiroot.Find(SelectMenuName.headDown).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.head, false); });
        uiroot.Find(SelectMenuName.handUp).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.hand, true); });
        uiroot.Find(SelectMenuName.handDown).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.hand, false); });
        uiroot.Find(SelectMenuName.footUp).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.foot, true); });
        uiroot.Find(SelectMenuName.footDown).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.foot, false); });
        uiroot.Find(SelectMenuName.upperbodyUp).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.upperbody, true); });
        uiroot.Find(SelectMenuName.upperbodyDown).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.upperbody, false); });
        uiroot.Find(SelectMenuName.lowerbodyUp).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.lowerbody, true); });
        uiroot.Find(SelectMenuName.lowerbodyDown).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeBody(ChangeBodyType.lowerbody, false); });
        uiroot.Find(SelectMenuName.blueButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(PlayerColor.Blue); });
        uiroot.Find(SelectMenuName.cyanButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(PlayerColor.Cyan); });
        uiroot.Find(SelectMenuName.greenButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(PlayerColor.Green); });
        uiroot.Find(SelectMenuName.purpleButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(PlayerColor.Purple); });
        uiroot.Find(SelectMenuName.redButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(PlayerColor.Red); });
        uiroot.Find(SelectMenuName.whiteButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(PlayerColor.White); });
        uiroot.Find(SelectMenuName.playButton).GetComponent<Button>()
    .onClick.AddListener(SavePlayerInfo);

    }

    void InitPlayerModel()
    {
        var saveData = PlayerModelFileManager.LoadPlayer();
        if (saveData != null)
        {
            ChangeBody(saveData);
        }
    }

    void SavePlayerInfo()
    {
        PlayerModelFileManager.SavePlayer(playerModelInfo);
    }

    public void ChangeBody(PlayerModel playerModel)
    {
        if (playerModel != null)
        {
            ChangeColor(playerModel.playerColor);
            ChangeBody(ChangeBodyType.head, false, playerModel.headModel);
            ChangeBody(ChangeBodyType.hand, false, playerModel.handModel);
            ChangeBody(ChangeBodyType.foot, false, playerModel.footModel);
            ChangeBody(ChangeBodyType.upperbody, false, playerModel.upperbodyModel);
            ChangeBody(ChangeBodyType.lowerbody, false, playerModel.lowerbodyModel);
        }
    }


    private void ChangeBody(ChangeBodyType body, bool isUp, int _index = -1)
    {
        SkinnedMeshRenderer smr;
        Mesh[] meshArray;
        int index;
        switch (body)
        {
            case ChangeBodyType.head:
                smr = headRenderer;
                meshArray = headMeshArray;
                index = playerModelInfo.headModel;
                break;
            case ChangeBodyType.hand:
                smr = handRenderer;
                meshArray = handMeshArray;
                index = playerModelInfo.handModel;
                break;
            case ChangeBodyType.foot:
                smr = footRenderer;
                meshArray = footMeshArray;
                index = playerModelInfo.footModel;
                break;
            case ChangeBodyType.upperbody:
                smr = upperbodyRenderer;
                meshArray = upperbodyMeshArray;
                index = playerModelInfo.upperbodyModel;
                break;
            case ChangeBodyType.lowerbody:
                smr = lowerbodyRenderer;
                meshArray = lowerbodyMeshArray;
                index = playerModelInfo.lowerbodyModel;
                break;
            default:
                return;
        }
        if (_index == -1)
        {
            if (isUp && index == 0)
            {
                index = meshArray.Length;
            }
            index = (isUp ? --index : ++index) % meshArray.Length;
        }
        else
        {
            index = _index;
        }
        smr.sharedMesh = meshArray[index];
        switch (body)
        {
            case ChangeBodyType.head:
                playerModelInfo.headModel = index;
                break;
            case ChangeBodyType.hand:
                playerModelInfo.handModel = index;
                break;
            case ChangeBodyType.foot:
                playerModelInfo.footModel = index;
                break;
            case ChangeBodyType.upperbody:
                playerModelInfo.upperbodyModel = index;
                break;
            case ChangeBodyType.lowerbody:
                playerModelInfo.lowerbodyModel = index;
                break;
            default:
                return;
        }

    }

    private void ChangeColor(PlayerColor playerColor)
    {
        Color col;
        playerModelInfo.playerColor = playerColor;
        switch (playerColor)
        {
            case PlayerColor.Blue:
                col = Color.blue;
                break;
            case PlayerColor.Cyan:
                col = Color.cyan;
                break;
            case PlayerColor.Green:
                col = Color.green;
                break;
            case PlayerColor.Purple:
                col = purple;
                break;
            case PlayerColor.Red:
                col = Color.red;
                break;
            case PlayerColor.White:
                col = Color.white;
                break;
            default:
                col = Color.white;
                break;
        }

        headRenderer.material.SetColor("_Color", col);
        handRenderer.material.SetColor("_Color", col);
        footRenderer.material.SetColor("_Color", col);
        upperbodyRenderer.material.SetColor("_Color", col);
        lowerbodyRenderer.material.SetColor("_Color", col);
    }
}
