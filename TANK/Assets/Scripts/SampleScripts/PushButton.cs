// ---------------------------------------------------------  
// PushButton.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;
using System.IO;

public class PushButton : MonoBehaviour
{

    #region 変数  
    private Canvas _registrationCanvas = default;

    private TMP_InputField _InputField = default;

    private TMP_Text _text = default;
    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  
    private void Start()
    {
        _registrationCanvas = GameObject.FindGameObjectWithTag("RegistCanvas").GetComponent<Canvas>();
        _InputField = GameObject.FindGameObjectWithTag("InputField").GetComponent<TMP_InputField>();
        _text = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
    }

    public void StartButton()
    {
        if (!string.IsNullOrEmpty(_InputField.text))
        {
            _text.text = "ちゃんと名前を入れてね";
        }
        else if(_InputField.text.Length > 10)
        {
            _text.text = "長い名前は覚えられないよ";
        }

        _registrationCanvas.gameObject.SetActive(false);
    }
    #endregion
}
