using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    public Button PurchaseButton;
    public Sprite PurchaseCompleteSprite;
    public TextMeshProUGUI txtPurchaseStatus;
    public GameObject txtPurchaseStatusObject;

    private readonly string apiURL = "https://surely-famous-husky.ngrok-free.app/api/ingame-purchase";

    [System.Serializable]
    public class MemberData
    {
        public string memberId;
        public string playerName;
        public string currency;
        public string description;
        public int subtotal;
    }

    void OnEnable()
    {
        PurchaseButton.onClick.AddListener(() => HandleButtonClick());
        txtPurchaseStatusObject.SetActive(false);
    }

    void OnDisable()
    {
        PurchaseButton.onClick.RemoveAllListeners();
    }

    void HandleButtonClick()
    {
        if (PlayerPrefs.HasKey("NexusCreatorCode"))
        {
            string memberId = PlayerPrefs.GetString("NexusCreatorCodeMemberId");
            string playerName = PlayerPrefs.GetString("PlayerName");

            // Hard coding store item attributes for demo purposes
            StartCoroutine(PostMemberId(memberId, playerName, "USD", "Carrott Cannon", 299));
        }
    }

    IEnumerator PostMemberId(string memberId, string playerName, string currency, string description, int subtotal)
    {
        MemberData dataObject = new() { 
            memberId = memberId, 
            playerName = playerName,
            currency = currency,
            description = description,
            subtotal = subtotal,
        };

        string jsonData = JsonUtility.ToJson(dataObject);

        using UnityWebRequest www = new(apiURL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("API Request Failed: " + www.error);
            txtPurchaseStatus.text = "Purchase Failed";
            txtPurchaseStatus.color = Color.red;
            txtPurchaseStatusObject.SetActive(true);
        }
        else
        {
            Debug.Log("API Request Success: " + www.downloadHandler.text);
            if (PlayerPrefs.HasKey("NexusCreatorCode"))
            {
                txtPurchaseStatus.text = "Purchase Successful! This purchase directly supported your favorite creator, " + PlayerPrefs.GetString("NexusCreatorCode") + "!";
            }
            else
            {
                txtPurchaseStatus.text = "Purchase Successful!";
            }
            txtPurchaseStatus.color = Color.green;
            txtPurchaseStatusObject.SetActive(true);
            PurchaseButton.GetComponent<Image>().sprite = PurchaseCompleteSprite;
        }
    }
}
