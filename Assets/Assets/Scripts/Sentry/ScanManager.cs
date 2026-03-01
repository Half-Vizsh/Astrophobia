using UnityEngine;
using System.Collections.Generic;

public class ScanManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform scanner;
    [SerializeField] private Transform plotsContainer;

    [Header("Rhombus")]
    [SerializeField] private float rhombusExtent = 0.9f;

    private readonly Dictionary<Vector2Int, Plot> grid = new();
    private readonly List<Plot> allPlots = new();

    void Start()
    {
        grid.Clear();
        allPlots.Clear();

        foreach (Plot p in plotsContainer.GetComponentsInChildren<Plot>())
        {
            Vector2Int key = new Vector2Int(p.row, p.col);
            grid[key] = p;
            allPlots.Add(p);
        }
    }

    void Update()
    {
        ClearHighlights();

        Plot center = GetClosestPlot(scanner.position);
        if (center == null) return;

        Vector2 delta = scanner.position - center.transform.position;

        if (IsInsideRhombus(delta))
        {
            HighlightRhombus(center.row, center.col);
        }
    }

    bool IsInsideRhombus(Vector2 delta)
    {
        return Mathf.Abs(delta.x) + Mathf.Abs(delta.y) <= rhombusExtent;
    }

    void HighlightRhombus(int r, int c)
    {
        Vector2Int[] offsets =
        {
            new(0, 0),
            new(1, 0),
            new(-1, 0),
            new(0, 1),
            new(0, -1)
        };

        foreach (var o in offsets)
        {
            Vector2Int key = new Vector2Int(r + o.x, c + o.y);
            if (grid.TryGetValue(key, out var plot))
                plot.Highlight(true);
        }
    }

    void ClearHighlights()
    {
        foreach (var p in allPlots)
            p.Highlight(false);
    }

    Plot GetClosestPlot(Vector3 pos)
    {
        Plot closest = null;
        float best = float.MaxValue;

        foreach (var p in allPlots)
        {
            float d = Vector2.Distance(pos, p.transform.position);
            if (d < best)
            {
                best = d;
                closest = p;
            }
        }

        return closest;
    }
}