using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonAnim : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    public Animator anim;

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("Normal");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetTrigger("Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetTrigger("Highlighted"); // thả chuột thì về hover
    }
}
