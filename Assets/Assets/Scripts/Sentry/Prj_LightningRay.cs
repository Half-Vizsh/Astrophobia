using System.Collections;
using UnityEngine;

public class Prj_LigthningRay : MonoBehaviour
{
    public float duration;
    public float scale;
    public float lifeTime;
    void Start()
    {
        StartCoroutine(ScaleOverTime(duration, scale));
        Destroy(gameObject, lifeTime);
    }
    private IEnumerator ScaleOverTime(float duration, float scale) {
    var startScale = transform.localScale;
    var endScale = new Vector3 (startScale.x, scale, startScale.z);
    var elapsed = 0f;

    while (elapsed < duration) {
        var t = elapsed / duration;
        transform.localScale = Vector3.Lerp(startScale, endScale, t);
        elapsed += Time.deltaTime;
        yield return null;
    }
    transform.localScale = endScale;
    }
}
