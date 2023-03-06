using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainTitleView : MonoBehaviour
{
    public GameObject Option;
    public Image fadeImage;

    private void Start()
    {
        SoundeManager.Instance.PlayBGM("TitleBGM");
    }

    public void StartGame()
    {
        fadeImage.DOFade(1, 0.5f).From(0)
            .OnStart(() => { fadeImage.gameObject.SetActive(true); })
            .OnComplete(() => { SceneManager.LoadScene("Main"); });
    }

    public void OptionOn()
    {
        SoundeManager.Instance.PlaySFX("ClickSfx");
        Option.SetActive(true);
    }

    public void OptionOff()
    {
        SoundeManager.Instance.PlaySFX("ClickSfx");
        Option.SetActive(false);
    }
}
