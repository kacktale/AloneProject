using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySaw : MonoBehaviour
{
    private Vector2 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        StartCoroutine(GetCloser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetCloser()
    {
        yield return new WaitForSeconds(1.0f);
        Vector2 start = transform.localPosition;
        Vector2 a = start;
        Vector2 b = Vector2.zero;
        float duration = 2f;

        while (true)
        {
            // A → B
            yield return MoveTo(a, b, duration);
            // B → A
            yield return MoveTo(b, a, duration);
        }

    }

    IEnumerator MoveTo(Vector2 from, Vector2 to, float duration)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }

        transform.localPosition = to;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyZone")) Destroy(gameObject);
    }
}
