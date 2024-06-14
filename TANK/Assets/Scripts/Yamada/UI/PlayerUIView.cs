// ---------------------------------------------------------  
// PlayerUIView.cs  
//   
// 作成日:  6/10
// 作成者:  山田智哉
// ---------------------------------------------------------  
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("照準イメージ")]
    private Image _sightImage = default;

    [SerializeField, Tooltip("プレイヤーから照準までのポインターイメージ")]
    private Image _pointerImage = default;

    [SerializeField, Tooltip("ポインターの数")]
    private int _pointerCount = 5;

    // ポインターイメージの配列
    private List<Image> _pointerImages = new List<Image>();

    #endregion

    #region プロパティ  

    #endregion

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        if (_pointerImage != null)
        {
            // _pointerImagePrefabを_pointerCount分生成してリストに追加
            for (int i = 0; i < _pointerCount; i++)
            {
                Image pointerImage = Instantiate(_pointerImage, this.transform);
                _pointerImages.Add(pointerImage);
            }
        }

        if (_sightImage != null)
        {
            // sightImageを生成
            _sightImage = Instantiate(_sightImage, this.transform);
        }

        // カーソル非表示
        Cursor.visible = false;

        // カーソルを画面内で動かせる
        Cursor.lockState = CursorLockMode.Confined;
    }
  
    #region privateメソッド群  
  
    #endregion

    #region publicメソッド群

    /// <summary>
    /// プレイヤーの照準表示
    /// </summary>
    /// <param name="sightPosition">照準位置</param>
    /// <param name="playerPosition">プレイヤーの位置</param>
    public void PlayerPointer(Vector3 sightPosition, Vector3 playerPosition)
    {
        // _pointerImageを均等に配置する
        if (_pointerImages.Count > 0)
        {
            // 砲台のtransformをrecttransformに変換
            Vector3 batteryScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, playerPosition);

            // 砲台からマウスカーソルの方向を算出
            Vector3 direction = sightPosition - playerPosition;

            // 間隔を計算
            float distanceBetweenImages = direction.magnitude / (_pointerImages.Count + 1);

            // ポインターを均等に並べる
            for (int i = 0; i < _pointerImages.Count; i++)
            {
                Vector3 imagePosition = batteryScreenPosition + direction.normalized * distanceBetweenImages * (i + 1);
                _pointerImages[i].transform.position = imagePosition;
            }

        }
        // 照準をカーソル位置に表示
        _sightImage.transform.position = Input.mousePosition;
    }

    #endregion
}
