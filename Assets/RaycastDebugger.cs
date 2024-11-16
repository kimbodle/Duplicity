using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RaycastDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Debug.Log($"Touch {touch.fingerId}: Phase {touch.phase}, Position {touch.position}");
            }
        }

        if (Input.GetMouseButtonDown(0)) // 터치나 클릭 이벤트
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                Debug.Log($"Raycast Hit: {result.gameObject.name}");
            }
        }
    }
}
