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
	public float cluePadding;
	public PlayerControllerScript playerController;

	public List<Message> messages = new List<Message>(){
		// 0
		new Message(){
			messageId = 0,
			title = "Example Clue",
			location = "Exhibit",
			content = "Nothing here really, this is just an example clue.\n\nGood Luck!",
		},
		new Message(){
			messageId = 1,
			title = "Velkomin í fjársjóðsleik",
			location = "Leiðbeiningar",
			content = "\"Fjársjóðirnir á Skriðu\" er leikur þar sem markmiðið er að finna falda fjársjóði á Skriðuklaustri.\n\nFjársjóðirnir eru munir sem fundust við fornleifauppgröftinn á miðaldarklaustrinu hér á túninu fyrir neðan Gunnarsshús.\n\nEf þú heldur að þú ráðir við áskorunina og hyggst verða leiðtogi reglubræðra eða reglusystra í klaustrinu að Skriðu, þá þarftu að:\n\n- Finna allar vísbendingarnar\n- Finna alla fjársjóðina\n- Leysa allar þrautirnar\n\nGangi þér vel!",
		},
		new Message(){
			messageId = 2,
			title = "Framvinda leiksins",
			location = "Leiðbeiningar",
			content = "Upp í vinstra horninu er hægt að fylgjast með framvinduna þína í leiknum.\n\nEf smellt er á myndina af leikmanninum opnast gluggi með ítarlegri upplýsingum, þar er svo hægt að velja sér nafn og velja útlit á leikmannin.",
			spritePath = "messages/playerProgress"
		},
		new Message(){
			messageId = 3,
			title = "Fjársjóðskistan",
			location = "Leiðbeiningar",
			content = "Í fjársjóðskistunni finnur þú alla þá fjársjóði sem þú hefur þegar safnað.\n\nÁ bakvið hvern fjársjóð eru þrautir sem uppfæra fjársjóðinn og gefur stjörnur ef þú leysir þær.",
			spritePath = "messages/chestBtn"
		},
		new Message(){
			messageId = 4,
			title = "Stækkunarglerið",
			location = "Leiðbeiningar",
			content = "Stækkunarglerið kveikir á myndavélinni í símanum þínum og gerir þér kleift að finna vísbendingar og fjársjóði á safninu með svokallaðri viðaukaveruleika- tækni (AR).\n\nTæknin auðkennir myndir (image recognition) til þess að staðsetja fjársjóðina í raunveruleikanum.\n\nPrófaðu sjálf/ur á púslinu sem er fyrir miðju sýningarsalsins í Gunnarshúsi á Skriðuklaustri.",
			spritePath = "messages/searchBtn"
		},
		new Message(){
			messageId = 5,
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
		StartCoroutine(InitClues());
	}

	public IEnumerator InitClues(){
		foreach(var clue in receivedClues){
			Destroy(clue);
		}
		receivedClues = new List<GameObject>();
		zeroNotification();
		yield return new WaitForSeconds(0.2f);
		if(playerController.player.foundMessages.Count == 0){
			playerController.player.foundMessages.Add(new FoundMessage(5, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(4, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(3, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(2, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(1, false, System.DateTime.Now));
			playerController.player.totalCluesFound = playerController.player.foundMessages.Count;
		}
		loadFoundMessages();
		sortMessages();
	}

	void loadFoundMessages(){
		foreach(var foundMessage in playerController.player.foundMessages){
			GameObject clueObject = (GameObject)Resources.Load("Clue");
			GameObject clue = Instantiate (clueObject);
			receivedClues.Add(clue);
			clue.transform.SetParent(clueParent, false);

			Message message = messages.Find(x => x.messageId == foundMessage.messageId);
			Sprite image = null;
			if (message.spritePath != null) {
				image = Resources.Load<Sprite>(message.spritePath); 
			}
			clue.GetComponent<messageScript>().init(message, foundMessage, image, false);
			if(!foundMessage.opened){
				updateNotification(1);
			}
		}
		totalCluesFound = playerController.player.foundMessages.Count;
		totalCluesText.text = totalCluesFound.ToString () + " / " + messages.Count.ToString ();
	}
	
	void spawnRandomClue(){
		int clueNr = rand.Next (messages.Count);
		receiveMessage (clueNr);
	}

	public void receiveMessage(int messageId, bool showNotification = true){
		GameObject clueObject = (GameObject)Resources.Load("Clue");
		GameObject clue = Instantiate (clueObject);
		receivedClues.Add (clue);
		clue.transform.SetParent(clueParent, false);

		Message message = messages.Find(x => x.messageId == messageId);
		Sprite image = null;
		if (message.spritePath != null) {
			image = Resources.Load<Sprite>(message.spritePath); 
		}
		FoundMessage foundMessage = new FoundMessage(message.messageId, false, System.DateTime.Now); 
		playerController.player.foundMessages.Add(foundMessage);

		clue.GetComponent<messageScript>().init(message, foundMessage, image, showNotification);
		sortMessages ();

		if (showNotification) {
			playerScript.addXpValue (350);
		}
		if(!foundMessage.opened){
			updateNotification (1);
		}
		updateTotal ();
		playerController.savePlayer();
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

	public void zeroNotification(){
		unopenedClues = 0;
		notificationPin.SetActive(false);
		notificationText.text = unopenedClues.ToString();
	}

	public void updateTotal(){
		totalCluesFound++;
		totalCluesText.text = totalCluesFound.ToString () + " / " + messages.Count.ToString ();
	}

	void sortMessages(){
		float yPos = 0;
		for (var i = receivedClues.Count - 1; i >= 0; i--) {
			receivedClues[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, yPos, 0f);
			yPos += -cluePadding;
		}
		cluesContainer.sizeDelta = new Vector2 (0f, -yPos);
	}
}

[System.Serializable]
public class Message {
	public int messageId;
	public string title;
	public string location;
	public string content;
	public string spritePath;
}