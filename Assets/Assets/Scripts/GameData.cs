using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string playerId { get; set; }
    public string playerName { get; set; }
    public string sex { get; set; }
    public int level { get; set; }
    public int totalXp { get; set; }
    public int currentXp { get; set; }
    public bool initialNameChange { get; set; }
    public bool initialSexChange { get; set; }

    public GameData()
    {
        newId();
		playerName = "";
		sex = "female";
		level = 1;
		totalXp = 0;
		currentXp = 0;
        initialNameChange = true;
        initialSexChange = true;
    }

    public GameData(string Id)
    {
        this.playerId = Id;
    }

    public GameData(string playerId, string playerName, string sex, int level, int totalXp, 
        int currentXp, bool initialNameChange, bool initialSexChange)
    {
		this.playerId = playerId;
		this.playerName = playerName;
		this.sex = sex;
		this.level = level;
		this.totalXp = totalXp;
		this.currentXp = currentXp;
        this.initialNameChange = initialNameChange;
        this.initialSexChange = initialSexChange;
    }

    public void initPlayer(){
        newId();
		this.playerName = "";
		this.sex = "female";
		this.level = 1;
		this.totalXp = 0;
		this.currentXp = 0;
    }

    public void newId()
    {
        this.playerId = Guid.NewGuid().ToString();
    }
}