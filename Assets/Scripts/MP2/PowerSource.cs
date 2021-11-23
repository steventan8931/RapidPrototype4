using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSource : MonoBehaviour
{
    public float m_CurrentHP = 200.0f;
    public float m_MaxHP = 200.0f;

    public Image m_PowerSourceHPBar;
    public Animator castleAnim;
    public GameObject castleSmoke;
    public GameObject warningUi;
    public GameObject failUi;
    private RestrictControl restrictCtrl;
    public ScreenShakeScr screenshake;
    public bool isShowingWarningUi = false;
    public float warningTimer = 0f;

    private MP2AudioManager m_AudioManager;
    private AudioSource m_AudioSource;
    bool cacheLoseSound = false;
    public GameObject m_EndCutScene;
    public GameObject m_BackGroundTrack;
    private void Awake()
    {
        restrictCtrl = FindObjectOfType<RestrictControl>();
        //screenshake = FindObjectOfType<ScreenShakeScr>();
        m_AudioSource = GetComponent<AudioSource>();
       m_AudioManager = FindObjectOfType<MP2AudioManager>();
    }
    public void receiveDmg(float dmg)
    {
        if (m_CurrentHP <= 0)
        {
            m_CurrentHP = 0;
            m_BackGroundTrack.SetActive(false);
            m_EndCutScene.SetActive(true);
            // game over func
            failFunc();
        }
        else
        {
            m_CurrentHP -= dmg;
            
            //screenshake.StartCoroutine(screenshake.ShakeScreen());
            if(isShowingWarningUi == false)
            {
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.Play();
                }
                //m_AudioManager.PlaySound("Siren");
                isShowingWarningUi = true;
                warningUi.SetActive(true);
                warningUi.GetComponent<CanvasGroup>().alpha = 1;
                castleAnim.SetBool("IsAttacked", true);
            }
            warningTimer = 0.5f;
            //Show warning UI
            //warningUi.SetActive(true);
            Invoke(nameof(decayOnWarning), 1.2f);
        }

    }
    public void decayOnWarning()
    {
        if(warningTimer >0)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0)
            {
                warningTimer = 0;
                //make warningUI disappear
                warningUi.GetComponent<Animator>().SetBool("isFading", true);
                castleAnim.SetBool("IsAttacked", false);
                Invoke(nameof(disableWarning), 1.1f);
            }
        }
        
    }
    void updateHpBar()
    {
        m_PowerSourceHPBar.fillAmount = (m_CurrentHP/m_MaxHP);
    }

    public void disableWarning()
    {
        warningUi.GetComponent<Animator>().SetBool("isFading", false);
        warningUi.GetComponent<CanvasGroup>().alpha = 1;
        isShowingWarningUi = false;
        warningUi.SetActive(false);
    }
    public void failFunc()
    {
        warningUi.SetActive(false);
        failUi.SetActive(true);
        castleAnim.SetBool("IsDestroyed", true);
        castleSmoke.SetActive(true);
        //unlock cursor and make it visible
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        restrictCtrl.DisableControls();
        //if (!cacheLoseSound)
        //{
        //    m_AudioManager.PlaySound("Lose");
        //    cacheLoseSound = true;
        //}
    }
    // Update is called once per frame
    void Update()
    {
        updateHpBar();
        decayOnWarning();
        
    }
}
