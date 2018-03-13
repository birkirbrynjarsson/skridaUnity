using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class InfoButton : MonoBehaviour
{
    public void SpawnInfoNotification()
    {
		UIManager.ShowNotification("GameInfoNotification", -1f, false, "Info", "Fjársjóðir Skriðuklausturs", null);
    }
}
