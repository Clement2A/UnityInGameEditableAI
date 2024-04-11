using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : Singleton<PlayerSystem>
{
    Player player;
    public Player Player => player;

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
}
