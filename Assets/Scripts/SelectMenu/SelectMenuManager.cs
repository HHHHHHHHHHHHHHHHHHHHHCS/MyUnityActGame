using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenuManager : MonoBehaviour
{
    private enum ChangeBodyType
    {
        head,
        hand,
        foot,
        upperbody,
        lowerbody
    }

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

    private int headMeshIndex = 0;
    private int handMeshIndex = 0;
    private int footMeshIndex = 0;
    private int upperbodyMeshIndex = 0;
    private int lowerbodyMeshIndex = 0;

    private void Awake()
    {
        InitFind();
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
            .onClick.AddListener(() => { ChangeColor(Color.blue); });
        uiroot.Find(SelectMenuName.cyanButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(Color.cyan); });
        uiroot.Find(SelectMenuName.greenButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(Color.green); });
        uiroot.Find(SelectMenuName.purpleButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(new Color(0.627451f, 0.12549f, 0.941176f)); });
        uiroot.Find(SelectMenuName.redButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(Color.red); });
        uiroot.Find(SelectMenuName.whiteButton).GetComponent<Button>()
            .onClick.AddListener(() => { ChangeColor(Color.white); });
    }


    private void ChangeBody(ChangeBodyType body, bool isUp)
    {
        SkinnedMeshRenderer smr;
        Mesh[] meshArray;
        int index;
        switch (body)
        {
            case ChangeBodyType.head:
                smr = headRenderer;
                meshArray = headMeshArray;
                index = headMeshIndex;
                break;
            case ChangeBodyType.hand:
                smr = handRenderer;
                meshArray = handMeshArray;
                index = handMeshIndex;
                break;
            case ChangeBodyType.foot:
                smr = footRenderer;
                meshArray = footMeshArray;
                index = footMeshIndex;
                break;
            case ChangeBodyType.upperbody:
                smr = upperbodyRenderer;
                meshArray = upperbodyMeshArray;
                index = upperbodyMeshIndex;
                break;
            case ChangeBodyType.lowerbody:
                smr = lowerbodyRenderer;
                meshArray = lowerbodyMeshArray;
                index = lowerbodyMeshIndex;
                break;
            default:
                return;
        }
        if (isUp && index == 0)
        {
            index = meshArray.Length;
        }
        index = (isUp ? --index : ++index) % meshArray.Length;
        smr.sharedMesh = meshArray[index];
        switch (body)
        {
            case ChangeBodyType.head:
                headMeshIndex = index;
                break;
            case ChangeBodyType.hand:
                handMeshIndex = index;
                break;
            case ChangeBodyType.foot:
                footMeshIndex = index;
                break;
            case ChangeBodyType.upperbody:
                upperbodyMeshIndex = index;
                break;
            case ChangeBodyType.lowerbody:
                smr = lowerbodyRenderer;
                meshArray = lowerbodyMeshArray;
                lowerbodyMeshIndex = index;
                break;
            default:
                return;
        }

    }

    private void ChangeColor(Color col)
    {
        headRenderer.material.SetColor("_Color", col);
        handRenderer.material.SetColor("_Color", col);
        footRenderer.material.SetColor("_Color", col);
        upperbodyRenderer.material.SetColor("_Color", col);
        lowerbodyRenderer.material.SetColor("_Color", col);
    }
}
