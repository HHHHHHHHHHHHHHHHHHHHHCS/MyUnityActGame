namespace PlayerModelBase
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

    public enum ChangeBodyType
    {
        head,
        hand,
        foot,
        upperbody,
        lowerbody
    }

    public class PlayerModel
    {
        public PlayerColor playerColor = PlayerColor.White;
        public int headModel = 0;
        public int handModel = 0;
        public int footModel = 0;
        public int upperbodyModel = 0;
        public int lowerbodyModel = 0;

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


}
