using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NexusSDK.AttributionAPI;

public class SupportCreatorMenu : MonoBehaviour
{
    public Button EnterCodeButton;
    public GameObject InputField_Code;
    public GameObject txtCurrentCreatorCodeObject;
    public TextMeshProUGUI txtCurrentCreatorCode;
    public GameObject txtSetCreatorCodeObject;
    public TextMeshProUGUI txtSetCreatorCode;

    void OnEnable()
    {
        EnterCodeButton.onClick.AddListener(() => HandleButtonClick());

        if (PlayerPrefs.HasKey("NexusCreatorCode"))
        {
            txtSetCreatorCode.text = PlayerPrefs.GetString("NexusCreatorCode");
            txtCurrentCreatorCodeObject.SetActive(true);
            txtSetCreatorCodeObject.SetActive(true);
        }
        else
        {
            txtSetCreatorCode.text = "";
            txtCurrentCreatorCodeObject.SetActive(false);
            txtSetCreatorCodeObject.SetActive(false);

        }
    }
    void OnDisable()
    {
        EnterCodeButton.onClick.RemoveAllListeners();
    }


    void HandleButtonClick()
    {
        GetMemberByCodeOrUuidRequestParams requestParams = new()
        {
            memberCodeOrID = InputField_Code.GetComponent<TMP_InputField>().text
        };

        print(InputField_Code.GetComponent<TMP_InputField>().text);

        StartCoroutine(NexusSDK.AttributionAPI.StartGetMemberByCodeOrUuidRequest(requestParams,
            OnGetMemberByCodeOrUuid200ResponseFunction, ErrorCallbackFunction));
    }

    void OnGetMemberByCodeOrUuid200ResponseFunction(NexusSDK.AttributionAPI.Member Member)
    {
        string creatorCode = InputField_Code.GetComponent<TMP_InputField>().text;

        PlayerPrefs.SetString("NexusCreatorCode", creatorCode);
        PlayerPrefs.SetString("NexusCreatorCodeMemberId", Member.id);
        PlayerPrefs.Save();

        txtSetCreatorCode.text = creatorCode;

        txtCurrentCreatorCodeObject.SetActive(true);
        txtSetCreatorCodeObject.SetActive(true);
    }

    void ErrorCallbackFunction(long ErrorCode)
    {
        UnityEngine.Debug.Log(ErrorCode);
    }
}
