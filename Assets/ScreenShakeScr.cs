using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeScr : MonoBehaviour
{
    public float duration = 1f;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ShakeScreen());
        }*/
    }

    public IEnumerator ShakeScreen()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength * 300;
            yield return null;
        }
        transform.position = startPos;
    }
}
