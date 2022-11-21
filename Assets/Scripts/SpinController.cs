using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using Proyecto26;
using TMPro;
using Models;
using Newtonsoft.Json;

public class SpinController : MonoBehaviour
{


    public string getSpinUrl;
    public string sendGiftUrl;
    public string Authorization;

    [SerializeField] private PickerWheel pickerWheel;
    [SerializeField] private TextMeshProUGUI textPhone;
    [SerializeField] private Image Icon;
    [SerializeField] private Text Label;
    //[SerializeField] private Text Amount;
    [SerializeField] private TextMeshProUGUI Amount;

    private RequestHelper currentRequest;
    private int GiftId;
    private string Phone;

    private int spinCounter = 0;


    private void Awake()
    {

        pickerWheel.wheelPieces = new WheelPiece[8];

        WheelPiece newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/aqua1");
        newPiece.Label = "¿Í‚‡Cash 1000";
        newPiece.Amount = 1000;
        newPiece.Chance = 2f;
        newPiece.TokenId = 37;
        pickerWheel.wheelPieces[0] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/aquaday1");
        newPiece.Label = "¿Í‚‡ Day 1";
        newPiece.Amount = 1;
        newPiece.Chance = 23f;
        newPiece.TokenId = 61;
        pickerWheel.wheelPieces[1] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/aqua1");
        newPiece.Label = "¿Í‚‡Cash 750";
        newPiece.Amount = 750;
        newPiece.Chance = 5f;
        newPiece.TokenId = 37;
        pickerWheel.wheelPieces[2] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/barrel");
        newPiece.Label = "barrel";
        newPiece.Amount = 1;
        newPiece.Chance = 10f;
        newPiece.TokenId = 52;
        pickerWheel.wheelPieces[3] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/aqua1");
        newPiece.Label = "¿Í‚‡Cash 500";
        newPiece.Amount = 500;
        newPiece.Chance = 10f;
        newPiece.TokenId = 37;
        pickerWheel.wheelPieces[4] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/fitness");
        newPiece.Label = "fitness";
        newPiece.Amount = 1;
        newPiece.Chance = 15f;
        newPiece.TokenId = 60;
        pickerWheel.wheelPieces[5] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/aqua1");
        newPiece.Label = "¿Í‚‡Cash 300";
        newPiece.Amount = 300;
        newPiece.Chance = 5f;
        newPiece.TokenId = 37;
        pickerWheel.wheelPieces[6] = newPiece;

        newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("items/aquaday");
        newPiece.Label = "¿Í‚‡ day";
        newPiece.Amount = 1;
        newPiece.Chance = 30f;
        newPiece.TokenId = 59;
        pickerWheel.wheelPieces[7] = newPiece;

        ////newPiece.LabelColor = new Color(255f, 130f, 0f, 255f);
        //ColorUtility.TryParseHtmlString("#EF6B00", out newPiece.LabelColor);

    }

    private void ProcessResult(WheelPiece wheelPiece)
    {
        Debug.Log("winPhone:" + Phone + " winGiftId:" + GiftId + " wheelPiece.TokenId:" + wheelPiece.TokenId);
        if (spinCounter > 1)
        {
            Icon.sprite = wheelPiece.Icon;
            Label.text = wheelPiece.Label;
            Amount.text = wheelPiece.Amount.ToString();

            if (GiftId > 0 && string.IsNullOrEmpty(Phone) == false && wheelPiece.TokenId > 0)
            {

                Gift gift = new Gift();
                gift.gift_id = GiftId;
                gift.phone = Phone;
                gift.ids1 = new int[] { wheelPiece.TokenId };
                gift.amounts1 = new int[] { wheelPiece.Amount };
                gift.msg = "";

                Debug.Log("Error Response: " + JsonConvert.SerializeObject(gift, Formatting.Indented));

                currentRequest = new RequestHelper
                {
                    Uri = "http://192.168.90.221:8001/send_gift",
                    Headers = new Dictionary<string, string> {
                        { "Authorization", Authorization }
                    },

                    BodyString = JsonConvert.SerializeObject(gift, Formatting.Indented),
                    EnableDebug = true
                };
                RestClient.Post(currentRequest)
                .Then(response =>
                {
                    if (response.StatusCode == 200)
                    {
                        Debug.Log("Succesful: " + response.Text);
                    }
                    else
                    {
                        Debug.Log("Error: " + response.Text);
                    }
                }).Catch(err =>
                {
                    var error = err as RequestException;
                    Debug.Log("Error Response: " + error.Response);
                });
            }


        }

        Debug.Log(
               @" <b>Index:</b> " + wheelPiece.Index + "<b> Label:</b> " + wheelPiece.Label
               + "<b> Amount:</b> " + wheelPiece.Amount + "<b> Chance:</b> " + wheelPiece.Chance
               + "%" + "<b> winIndex:</b> " + GiftId + " <b> winPhone:</b> " + Phone + "<b> TokenId:</b> " + wheelPiece.Chance
            );
    }

    private void GetSpin()
    {
        currentRequest = new RequestHelper
        {
            Uri = "http://192.168.90.221:8001/get_spin",
            Headers = new Dictionary<string, string> {
                { "Authorization", Authorization}
            },

            //Params = new Dictionary<string, string> {
            //    { "cashdesk", "aqua" }
            //},
            Body = new GetSpin
            {
                cashdesk = "aqua"
            },
            EnableDebug = true
        };
        RestClient.Post<Spin>(currentRequest)
        .Then(response =>
        {
            if (string.IsNullOrEmpty(response.phone) == false && response.gift_id > 0)
            {
                Icon.sprite = Resources.Load<Sprite>("random");
                Label.text = "?";
                Amount.text = "";
                textPhone.text = response.phone;
                GiftId = response.gift_id;
                Phone = response.phone;
                Debug.Log("Succesful: phone" + response.phone + " gift_id-" + response.gift_id);
            }
            else
            {
                Icon.sprite = Resources.Load<Sprite>("random");
                Label.text = "?";
                Amount.text = "";
                textPhone.text = "¿ ¬¿œ¿– ";
                GiftId = 0;
                Phone = "";

            }
        }).Catch(err =>
        {
            var error = err as RequestException;
            Debug.Log("Error Response: " + error.Response);
            Icon.sprite = Resources.Load<Sprite>("random");
            Label.text = "?";
            Amount.text = "";
            textPhone.text = "¿ ¬¿œ¿– ";
            GiftId = 0;
            Phone = "";
        });
    }


    private IEnumerator Spin()
    {
        while (true)
        {

            if (spinCounter == 0)
            {
                Icon.sprite = Resources.Load<Sprite>("random");
                Label.text = "?";
                Amount.text = "";
                textPhone.text = "¿ ¬¿œ¿– ";
                GiftId = 0;
                Phone = "";
            }
            else
            {
                if (spinCounter == 1) pickerWheel.spinDuration = 10;
                if (spinCounter > 1) yield return new WaitForSeconds(10);
                GetSpin();
            }

            pickerWheel.OnSpinEnd(wheelPiece => ProcessResult(wheelPiece));
            spinCounter++;
            pickerWheel.Spin();
            yield return new WaitForSeconds(pickerWheel.spinDuration+1);
        }
    }

    void Start()
    {
        pickerWheel.spinDuration = 1;
        StartCoroutine(Spin());
    }

}

