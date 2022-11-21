using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;

public class Demo : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
   [SerializeField] private Text uiSpinButtonText ;

   [SerializeField] private PickerWheel pickerWheel ;


    private void Awake()
    {
        //UnityEngine.Sprite pieceIco = Resources.Load("cyberCash", typeof(Sprite)) as UnityEngine.Sprite;

        //Sprite pieceIco = Resources.Load<Sprite>("items/cuberCash");
        //Sprite sp = Resources.Load<Sprite>("Recouces/Barrel");

        WheelPiece newPiece = new WheelPiece();
        newPiece.Icon = Resources.Load<Sprite>("Barrel");
        newPiece.Label = "Cyber";
        newPiece.Amount = 150;
        newPiece.Chance = 15f;
        //        pickerWheel.wheelPieces[0] = newPiece;

        pickerWheel.wheelPieces = new WheelPiece[]
        {
            newPiece,
            newPiece,
            newPiece,
            newPiece,
            newPiece,
            newPiece,
        };

    }

    private void Start () {
 /*       
        UnityEngine.Sprite pieceIco = Resources.Load("cyberCash", typeof(Sprite)) as UnityEngine.Sprite;
        WheelPiece newPiece = new WheelPiece();
        //pieceIco.Icon = pieceIco ;
        newPiece.Label = "Cyber" ;
        newPiece.Amount = 150;
        newPiece.Chance = 15f;
//        pickerWheel.wheelPieces[0] = newPiece;

        pickerWheel.wheelPieces = new WheelPiece[]
        {
            newPiece,
            newPiece,
            newPiece,
            newPiece,
            newPiece,
            newPiece,
        };

*/
        uiSpinButton.onClick.AddListener(() =>
        {

            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Крутится...";

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log(
                       @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                       + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
                    );

                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "вращать";
            });

            pickerWheel.Spin();

        }) ;

   }

}
