using PlayerModelBase;

public class PlayerModelFileManager
{
    public static void SavePlayer(PlayerColor playerColor, int headModel, int handModel
    , int footModel, int upperbodyModel, int lowerbodyModel)
    {
        PlayerModel playerModel = new PlayerModel(playerColor, headModel, handModel
            , footModel, upperbodyModel, lowerbodyModel);
        SavePlayer(playerModel);
    }

    public static void SavePlayer(PlayerModel playerModel)
    {
        string json = JsonManagerBase.ObjToJsonString(playerModel);
        FileManager.SaveFile(FilePath.saveDirectory, FilePath.playerModel, json);
    }

    public static PlayerModel LoadPlayer()
    {
        PlayerModel playerModel = null;
        string result = FileManager.LoadFile(FilePath.saveDirectory, FilePath.playerModel);
        if (result != null)
        {
            playerModel = JsonManagerBase.JsonStringToObj<PlayerModel>(result);
        }
        return playerModel;
    }
}
