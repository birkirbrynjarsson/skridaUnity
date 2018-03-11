using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;
using Skrida.Database;

public class PlayerControllerScript : MonoBehaviour
{

    public GameData player;
    public DatabaseControllerScript database;

    public RectTransform XpProgressBar;
    private float progressWidth;
    private float progressHeight;
    public float progressMaxWidth;

    // Player Menu
    public Text playerNameText;
    public InputField playerNameInput;
    public Text playerTitle;
    public Text playerMenuLvlText;
    public Text playerMenuXpText;
    public Text playerMenuTotalXpText;
    public Sprite levelUpNotificationStar;
    public Sprite titleMedal;

    public Button maleButton;
    public Image maleImage;
    public Sprite maleBtnInactive;
    public Sprite maleBtnActive;
    public Button femaleButton;
    public Image femaleImage;
    public Sprite femaleBtnInactive;
    public Sprite femaleBtnActive;


    // Common info
    public int levelXp;
    public int newTitleLevels;

    // Player HUD
    public Button addXpButton;
    public GameObject lvlStar;
    public GameObject lvlFlash;
    public Text lvlText;
    public Text xpText;
    public GameObject XPFirework1;

    // Item Controller, Cheats
    public ItemControllerScript itemController;
    public ClueControllerScript clueController;

    private string[,] titles = {
        {"Vinnumaður", "Nemandi", "Munkur", "Prestvígður Munkur", "Príor", "Ábóti", "Skálholtsbiskup"},
        {"Vinnukona", "Nemandi", "Nunna", "Prestvígð Nunna", "Príorinna", "Abbadís", "Skálholtsbiskup"}
    };

    private string[,] titleText = {
        {"Árið er 1493 og þú ert ungur og guðhræddur vinnumaður úr Fljótsdal. Á leið þinni til Kaupstaðar einn blíðviðrisdag í Júní ríður þú heim að Skriðuklaustri þar sem þér er boðin vinna. Þú þiggur það með þökkum.",
        "Príorinn á Skriðuklaustri hefur fylgst með þér undanfarið og dáist að vinnusemi þinni, hann vill endilega bjóða þér að nema við klaustrið.\nÞú þiggur einstakt boð og sest á skólabekk.",
        "Þú hefur lært margt í námi þínu við klaustrið og eftir að hafa fylgst náið með starfseminni þar er vilji þinn að gerast þjónn Guðs.\nReglubræðurnir taka þér fagnandi, þú iðkar trúnna af reglusemi og hjálpar hina sjúku sem leita á náðir klaustursins af bestu getu.",
        "Þú hefur numið Guðs orð af mikilli samviskusemi og ert boðinn að taka aukna ábyrgð innan reglunnar sem prestur. Það gleður þig að boða hið heilaga orð í þessu nýja hlutverki.",
        "Skálholtsbiskup er uppnuminn yfir því góða starfi sem þú hefur unnið sem prestur og útnefnir þig príor yfir reglunni undir ábótavaldi sínu.",
        "Að launum fyrir velgengni þína í starfi príors og skuldbindingu þína við Guð ánafnar Skálholtsbiskup þér ábótavald sitt yfir Skriðuklaustri.",
        "Skálholtsbiskup sest í helgan stein en ánefnir þig sem eftirmann sinn. Þú ert valdamesti þjónn kirkjunnar á Íslandi."},
        {"Árið er 1493 og þú ert ung og guðhrædd vinnukona úr Fljótsdal. Á leið þinni til Kaupstaðar einn blíðviðrisdag í Júní ríður þú heim að Skriðuklaustri þar sem þér er boðin vinna. Þú þiggur það með þökkum.",
        "Príorinn á Skriðuklaustri hefur fylgst með þér undanfarið og dáist að vinnusemi þinni, hann vill endilega bjóða þér að nema við klaustrið.\nÞú þiggur einstakt boð og sest á skólabekk.",
        "Þú hefur lært margt í námi þínu við klaustrið og eftir að hafa fylgst náið með starfseminni þar er vilji þinn að gerast þjónn Guðs.\nReglubræðurnir taka þér fagnandi, þú iðkar trúnna af reglusemi og hjálpar hina sjúku sem leita á náðir klaustursins af bestu getu.",
        "Þú hefur numið Guðs orð af mikilli samviskusemi og ert boðin að taka aukna ábyrgð innan reglunnar sem prestur. Það gleður þig að boða hið heilaga orð í þessu nýja hlutverki.",
        "Skálholtsbiskup er uppnuminn yfir því góða starfi sem þú hefur unnið sem prestur og útnefnir þig príor yfir reglunni undir ábótavaldi sínu.",
        "Að launum fyrir velgengni þína í starfi príors og skuldbindingu þína við Guð ánafnar Skálholtsbiskup þér ábótavald sitt yfir Skriðuklaustri.",
        "Skálholtsbiskup sest í helgan stein en ánefnir þig sem eftirmann sinn. Þú ert valdamesti þjónn kirkjunnar á Íslandi."}
    };

    // Use this for initialization
    void Start()
    {
		LoadPlayer();
        Initialize();
        progressHeight = XpProgressBar.rect.height;
        //rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
        //addXpButton.onClick.AddListener(addXp);
        playerNameInput.onValueChanged.AddListener(updatePlayerName);
        maleButton.onClick.AddListener(maleClicked);
        femaleButton.onClick.AddListener(femaleClicked);


        // Event listeners to gain XP
    }

    private void Initialize()
    {
        lvlText.text = player.level.ToString();
        //addXpValue(0);
        playerNameInput.text = player.playerName;
        playerNameText.text = player.playerName;
        xpText.text = player.currentXp.ToString() + " / " + levelXp.ToString() + " XP";
        playerMenuLvlText.text = player.level.ToString();
        playerMenuXpText.text = player.currentXp.ToString() + " / " + levelXp.ToString() + " XP";
        playerMenuTotalXpText.text = player.totalXp.ToString() + " XP";

        int sexIndex = (player.sex == "female") ? 1 : 0;
        playerTitle.text = titles[sexIndex, player.level / newTitleLevels];
        if(player.sex == "female") {
            femaleImage.sprite = femaleBtnActive;
        } else if(player.sex == "male") {
            maleImage.sprite = maleBtnActive;
        }
    }

    public void LoadPlayer()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
			BinaryFormatter bf = new BinaryFormatter();
			this.player = (GameData)bf.Deserialize(file);
			if(this.player.playerId == null){
				this.player = new GameData();
			}
			file.Close();
        }
        else
        {
            this.player = new GameData();
        }
    }

    public void savePlayer()
    {
        string destination = Application.persistentDataPath + "/save.dat"; 
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, this.player);
        file.Close();
        database.SavePlayer(this.player);
    }

    public void addXpValue(int newXp)
    {
        if (player.currentXp == 0)
        {
            XpProgressBar.sizeDelta = new Vector2(0f, progressHeight);
            XpProgressBar.GetComponent<Image>().color = new Color32(185, 221, 51, 255);
        }
        if (player.currentXp + newXp >= levelXp)
        {

            player.totalXp += levelXp - player.currentXp;
            player.currentXp += newXp;
            newXp = player.currentXp - levelXp;
            player.currentXp = 0;

            XpProgressBar.GetComponent<Image>().color = new Color32(255, 223, 0, 255);
            xpText.text = player.currentXp.ToString() + " / " + levelXp.ToString() + " XP";

            Vector2 targetPosition = new Vector2(progressMaxWidth, progressHeight);
            iTween.ValueTo(XpProgressBar.gameObject, iTween.Hash(
                "from", XpProgressBar.sizeDelta,
                "to", targetPosition,
                "time", 1f,
                "onupdatetarget", this.gameObject,
                "onupdate", "moveGuiElement",
                "oncomplete", "addXpValue",
                "oncompletetarget", this.gameObject,
                "oncompleteparams", newXp
            ));

            goUpLevel(newXp);

        }
        else
        {

            player.totalXp += newXp;
            player.currentXp += newXp;

            xpText.text = player.currentXp.ToString() + " / " + levelXp.ToString() + " XP";
            XPFirework1.SetActive(false);
            XPFirework1.SetActive(true);

            progressWidth = (float)player.currentXp / levelXp * progressMaxWidth;
            Vector2 targetPosition = new Vector2(progressWidth, progressHeight);
            iTween.ValueTo(XpProgressBar.gameObject, iTween.Hash(
                "from", XpProgressBar.sizeDelta,
                "to", targetPosition,
                "time", 1f,
                "onupdatetarget", this.gameObject,
                "onupdate", "moveGuiElement"
            ));
        }
        playerMenuXpText.text = player.currentXp.ToString() + " / " + levelXp.ToString() + " XP";
        playerMenuTotalXpText.text = player.totalXp.ToString() + " XP";

        savePlayer();
    }

    void updateProgressBar()
    {
        progressWidth = (float)player.currentXp / levelXp * progressMaxWidth;
        Vector2 targetPosition = new Vector2(progressWidth, progressHeight);
        iTween.ValueTo(XpProgressBar.gameObject, iTween.Hash(
            "from", XpProgressBar.sizeDelta,
            "to", targetPosition,
            "time", 0.4f,
            "onupdatetarget", this.gameObject,
            "onupdate", "moveGuiElement"
        ));

        //XpProgressBar.sizeDelta = new Vector2 (progressWidth, progressHeight);
    }

    public void moveGuiElement(Vector2 position)
    {
        XpProgressBar.sizeDelta = position;
    }

    void goUpLevel(int addXp)
    {
        lvlStar.transform.localScale = new Vector3(2.4f, 2.4f, 1f);
        Vector3 scaleTo = new Vector3(5f, 5f, 1f);
        iTween.PunchScale(lvlStar, iTween.Hash(
            "amount", scaleTo,
            "time", 2f,
            "oncomplete", "resetStarScale",
            "oncompletetarget", this.gameObject
        ));
        player.level++;
        lvlText.text = player.level.ToString();
        playerMenuLvlText.text = player.level.ToString();
        lvlFlash.SetActive(false);
        lvlFlash.SetActive(true);
        UIManager.ShowNotification("LevelNotification", 10f, true, player.level.ToString(), "Til lukku!\nÞú hefur farið upp um stig.\nHeildar stigafjöldi: " + (player.totalXp + addXp).ToString() + " XP", levelUpNotificationStar);
        if (player.level % newTitleLevels == 0)
        {
            updateTitle();
        }
        
        savePlayer();
    }

    void resetStarScale()
    {
        Vector3 rotateTo = new Vector3(0f, 0f, 720f);
        iTween.PunchRotation(lvlStar, rotateTo, 1f);
        Vector3 scaleTo = new Vector3(1f, 1f, 1f);
        iTween.ScaleTo(lvlStar, scaleTo, 1f);
    }

    void updatePlayerName(string value)
    {
        player.playerName = value;
        playerNameText.text = player.playerName;
        if (player.initialNameChange)
        {
            player.initialNameChange = false;
            addXpValue(800);
        }
        if (player.playerName.ToLower() == "gulurbirkir")
        {
            itemController.findAllItems();
        }
        
        savePlayer();
    }

    void maleClicked()
    {
        player.sex = "male";
        maleImage.sprite = maleBtnActive;
        femaleImage.sprite = femaleBtnInactive;
        playerTitle.text = titles[0, player.level / newTitleLevels];
        if (player.initialSexChange)
        {
            player.initialSexChange = false;
            addXpValue(300);
            
        }
        updateTitle();
        savePlayer();
    }

    void femaleClicked()
    {
        player.sex = "female";
        femaleImage.sprite = femaleBtnActive;
        maleImage.sprite = maleBtnInactive;
        playerTitle.text = titles[1, player.level / newTitleLevels];
        if (player.initialSexChange)
        {
            player.initialSexChange = false;
            addXpValue(300);
        }
        updateTitle();
        savePlayer();
    }

    void updateTitle()
    {
        int sexIndex = (player.sex == "female") ? 1 : 0;
        if (player.level / newTitleLevels < titles.GetLength(1))
        {
            playerTitle.text = titles[sexIndex, player.level / newTitleLevels];
            UIManager.ShowNotification("TitleNotification", 0f, true, titles[sexIndex, player.level / newTitleLevels], titleText[sexIndex, player.level / newTitleLevels], titleMedal);
        }
    }
}
