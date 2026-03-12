using UnityEngine;

public class ExplosionGrow : MonoBehaviour
{
    public float growTime = 0.2f;
    public float finalSize = 1.5f;
    public float lingerTime = 1.5f;

    Vector3 startScale;
    Vector3 targetScale;

    float timer = 0f;
    bool finishedGrowing = false;

    void Start()
    {
        startScale = transform.localScale;
        targetScale = Vector3.one * finalSize;
    }

    void Update()
    {
        if (!finishedGrowing)
        {
            timer += Time.deltaTime;

            float t = timer / growTime;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            if (t >= 1f)
            {
                finishedGrowing = true;
                Destroy(gameObject, lingerTime);
            }
        }
    }
}