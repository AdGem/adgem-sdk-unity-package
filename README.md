# AdGem Unity Package
The AdGem SDK for the Unity Package Manager.

## Requirements

- You must have an active [AdGem account](https://dashboard.adgem.com/register)
- You must add your App to your [account](https://dashboard.adgem.com/publisher/apps)
- The AdGem Unity SDK is compatible with apps built in  **Unity 2020 and higher**

## Integration
--------

### Install the AdGem Unity SDK

Install the AdGem Unity SDK package via the [Unity Package Manager using the following Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html):

```
https://github.com/AdGem/adgem-sdk-unity-package.git#1.0.0
```

In the Editor, you can access the Package Manager window through the **Window > Package Manager** menu.

### Setup the External Dependency Manager for Unity

In order to resolve Android/iOS AdGem Unity SDK dependencies the [External Dependency Manager for Unity](https://github.com/googlesamples/unity-jar-resolver) has to be installed in your project.

### Configure the AdGem Unity SDK

Set AdGem app ID from the AdGem publisher dashboard in ***Window > AdGem** settings menu.

## Usage
--------

All communication with the AdGem Unity SDK happens via the `AdGem` class.

_Please note there is no need to store instance of AdGem globally. The SDK will cache its instance on a first call and will always return the same one for all subsequent calls to AdGem_

### Set the Player Id _(the unique identifier for a user)_

You need to set the [player_id](../unity-optional-parameters/) (a unique id for your user) parameter for each of your individual users.

```csharp
var metadata = new PlayerMetadata("playerID-123")
{
    gender = PlayerMetadata.Gender.MALE,
    age = Random.Range(12, 87),
    placement = Random.Range(1, 1195),
    createdAt = DateTime.Now,
    isPayer = true,
    iapTotalUsd = Random.Range(1.99f, 1267)
};

AdGem.SetPlayerMetaData(metadata);
```

### Register the AdGem Offer Wall Callbacks

The AdGem Unity SDK provides callbacks that notify when offer wall internal state changes which may be registered through the instance of `AdGem.OfferwallCallback`.

```csharp
var callback = AdGem.OfferwallCallback;

callback.OnLoadingStarted.AddListener(() =>
{
    // Notifies that the offer wall loading has started
});
callback.OnLoadingFinished.AddListener(() =>
{
    // Notifies that the offer wall has been loaded.
});
callback.OnLoadingFailed.AddListener(error =>
{
    // Notifies that the offer wall has failed to load.
});
callback.OnRewardReceived.AddListener(amount =>
{
    // Notifies that the user has completed an action and should be rewarded with a specified virtual currency amount.
});
callback.OnClosed.AddListener(() =>
{
    // Notifies that the offer wall was closed.
});
```

Once registered, a callback will be used to deliver offer wall updates.

Keep in mind that it is the caller’s responsibility to unregister callabcks. For example, if callbacks were registered in MonoBehavior's `Start()` then they must be unregistered in corresponding `OnDestroy()` call.

```csharp
public class AdGemDemoController : MonoBehaviour
{
    private void Start()
	{
        ...
        var callbackDelegate = AdGem.OfferwallCallback;
		callbackDelegate.OnLoadingStarted.AddListener(OnOfferwallLoadingStarted);
		callbackDelegate.OnLoadingFinished.AddListener(OnOfferwallLoadingFinished);
		callbackDelegate.OnLoadingFailed.AddListener(OnOfferwallLoadingFailed);
		callbackDelegate.OnRewardReceived.AddListener(OnOfferwallRewardReceived);
		callbackDelegate.OnClosed.AddListener(OnOfferwallClosed);
        ...
	}

	private void OnDestroy()
	{
        ...
		var callbackDelegate = AdGem.OfferwallCallback;
		callbackDelegate.OnLoadingStarted.RemoveListener(OnOfferwallLoadingStarted);
		callbackDelegate.OnLoadingFinished.RemoveListener(OnOfferwallLoadingFinished);
		callbackDelegate.OnLoadingFailed.RemoveListener(OnOfferwallLoadingFailed);
		callbackDelegate.OnRewardReceived.RemoveListener(OnOfferwallRewardReceived);
		callbackDelegate.OnClosed.RemoveListener(OnOfferwallClosed);
        ...
	}
}
```

### Show Offer Wall

Use the `AdGem` class to show Offer Wall in your project:

```csharp
AdGem.ShowOfferwall()
```