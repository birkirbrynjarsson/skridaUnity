using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ClueControllerScript : MonoBehaviour {

	public static System.Random rand;
	public Transform clueParent;
	public Button addClueButton;
	private List<GameObject> receivedClues = new List<GameObject>();
	public RectTransform cluesContainer;
	public PlayerControllerScript playerScript;

	public int unopenedClues;
	public int totalCluesFound;
	public Text totalCluesText;
	public GameObject notificationPin;
	public Text notificationText;

	public List<Message> messages = new List<Message>(){
		// 0
		new Message(){
			title = "Example Clue",
			location = "Exhibit",
			content = "Nothing here really, this is just an example clue.\n\nGood Luck!",
		},
		new Message(){
			title = "Velkomin í fjársjóðsleik",
			location = "Leiðbeiningar",
			content = "\"Fjársjóðirnir á Skriðu\" er leikur þar sem markmiðið er að finna falda fjársjóði á Skriðuklaustri.\n\nFjársjóðirnir eru munir sem fundust við fornleifauppgröftinn á miðaldarklaustrinu hér á túninu fyrir neðan Gunnarsshús.\n\nEf þú heldur að þú ráðir við áskorunina og hyggst verða leiðtogi reglubræðra eða reglusystra í klaustrinu að Skriðu, þá þarftu að:\n\n- Finna allar vísbendingarnar\n- Finna alla fjársjóðina\n- Leysa allar þrautirnar\n\nGangi þér vel!",
		},
		new Message(){
			title = "Framvinda leiksins",
			location = "Leiðbeiningar",
			content = "Upp í vinstra horninu er hægt að fylgjast með framvinduna þína í leiknum.\n\nEf smellt er á myndina af leikmanninum opnast gluggi með ítarlegri upplýsingum, þar er svo hægt að velja sér nafn og velja útlit á leikmannin.",
			spritePath = "messages/playerProgress"
		},
		new Message(){
			title = "Fjársjóðskistan",
			location = "Leiðbeiningar",
			content = "Í fjársjóðskistunni finnur þú alla þá fjársjóði sem þú hefur þegar safnað.\n\nÁ bakvið hvern fjársjóð eru þrautir sem uppfæra fjársjóðinn og gefur stjörnur ef þú leysir þær.",
			spritePath = "messages/chestBtn"
		},
		new Message(){
			title = "Stækkunarglerið",
			location = "Leiðbeiningar",
			content = "Stækkunarglerið kveikir á myndavélinni í símanum þínum og gerir þér kleift að finna vísbendingar og fjársjóði á safninu með svokallaðri viðaukaveruleika- tækni (AR).\n\nTæknin auðkennir myndir (image recognition) til þess að staðsetja fjársjóðina í raunveruleikanum.\n\nPrófaðu sjálf/ur á púslinu sem er fyrir miðju sýningarsalsins í Gunnarshúsi á Skriðuklaustri.",
			spritePath = "messages/searchBtn"
		},
		new Message(){
			title = "Innsiglaða perkamentið",
			location = "Leiðbeiningar",
			content = "Þetta er valmyndin sem þú ert í núna.\n\nHingað koma þær vísbendingar sem þú finnur við fjársjóðsleitina.",
			spritePath = "messages/mailBtn"
		}
	};

	// Use this for initialization
	void Start () {
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
		addClueButton.onClick.AddListener (spawnRandomClue);
		ScrollTrackableScript.onMessagePickup += receiveMessage;
		receiveMessage (5, false);
		receiveMessage (4, false);
		receiveMessage (3, false);
		receiveMessage (2, false);
		receiveMessage (1, false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void spawnRandomClue(){
		int clueNr = rand.Next (messages.Count);
		receiveMessage (clueNr);
	}

	public void receiveMessage(int msgNr, bool showNotification = true){
		GameObject clueObject = (GameObject)Resources.Load("Clue");
		GameObject clue = Instantiate (clueObject);
		receivedClues.Add (clue);
		clue.transform.SetParent(clueParent, false);

		string title = messages [msgNr].title;
		string location = messages [msgNr].location;
		string content = messages [msgNr].content;
		Sprite image = null;
		if (messages [msgNr].spritePath != null) {
			image = Resources.Load<Sprite>(messages [msgNr].spritePath); 
		}
		clue.GetComponent<messageScript>().init(title, location, content, image, showNotification);
		sortMessages ();

		if (showNotification) {
			playerScript.addXpValue (350);
		}
		updateNotification (1);
		updateTotal ();
	}

	public void updateNotification (int nr){
		unopenedClues += nr;
		if (unopenedClues > 0) {
			notificationPin.SetActive (true);
		} else if (unopenedClues <= 0) {
			notificationPin.SetActive (false);
			unopenedClues = 0;
		}
		notificationText.text = unopenedClues.ToString ();
	}

	public void updateTotal(){
		totalCluesFound++;
		totalCluesText.text = totalCluesFound.ToString () + " / " + messages.Count.ToString ();
	}

	void sortMessages(){
		float yPos = 0;
		for (var i = receivedClues.Count - 1; i >= 0; i--) {
			receivedClues[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, yPos, 0f);
			yPos += -130f;
		}
		cluesContainer.sizeDelta = new Vector2 (0f, -yPos);
	}
}

public class Message {
	public string title;
	public string location;
	public string content;
	public string spritePath;
}