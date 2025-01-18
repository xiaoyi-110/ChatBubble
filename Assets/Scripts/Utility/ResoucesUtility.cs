
public static class ResoucesUtility
{
    public static string GetUIFormAsset(string assetName)
    {
        return string.Format("Prefabs/UI/Form/{0}", assetName);
    }

    public static string GetDataTableAsset(string assetName)
    {
        return string.Format("Datas/DataTable/{0}", assetName);
    }

    public static string GetBulletAsset(string assetName)
    {
        return string.Format("Prefabs/Entity/Bullet/{0}");
    }

}