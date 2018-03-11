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
    public List<FoundMessage> foundMessages;
    // public List<Tuple<int, System.DateTime>> messages;
    public int totalCluesFound;
    public int totalTreasuresFound;

    public GameData()
    {
        this.playerId = Guid.NewGuid().ToString();
        playerName = "";
        sex = "";
        level = 1;
        totalXp = 0;
        currentXp = 0;
        initialNameChange = true;
        initialSexChange = true;
        foundMessages = new List<FoundMessage>();
    }
}

[System.Serializable]
public class FoundMessage
{
    public int messageId;
    public bool opened;
    public System.DateTime time;
    public FoundMessage(int nr, bool opened, System.DateTime time)
    {
        messageId = nr;
        this.opened = opened;
        this.time = time;
    }
}