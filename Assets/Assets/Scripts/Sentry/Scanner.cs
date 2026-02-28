using UnityEngine;

public class PlacementCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.name);
        Plot plot = other.GetComponent<Plot>();
        if (plot != null)
        {
            plot.Highlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Plot plot = other.GetComponent<Plot>();
        if (plot != null)
        {
            plot.Highlight(false);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
