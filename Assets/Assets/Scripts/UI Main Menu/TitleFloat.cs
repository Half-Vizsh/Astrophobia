using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    public float amplitude = 20f;
    public float speed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;

        transform.localPosition =
            startPos + new Vector3(0f, yOffset, 0f);
    }
}