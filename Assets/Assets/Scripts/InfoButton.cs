using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class InfoButton : MonoBehaviour
{
    private string info = "\"Fjársjóðir Skriðuklausturs\"\n er snjallsímaleikur til spilunar á Skriðuklaustri í Fljótsdal og byggir á gögnum og minjum fornleifarannsóknarinnar á Skriðuklaustri.\n\n" +
                            "Vinna við leikinn hófst sumarið 2017 sem nýsköpunarverkefni styrkt af Nýsköpunarsjóði námsmanna í umsjón Skúla Björns Gunnarssonar.\n\n" +
                            "Hönnun og smíði leiksins var í höndum styrkþegans Birkis Brynjarssonar, nemanda í tölvunarfræði við Háskólann í Reykjavík.\n\n" +
                            "Markmið verkefnisins var að þróa bæði fræðandi og spennandi leik fyrir börn og fullorðna út frá þeim munum og minjum sem fundust í fornleifarannsókninni á miðaldarklaustrinu á Skriðuklaustri á árunum 2000 - 2012.\n\n" +
                            "Leikurinn er sambland af fjársjóðsleit, spurninga- og hlutverkaleik spilaður í snjallsíma og nýtir sér AR-tækni með svipuðu móti og margir þekkja úr Pókemon GO.\n\n\n" +
                            "Aðild að verkefninu eiga:\nBirkir Brynjarsson\nSkúli Björn Gunnarsson\nHlynur Stefánsson\nLocatify\nGunnarsstofnun\nNýsköpunarsjóður námsmanna\nCINE";
    public void SpawnInfoNotification()
    {
		UIManager.ShowNotification("GameInfoNotification", -1f, false, "Um Leikinn", info, null);
    }
}
