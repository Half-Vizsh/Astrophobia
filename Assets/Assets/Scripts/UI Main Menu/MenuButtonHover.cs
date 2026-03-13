using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite hoverSprite;

    public float hoverScale = 1.1f;
    public float smoothSpeed = 8f;

    Image image;

    Vector3 originalScale;
    Vector3 targetScale;

    void Start()
    {
        image = GetComponent<Image>();

        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * smoothSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
        targetScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
        targetScale = originalScale;
    }
}