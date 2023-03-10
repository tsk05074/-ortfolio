using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class MainTitleView : MonoBehaviour
{
    public GameObject Option;
    public GameObject title;
    public GameObject tutorial;
    public Image fadeImage;

    public Ease animEase;
    public Transform imageTrasform;
    public float animDuration;

    [SerializeField]
    private GameObject video1;
    [SerializeField]
    private GameObject video2;

    public Image currentImage;
    public Sprite[] nextImage;
    BannerAdmobo banner;
    int index = 0;

    private void Start()
    {
        StartCoroutine(GmaeVideo());
        banner = GameObject.Find("Admob").GetComponent<BannerAdmobo>();
        SoundeManager.Instance.PlayBGM("TitleBGM");
        imageTrasform
            .DOShakeScale(animDuration, 0.2f, 1, 0.1f, true)
            //.SetEase(animEase)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void ChangeImage()
    {
        if (index < 4)
        {
            Debug.Log(index);
            currentImage.sprite = nextImage[index];
            index++;
        }
        else
        {
            banner.Exis_ad();
            SceneManager.LoadScene("Main");
        }
    }

    public IEnumerator GmaeVideo()
    {
        yield return new WaitForSeconds(7.0f);
        video1.SetActive(false);

        video2.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        video2.SetActive(false);
    }
    public void StartGame()
    {
        banner.Exis_ad();

        fadeImage.DOFade(1, 0.5f).From(0)
            .OnStart(() => { fadeImage.gameObject.SetActive(true); })
            .OnComplete(() => { SceneManager.LoadScene("Main"); });
    }

    public void OptionOn()
    {
        SoundeManager.Instance.PlaySFX("ClickSfx");
        title.SetActive(false);
        Option.SetActive(true);
        tutorial.SetActive(false);
    }

    public void OptionOff()
    {
        SoundeManager.Instance.PlaySFX("ClickSfx");
        Option.SetActive(false);
        title.SetActive(true);
        tutorial.SetActive(true);
    }

    public void OnTutorialButton()
    {
        SoundeManager.Instance.PlaySFX("ClickSfx");
        tutorial.SetActive(true);

    }
}
