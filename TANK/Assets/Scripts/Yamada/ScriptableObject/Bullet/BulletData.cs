// ---------------------------------------------------------  
// BulletData.cs  
//   
// 作成日:  6/11
// 作成者:  山田智哉
// ---------------------------------------------------------  

[System.Serializable]
public struct BulletData
{

    #region 変数  

    // 弾速
    public float _speed;

    // 生存時間
    public float _lifeTime;

    // 反射回数
    public int _reflections;

    #endregion

    #region 構造体

    public BulletData(
        float speed,
        float lifeTime,
        int reflections
        )
    {
        this._speed = speed;
        this._lifeTime = lifeTime;
        this._reflections = reflections;
    }

    #endregion
}
