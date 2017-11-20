
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class JoystickEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [System.Serializable]
    public class VirtualJoystickEvent : UnityEvent<Vector3> { }

    public Transform content;
    public UnityEvent beginControl;
    public VirtualJoystickEvent controlling;
    public UnityEvent endControl;



    public void OnBeginDrag(PointerEventData eventData)
    {
        beginControl.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (content)
        {
            controlling.Invoke(content.localPosition.normalized);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endControl.Invoke();
    }
}
