// ---------------------------------------------------------  
// CharacterData.cs  
//   
// 作成日:  6/12
// 作成者:  山田智哉
// ---------------------------------------------------------  

[System.Serializable]
public struct CharacterData
{

    #region 変数  

    // 最大連射数
    public int _maxRapidFire;
    // 最大地雷設置数
    public int _maxMineSet;
    // 移動速度
    public float _moveSpeed;

    #endregion

    #region 構造体

    public CharacterData(
        int rapidFireCount,
        int maxMineSet,
        float moveSpeed
        )
    {
        this._maxRapidFire = rapidFireCount;
        this._maxMineSet = maxMineSet;
        this._moveSpeed = moveSpeed;
    }

    #endregion
}
