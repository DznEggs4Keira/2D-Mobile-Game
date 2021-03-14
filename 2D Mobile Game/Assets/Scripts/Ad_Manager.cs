using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ad_Manager : MonoBehaviour
{
    private readonly string playStoreID = "4047499";
    private readonly string appStoreID = "4047498";

    private readonly string interstitialAd = "video";
    private readonly string bannerAd = "banner";

    public bool isTargetPlayStore;
    public bool isTestAd;

    private void Start()
    {
        InitialiseAd();

        StartCoroutine(PlayBannerAd());
    }

    private void InitialiseAd()
    {
        if(isTargetPlayStore) { Advertisement.Initialize(playStoreID, isTestAd); return; }
        Advertisement.Initialize(appStoreID, isTestAd);
    }

    IEnumerator PlayBannerAd()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerAd);
    }

    public void PlayInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialAd))
            Advertisement.Show(interstitialAd);
    }
}
