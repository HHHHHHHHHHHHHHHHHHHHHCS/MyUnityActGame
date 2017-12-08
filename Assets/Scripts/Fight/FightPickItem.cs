using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPickItem : MonoBehaviour
{
    public enum PickItemType
    {
        dualSword,
        gun,
        hp
    }

    [SerializeField]
    private PickItemType itemType;
    [SerializeField]
    private AudioClip pickAudio;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.player))
        {
            var player = FightGameManager.Instance.Player;
            bool haveItem = false;
            switch (itemType)
            {
                case PickItemType.dualSword:
                    haveItem = player.GetRangeItem();
                    break;
                case PickItemType.gun:
                    haveItem = player.GetGunItem();
                    break;
                case PickItemType.hp:
                    haveItem = player.GetHPItem();
                    break;
                default:
                    break;
            }
            if (haveItem)
            {
                AudioSource.PlayClipAtPoint(pickAudio, transform.position);
                Destroy(gameObject);
            }
        }
    }
}

