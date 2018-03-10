using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public string playerId;
    public string playerName;
    public string sex;
    public int level;
    public int totalXp;
    public int currentXp;
    public bool initialNameChange;
    public bool initialSexChange;

    public GameData()
    {
        this.playerId = Guid.NewGuid().ToString();
		playerName = "";
		sex = "female";
		level = 1;
		totalXp = 0;
		currentXp = 0;
        initialNameChange = true;
        initialSexChange = true;
    }
}