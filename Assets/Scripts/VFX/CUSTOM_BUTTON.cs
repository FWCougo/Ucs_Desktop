using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CUSTOM_BUTTON : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("BUTTON")]
    [SerializeField] private UnityEvent evento;
    [SerializeField] private float sizeMultiplier;
    [SerializeField] private float sizeAnimDuration = 0.5f;

    [Header("TEXT")]
    [SerializeField] private Vector3 textRotValue;
    [SerializeField] private float textRotDuration = 0.5f;
    [SerializeField] private TMP_Text text;

    Tween textTween;

    public void OnPointerDown(PointerEventData eventData)
    {
        evento.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(sizeMultiplier, sizeAnimDuration).SetEase(Ease.OutElastic);        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, sizeAnimDuration).SetEase(Ease.OutElastic);
    }
}
