using UnityEngine;

public class Plot : MonoBehaviour
{
    public int row;
    public int col;

    private SpriteRenderer sr;
    private Color defaultColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
    }

    public void Highlight(bool state)
    {
        sr.color = state ? Color.cyan : defaultColor;
    }
}