using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerModelBase;

public class PlayerCloth : MonoBehaviour
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

    private PlayerModel _playerModelInfo;
    public PlayerModel PlayerModelInfo
    {
        get
        {
            if (_playerModelInfo == null)
            {
                _playerModelInfo = new PlayerModel();
            }
            return _playerModelInfo;
        }

        private set
        {
            _playerModelInfo = value;
        }
    }

    private readonly Color purple = new Color(0.627451f, 0.12549f, 0.941176f);

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

    public void ChangeBody(ChangeBodyType body, bool isUp, int _index = -1)
    {
        SkinnedMeshRenderer smr;
        Mesh[] meshArray;
        int index;
        switch (body)
        {
            case ChangeBodyType.head:
                smr = headRenderer;
                meshArray = headMeshArray;
                index = PlayerModelInfo.headModel;
                break;
            case ChangeBodyType.hand:
                smr = handRenderer;
                meshArray = handMeshArray;
                index = PlayerModelInfo.handModel;
                break;
            case ChangeBodyType.foot:
                smr = footRenderer;
                meshArray = footMeshArray;
                index = PlayerModelInfo.footModel;
                break;
            case ChangeBodyType.upperbody:
                smr = upperbodyRenderer;
                meshArray = upperbodyMeshArray;
                index = PlayerModelInfo.upperbodyModel;
                break;
            case ChangeBodyType.lowerbody:
                smr = lowerbodyRenderer;
                meshArray = lowerbodyMeshArray;
                index = PlayerModelInfo.lowerbodyModel;
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
                PlayerModelInfo.headModel = index;
                break;
            case ChangeBodyType.hand:
                PlayerModelInfo.handModel = index;
                break;
            case ChangeBodyType.foot:
                PlayerModelInfo.footModel = index;
                break;
            case ChangeBodyType.upperbody:
                PlayerModelInfo.upperbodyModel = index;
                break;
            case ChangeBodyType.lowerbody:
                PlayerModelInfo.lowerbodyModel = index;
                break;
            default:
                return;
        }

    }

    public void ChangeColor(PlayerColor playerColor)
    {
        Color col;
        PlayerModelInfo.playerColor = playerColor;
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
