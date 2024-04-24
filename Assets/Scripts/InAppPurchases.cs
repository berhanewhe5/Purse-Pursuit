using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
/*
public class InAppPurchases : MonoBehaviour, IStoreListener
{

  public GameObject LoadingOverlay;
  private Action OnPurchaseComplete;
  private IStoreController storeController;
  private IExtensionProvider extensionProvider;

  private async void Awake()
  {
      InitializationOptions options = new InitializationOptions()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
          .SetEnvironmentName("test");
#else
          .SetEnvironmentName("production");
#endif
      await UnityServices.InitializeAsync(options);
      ResourceRequest operation = Resources.LoadAsync<TextAsset>("IAPProductCatalog");
      operation.completed += HandleIAPCatalogLoaded;
  }

  public void HandleIAPCatalogLoaded(AsyncOperation operation)
  {
      ResourceRequest request = operation as ResourceRequest;

      Debug.Log($"Loaded Asset: {request.asset}");
      ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
      Debug.Log($"Loaded catalog with {catalog.allProducts.Count} items");

#if UNITY_ANDROID
      ConfigurationBuilder builder = ConfigurationBuilder.Instance(
          StandardPurchasingModule.Instance(AppStore.GooglePlay)
      );
#elif UNITY_IOS
      ConfigurationBuilder builder = ConfigurationBuilder.Instance(
          StandardPurchasingModule.Instance(AppStore.AppleAppStore)
      );
#else
      ConfigurationBuilder builder = ConfigurationBuilder.Instance(
          StandardPurchasingModule.Instance(AppStore.NotSpecified)
      );

#endif

      foreach (ProductCatalogItem item in catalog.allProducts)
      {
          builder.AddProduct(item.id, item.type);
      }

      UnityPurchasing.Initialize(this, builder);
  }
  public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
  {
      storeController = controller;
      extensionProvider = extensions;
  }

  public void Purchase(int value)
  {
      OnPurchase?.Invok
  }
  public void HandlePurchase(Product Product, Action OnPurchaseComplete)
  {
      LoadingOverlay.SetActive(true);
      storeController.InitiatePurchase(Product);
      this.OnPurchaseComplete = OnPurchaseComplete;
  }

  public void OnInitializeFailed(InitializationFailureReason error)
  {
      Debug.LogError($"Failed to initialize IAP because of {error}.");
  }

  public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
  {
      Debug.LogError($"Failed to purchase {i.definition.id} because of {p}.");
      OnPurchaseComplete?.Invoke();
      OnPurchaseComplete = null;
      LoadingOverlay.SetActive(false);
  }

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
  {
      Debug.Log($"Purchased {e.purchasedProduct.definition.id}");
      OnPurchaseComplete?.Invoke();
      OnPurchaseComplete = null;
      LoadingOverlay.SetActive(false);

      //give the player the item they purchased

      return PurchaseProcessingResult.Complete;
  }

}
*/