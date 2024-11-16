using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomScrollRect : ScrollRect
{
    public bool allowDrag = true;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (allowDrag)
        {
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (allowDrag)
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (allowDrag)
        {
            base.OnEndDrag(eventData);
        }
    }
}
