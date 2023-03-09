using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdmobo : MonoBehaviour
{
    private BannerView bannnerView;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

   private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitid = "ca-app-pub-4659297465066412/7587011638";
#elif UNITY_IPHONE
        string adUnitid = "unexpected_platform";
#else
        string adUnitid = "unexpected_platform";
#endif

        if (this.bannnerView != null)
        {
            this.bannnerView.Destroy();
        }

        AdSize adaptiveSize =
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannnerView = new BannerView(adUnitid, adaptiveSize, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        this.bannnerView.LoadAd(request);

    }

}
