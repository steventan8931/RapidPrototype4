using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendTextFade : MonoBehaviour
{
    public Animator fadeAnim;
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        fadeAnim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        Invoke(nameof(startFading), 4.5f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startFading()
    {
        fadeAnim.SetBool("IsFade", true);
    }
}
