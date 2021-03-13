using UnityEngine;
using UnityEngine.Advertisements;

public class Ad_Manager : MonoBehaviour
{
    private string playStoreID = "4047499";
    private string appStoreID = "4047498";

    private string interstitialAd = "video";
    private string bannerAd = "banner";

    public bool isTargetPlayStore;
    public bool isTestAd;

    private void Start()
    {
        InitialiseAd();
    }

    private void InitialiseAd()
    {
        if(isTargetPlayStore) { Advertisement.Initialize(playStoreID, isTestAd); return; }
        Advertisement.Initialize(appStoreID, isTestAd);
    }

    public void PlayBannerAd()
    {
        if (Advertisement.IsReady(bannerAd))
            Advertisement.Show(bannerAd);
    }

    public void PlayInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialAd))
            Advertisement.Show(interstitialAd);
    }
}
