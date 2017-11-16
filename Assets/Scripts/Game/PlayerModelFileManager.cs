using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelFileManager
{
    public enum PlayerColor
    {
        Blue,
        Cyan,
        Green,
        Purple,
        Red,
        White
    }

    public class PlayerModel
    {
        public PlayerColor playerColor;
        public int headModel;
        public int handModel;
        public int footModel;
        public int upperbodyModel;
        public int lowerbodyModel;

        public PlayerModel()
        {

        }

        public PlayerModel(PlayerColor _playerColor, int _headModel, int _handModel,
            int _footModel, int _upperbodyModel, int _lowerbodyModel)
        {
            playerColor = _playerColor;
            headModel = _headModel;
            handModel = _handModel;
            footModel = _footModel;
            upperbodyModel = _upperbodyModel;
            lowerbodyModel = _lowerbodyModel;
        }
    }

    public void SavePlayer(PlayerColor playerColor, int headModel, int handModel
        , int footModel, int upperbodyModel, int lowerbodyModel)
    {
        PlayerModel playerModel = new PlayerModel(playerColor, headModel, handModel
            , footModel, upperbodyModel, lowerbodyModel);
        SavePlayer(playerModel);
    }



    public void SavePlayer(PlayerModel playerModel)
    {

        string json = JsonManagerBase.ObjToJsonString(playerModel);
        FileManager.SaveFile(FilePath.saveDirectory, FilePath.playerModel, json);
    }

    public PlayerModel LoadPlayer()
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
