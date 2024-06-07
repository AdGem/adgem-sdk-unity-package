using System;
using UnityEngine;
using UnityEngine.UI;
using AdGemUnity.Runtime;
using TMPro;
using Random = UnityEngine.Random;

public class AdGemDemoController : MonoBehaviour
{
	[SerializeField] private AdGemDemoLogger logger;

	[SerializeField] private Button showOfferwallButton;
	[SerializeField] private Button logLastErrorButton;

	[SerializeField] private TMP_Text offerwallStateText;

	private void Start()
	{
		showOfferwallButton.onClick.AddListener(OnShowOfferwallClicked);
		logLastErrorButton.onClick.AddListener(OnLogErrorClicked);

		BindAdGemCallbacks();

		// You have to set the player metadata before any other SDK calls.
		SetPlayerMetadata();

		InvokeRepeating(nameof(CheckOfferwallState), 0, 1);
	}

	private void OnDestroy()
	{
		UnBindAdGemCallbacks();
		CancelInvoke(nameof(CheckOfferwallState));
	}

	private void BindAdGemCallbacks()
	{
		var callbackDelegate = AdGem.OfferwallCallback;
		callbackDelegate.OnLoadingStarted.AddListener(OnOfferwallLoadingStarted);
		callbackDelegate.OnLoadingFinished.AddListener(OnOfferwallLoadingFinished);
		callbackDelegate.OnLoadingFailed.AddListener(OnOfferwallLoadingFailed);
		callbackDelegate.OnRewardReceived.AddListener(OnOfferwallRewardReceived);
		callbackDelegate.OnClosed.AddListener(OnOfferwallClosed);
	}

	private void UnBindAdGemCallbacks()
	{
		var callbackDelegate = AdGem.OfferwallCallback;
		callbackDelegate.OnLoadingStarted.RemoveListener(OnOfferwallLoadingStarted);
		callbackDelegate.OnLoadingFinished.RemoveListener(OnOfferwallLoadingFinished);
		callbackDelegate.OnLoadingFailed.RemoveListener(OnOfferwallLoadingFailed);
		callbackDelegate.OnRewardReceived.RemoveListener(OnOfferwallRewardReceived);
		callbackDelegate.OnClosed.RemoveListener(OnOfferwallClosed);
	}

	private void SetPlayerMetadata()
	{
		var id = Guid.NewGuid().ToString();
		var metadata = new PlayerMetadata(id)
		{
			// All these fields are optional
			gender = PlayerMetadata.Gender.MALE,
			age = Random.Range(12, 87),
			placement = Random.Range(1, 1195),
			createdAt = DateTime.Now,
			isPayer = true,
			iapTotalUsd = Random.Range(1.99f, 1267)
		};

		// Custom fields are also optional. Only 5 values will be set.
		metadata.customFields.AddRange(new[]
		{
			"uno",
			"two",
			"tres",
			"quattro",
			"fifth",
			"won't be added"
		});
	}

	private void CheckOfferwallState()
	{
		showOfferwallButton.interactable = AdGem.IsOfferwallReady();
		offerwallStateText.text = $"Offerwall State: {AdGem.GetOfferwallState()}";
	}

	private void OnShowOfferwallClicked()
	{
		AdGem.ShowOfferwall();
	}

	private void OnLogErrorClicked()
	{
		var error = AdGem.GetError();
		if (string.IsNullOrEmpty(error))
			logger.LogMessage("No last error.");
		else
			logger.LogError(error);
	}

	private void OnOfferwallLoadingStarted()
	{
		logger.LogMessage("Offerwall Loading Started.");
	}

	private void OnOfferwallLoadingFinished()
	{
		logger.LogMessage("Offerwall Loading Finished.");
	}

	private void OnOfferwallLoadingFailed(string error)
	{
		logger.LogError("Offerwall Loading Error: " + error);
	}

	private void OnOfferwallRewardReceived(int amount)
	{
		logger.LogMessage("Offerwall Reward Received: " + amount);
	}

	private void OnOfferwallClosed()
	{
		logger.LogMessage("Offerwall Closed.");
	}
}