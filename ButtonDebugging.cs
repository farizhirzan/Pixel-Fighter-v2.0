using UnityEngine;
using UnityEngine.EventSystems;

public class LogEventSystem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked through EventSystem!");
    }
}