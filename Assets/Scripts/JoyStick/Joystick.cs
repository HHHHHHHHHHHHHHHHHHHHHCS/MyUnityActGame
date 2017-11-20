using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : ScrollRect
{

    protected float radius;

    protected override void Start()
    {

        radius = ((RectTransform)transform).sizeDelta.x * 0.5f;
    }

    public override void OnDrag(PointerEventData eventData)
    {

        base.OnDrag(eventData);
        content.anchoredPosition = Vector3.ClampMagnitude(content.anchoredPosition, radius);
    }
}