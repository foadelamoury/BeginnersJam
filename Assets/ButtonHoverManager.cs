using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public static ButtonHoverManager Instance;
    [SerializeField] private RectTransform hoverOverlay;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        // Try to find HoverOverlay if not manually assigned
        if (hoverOverlay == null)
        {
            GameObject overlayObject = GameObject.Find("HoverOverlay");
            if (overlayObject != null)
            {
                hoverOverlay = overlayObject.GetComponent<RectTransform>();
            }
            else
            {
                Debug.LogError("HoverOverlay not found");
            }
        }

        if (hoverOverlay != null)
        {
            hoverOverlay.gameObject.SetActive(false); // Hide it initially
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHover(eventData.pointerEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideHover();
    }

    public void OnSelect(BaseEventData eventData)
    {
        ShowHover(eventData.selectedObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        HideHover();
    }

    private void ShowHover(GameObject target)
    {
        if (hoverOverlay == null) return;

        if (target != null && target.TryGetComponent<RectTransform>(out RectTransform targetRect))
        {
            hoverOverlay.gameObject.SetActive(true);
            hoverOverlay.position = targetRect.position;
            hoverOverlay.sizeDelta = targetRect.sizeDelta;
        }
    }

    private void HideHover()
    {
        if (hoverOverlay == null) return;

        hoverOverlay.gameObject.SetActive(false);
    }
}
