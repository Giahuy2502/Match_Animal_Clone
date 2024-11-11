using System;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    public string environment = "production";
    private IStoreController controller;
    private IExtensionProvider extensions;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        InitGamingServices();
        InitIAPSystem();
    }

    private async void InitGamingServices()
    {
        try
        {
            Debug.Log($"--- (IAP) Initializing Gaming Services...");

            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            Debug.Log($"--- (IAP) Initializing Gaming Services failed --- {exception.Message}");
            // An error occurred during services initialization.
        }
    }

    public void InitIAPSystem()
    {
        Debug.Log($"--- (IAP) Initializing IAP...");

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        List<string> products_ID = new List<string> { "option_1", "option_2", "option_3", "option_4", "option_5" };
        foreach (string product in products_ID)
        {
            builder.AddProduct(product, ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log($"--- (IAP) Initializing IAP success");

        this.controller = controller;
        this.extensions = extensions;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
     
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"--- (IAP) Initializing IAP failed --- {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"--- (IAP) Initializing IAP failed --- {error} --- {message}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
       
    }

}
