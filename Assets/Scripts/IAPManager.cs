using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class IAPManager : IStoreListener
{

    private IStoreController controller;
    private IExtensionProvider extensions;

    public IAPManager()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct("ten_thousand_dollars", ProductType.Consumable, new IDs
        {
            {"100_gold_coins_googleplay", GooglePlay.Name},
            {"100_gold_coins_appstore", AppleAppStore.Name}
        });
        builder.AddProduct("fifty_thousand_dollars", ProductType.Consumable, new IDs
        {
            {"100_gold_coins_googleplay", GooglePlay.Name},
            {"100_gold_coins_appstore", AppleAppStore.Name}
        });
        builder.AddProduct("one_hundred_thousand_dollars", ProductType.Consumable, new IDs
        {
            {"100_gold_coins_googleplay", GooglePlay.Name},
            {"100_gold_coins_appstore", AppleAppStore.Name}
        });
        builder.AddProduct("remove_ads", ProductType.NonConsumable, new IDs
        {
            {"100_gold_coins_googleplay", GooglePlay.Name},
            {"100_gold_coins_appstore", AppleAppStore.Name}
        });
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;


    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    { 
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        if (p == PurchaseFailureReason.PurchasingUnavailable)
        {
            // IAP may be disabled in device settings.
        }
    }

    public void OnPurchaseClicked(string productId)
    {
        controller.InitiatePurchase(productId);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }
}

