using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundDetail : MonoBehaviour
{
    public Transform LightPart;
    public Transform DarkPart;

    private bool IsUp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BackGroundAnim());
    }
    IEnumerator BackGroundAnim()
    {
        float elapsed = 0f;
        float durtation = 3f;

        while (elapsed < durtation)
        {
            elapsed += Time.deltaTime;
            if (!IsUp)
            {
                LightPart.position += new Vector3(1, 1) * Time.deltaTime;
                DarkPart.position -= new Vector3(1, 0) * Time.deltaTime;
            }
            else
            {
                LightPart.position -= new Vector3(1, 1) * Time.deltaTime;
                DarkPart.position += new Vector3(1, 0) * Time.deltaTime;
            }
            yield return null;
        }
        IsUp = !IsUp;
        StartCoroutine(BackGroundAnim());
    }
}
