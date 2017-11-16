
public class FilePath
{
#if UNITY_ANDROID || UNITY_IPHONE
        private static readonly string basePath = UnityEngine.Application.dataPath;
#else
    private static readonly string basePath = UnityEngine.Application.dataPath;
#endif
    private const string _saveDirectory = "/Save";
    public static readonly string saveDirectory = basePath + _saveDirectory;
    public const string playerModel = "playerModel.data";

}
