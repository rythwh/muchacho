using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public RectTransform followArea;
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        Cursor.visible = false;
    }
    
    void Update()
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            Input.mousePosition, 
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, 
            out localMousePosition
        );
        
        Vector2 clampedPosition = ClampToArea(localMousePosition, followArea);
        
        rectTransform.anchoredPosition = clampedPosition;
    }
    
    Vector2 ClampToArea(Vector2 position, RectTransform area)
    {
        Vector2 min = area.rect.min + (Vector2)area.anchoredPosition;
        Vector2 max = area.rect.max + (Vector2)area.anchoredPosition;

        float x = Mathf.Clamp(position.x, min.x, max.x);
        float y = Mathf.Clamp(position.y, min.y, max.y);

        return new Vector2(x, y);
    }
}
