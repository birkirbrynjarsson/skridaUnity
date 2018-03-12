using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameData
{
    private static int TOTAL_TREASURES = 9;
    public string playerId;
    public string playerName;
    public string sex;
    public int level;
    public int totalXp;
    public int currentXp;
    public bool initialNameChange;
    public bool initialSexChange;
    public List<FoundMessage> foundMessages;
    public List<FoundTreasure> foundTreasures;
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
        foundTreasures = new List<FoundTreasure>();
        for (int i = 0; i < TOTAL_TREASURES; i++)
        {
            foundTreasures.Add(new FoundTreasure(i, -1));
        }
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

[System.Serializable]
public class FoundTreasure
{
    public int treasureId;
    public int level;
    public System.DateTime time;
    public FoundTreasure(int id, int level)
    {
        treasureId = id;
        this.level = level;
        if (level < -1 || level > 3)
        {
            this.level = -1;
        }
        time = System.DateTime.Now;
    }
}