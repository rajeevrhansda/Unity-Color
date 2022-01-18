using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class Native : MonoBehaviour
{

    private UnifiedNativeAd adNative;

    [SerializeField] GameObject adNativePanel;
    [SerializeField] RawImage adIcon;
    [SerializeField] RawImage BodyImage;
    [SerializeField] RawImage adChoices;
    [SerializeField] Text adHeadline;
    [SerializeField] Text adCallToAction;
    [SerializeField] Text adAdvertiser;

    #region Singleton class: Native

    public static Native Instance;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    #region CoreFunctions
    void Start()
    {
        MobileAds.Initialize("ca-app-pub-8921506867398125~8110240027");
        Requestnative();
    }



    void Requestnative()
    {
        AdLoader adLoader = new AdLoader.Builder("ca-app-pub-8921506867398125/3702235156").ForUnifiedNativeAd().Build();
        adLoader.OnUnifiedNativeAdLoaded += this.HandleOnUnifiedNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        adLoader.LoadAd(AdRequestBuild());
    }

    private void HandleOnUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
    {
        this.adNative = args.nativeAd;
        DisplayAd(this.adNative);
        UIManager.Instance.ActivateNativePanel();
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Native ad failed to load: " + args.Message);
        adHeadline.text = "Native ad failed to load: " + args.Message;
    }

    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }

    void DisplayAd(UnifiedNativeAd ad)
    {
        Texture2D iconTexture = this.adNative.GetIconTexture();
        Texture2D iconAdChoices = this.adNative.GetAdChoicesLogoTexture();
        if (this.adNative.GetImageTextures().Count > 0)
        {
            List<Texture2D> goList = this.adNative.GetImageTextures();
            BodyImage.texture = goList[0];
            List<GameObject> list = new List<GameObject>();
            list.Add(BodyImage.gameObject);
            this.adNative.RegisterImageGameObjects(list);

        }
        string headline = this.adNative.GetHeadlineText();
        string cta = this.adNative.GetCallToActionText();
        string advertiser = this.adNative.GetAdvertiserText();
        adIcon.texture = iconTexture;
        adChoices.texture = iconAdChoices;
        adHeadline.text = headline;
        adAdvertiser.text = advertiser;
        adCallToAction.text = cta;

        //register gameobjects
        adNative.RegisterIconImageGameObject(adIcon.gameObject);
        adNative.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
        adNative.RegisterHeadlineTextGameObject(adHeadline.gameObject);
        adNative.RegisterCallToActionGameObject(adCallToAction.gameObject);
        adNative.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject);
    }

    #endregion
}