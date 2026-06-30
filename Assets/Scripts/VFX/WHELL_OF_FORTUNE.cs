using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class WHELL_OF_FORTUNE : MonoBehaviour
{
    [SerializeField] private Image option1;
    [SerializeField] private Image option2;
    [SerializeField] private Image option3;

    [SerializeField] private Sprite[] images;

    [SerializeField] private bool isSpinnig = false;

    [SerializeField] private float timeSpinnig = 3;

    [Header("EFEITOS DA ROLETA")]
    public int nEfeitosNegativos = 1;

    public int nEfeitosPositivos = 1;


    [Header("SFX")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip spinningClip;
    [SerializeField] private AudioClip failClip;
    [SerializeField] private AudioClip sucessClip;

    [Header("TXT")]
    [SerializeField] private TMP_Text result_TXT;
    [SerializeField] private GameObject doubleCoins_GO;

    [Header("VFX")]
    [SerializeField] private Volume globalVolume;

    [SerializeField] Vignette vignette;
    [SerializeField] private Color normalVignette;
    [SerializeField] private Color blindVignette;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        timeSpinnig = spinningClip.length;
        result_TXT.gameObject.SetActive(false);
        result_TXT.text = "";

        globalVolume.profile.TryGet(out vignette);

        ClearEffects();
    }


    public void ClearEffects()
    {
        UnBlind();
        DoubleCoins(false);
    }

    IEnumerator LimparEfeitos(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        ClearEffects();

        yield return null;
    }


    public void EscolherEfeitoNegativo()
    {
        int _escolhido = Random.Range(0, nEfeitosNegativos);

        if(_escolhido == 0)
        {
            Blindness();
        }

        StartCoroutine(LimparEfeitos(45));
    }

    #region CEGUEIRA
    float waitForSeconds = 0.025f;
    float changeStep = 0.01f;
    Color vignetteColor;
    [ContextMenu("BLIND")]
    public void Blindness()
    {        
        result_TXT.gameObject.SetActive(true);
        result_TXT.text = "CEGUEIRA";
        StartCoroutine(ChangeVignetteSmoothness());
        StartCoroutine(ChangeVignetteIntensity());
        ChangeVignetteColor(blindVignette, 3);
    }
    [ContextMenu("UNBLIND")]
    public void UnBlind()
    {
        StartCoroutine(ReduceVignetteSmoothness());
        StartCoroutine(ReduceVignetteIntensity());
        ChangeVignetteColor(normalVignette, 3);
    }

    public void ChangeVignetteColor(Color targetColor, float duration)
    {
        DOTween.To(
            () => vignette.color.value,      // getter
            x => vignette.color.value = x,   // setter
            targetColor,
            duration
        );
    }

    IEnumerator ReduceVignetteSmoothness()
    {
        while (vignette.smoothness.value > 0.578f)
        {
            vignette.smoothness.value -= changeStep;
            yield return new WaitForSeconds(waitForSeconds);
        }      

        yield return null;
    }
    IEnumerator ChangeVignetteSmoothness()
    {
        while (vignette.smoothness.value < 1)
        {
            vignette.smoothness.value += changeStep;
            yield return new WaitForSeconds(waitForSeconds);
        }        

        yield return null;
    }

    IEnumerator ReduceVignetteIntensity()
    {
        while (vignette.intensity.value > 0.4f)
        {
            vignette.intensity.value -= changeStep;
            yield return new WaitForSeconds(waitForSeconds);
        }       

        yield return null;
    }
    IEnumerator ChangeVignetteIntensity()
    {
        while(vignette.intensity.value < 1)
        {
            vignette.intensity.value += changeStep;
            yield return new WaitForSeconds(waitForSeconds);
        }        

        yield return null;  
    }

    #endregion CEGUEIRA


    public void EscolherEfeitoPositivo()
    {
        int _escolhido = Random.Range(0, nEfeitosNegativos);

        if (_escolhido == 0)
        {
            DoubleCoins(true);
        }

        StartCoroutine(LimparEfeitos(10));
    }



    #region 2X MOEDAS
    Tween DoubleCoinVFX;

    void DoubleCoins(bool value)
    {
        if (value)
        {
            result_TXT.gameObject.SetActive(true);
            result_TXT.text = "2X MOEDAS";
            doubleCoins_GO.SetActive(true);
            DoubleCoinVFX = doubleCoins_GO.transform.DOScale(1.2f,1).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            DoubleCoinVFX.Kill();
            doubleCoins_GO.SetActive(false);
            result_TXT.gameObject.SetActive(false);
            doubleCoins_GO.transform.localScale = Vector3.one;
        }

        GAME_MANAGER.Instance.DoubleCoins(value);

    }       

    #endregion

    [ContextMenu("ANIMATE THIS")]
    public void SPIN_THE_WHEEL()
    {
        transform.DOScale(1, 1);
        StartCoroutine(AnimateOptions());
    }
    IEnumerator AnimateOptions()
    {
        source.PlayOneShot(spinningClip);

        isSpinnig = true;

        float _elapsed = 0;

        float _waitToChangeImg = 0.2f;

        int _selected = Random.Range(0, 2);

        while(isSpinnig)
        {
            option1.sprite = images[Random.Range(0, images.Length)];
            option2.sprite = images[Random.Range(0, images.Length)];
            option3.sprite = images[Random.Range(0, images.Length)];

            yield return new WaitForSeconds(_waitToChangeImg);

            _elapsed += _waitToChangeImg;

            if(_elapsed > 2.6)
            {
                isSpinnig = false;
            }
        }

        if (_selected == 0) //SUCESS
        {
            option1.sprite = images[0];
            yield return new WaitForSeconds(0.25f);
            option2.sprite = images[0];
            yield return new WaitForSeconds(0.25f);
            option3.sprite = images[0];

            yield return new WaitForSeconds(1.5f);
            
            source.PlayOneShot(sucessClip);

            EscolherEfeitoPositivo();
        }
        else //FAIL
        {
            option1.sprite = images[1];
            yield return new WaitForSeconds(0.25f);
            option2.sprite = images[1];
            yield return new WaitForSeconds(0.25f);
            option3.sprite = images[1];

            yield return new WaitForSeconds(1.5f);
            
            source.PlayOneShot(failClip);

            EscolherEfeitoNegativo();

        }

        yield return new WaitForSeconds(5f);

        transform.DOScale(0, 1).OnComplete(() =>
        {
            result_TXT.gameObject.SetActive(false);
            result_TXT.text = "";
        });
    }
}
