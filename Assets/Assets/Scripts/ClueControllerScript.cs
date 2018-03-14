using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Skrida.Database;
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
	public DatabaseControllerScript database;

	// Use this for initialization
	void Start () {
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
		// addClueButton.onClick.AddListener (spawnRandomClue);
		ScrollTrackableScript.onMessagePickup += receiveMessage;
		StartCoroutine(InitClues());
	}

	public IEnumerator InitClues(){
		foreach(var clue in receivedClues){
			Destroy(clue);
		}
		receivedClues = new List<GameObject>();
		zeroNotification();
		yield return new WaitForSeconds(0.5f);
		if(playerController.player.foundMessages.Count == 0){
			playerController.player.foundMessages.Add(new FoundMessage(5, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(4, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(3, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(2, false, System.DateTime.Now));
			playerController.player.foundMessages.Add(new FoundMessage(1, false, System.DateTime.Now));
			playerController.player.totalCluesFound = playerController.player.foundMessages.Count;
		}
		StartCoroutine(loadFoundMessages());
	}

	private IEnumerator loadFoundMessages(){
		while(database.localMessages.Find(x => x.title == "Example Clue") == null){
			yield return new WaitForSeconds(1f);
		}
		foreach(var clue in receivedClues){
			Destroy(clue);
		}
		foreach(var foundMessage in playerController.player.foundMessages){
			GameObject clueObject = (GameObject)Resources.Load("Clue");
			GameObject clue = Instantiate (clueObject);
			receivedClues.Add(clue);
			clue.transform.SetParent(clueParent, false);

			Message message = database.localMessages.Find(x => x.messageId == foundMessage.messageId);
			Sprite image = null;
			if (message.spritePath != "") {
				image = Resources.Load<Sprite>(message.spritePath); 
			}
			clue.GetComponent<messageScript>().init(message, foundMessage, image, false);
			if(!foundMessage.opened){
				updateNotification();
			}
		}
		totalCluesFound = playerController.player.foundMessages.Count;
		totalCluesText.text = totalCluesFound.ToString () + " / " + database.localMessages.Count.ToString ();
		sortMessages();
	}
	
	// void spawnRandomClue(){
	// 	int clueNr = rand.Next (messages.Count);
	// 	receiveMessage (clueNr);
	// }

	public void receiveMessage(int messageId, bool showNotification = true){
		GameObject clueObject = (GameObject)Resources.Load("Clue");
		GameObject clue = Instantiate (clueObject);
		receivedClues.Add (clue);
		clue.transform.SetParent(clueParent, false);

		// Message message = messages.Find(x => x.messageId == messageId);
		Message message = database.localMessages.Find(x => x.messageId == messageId);
		Sprite image = null;
		if (message.spritePath != "") {
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
			updateNotification ();
		}
		updateTotal ();
		playerController.savePlayer();
	}

	public void updateNotification (){
		unopenedClues = playerController.player.foundMessages.FindAll(x => x.opened == false).Count;
		if (unopenedClues > 0) {
			notificationPin.SetActive (true);
		} else if (unopenedClues <= 0) {
			notificationPin.SetActive (false);
		}
		notificationText.text = unopenedClues.ToString ();
	}

	public void zeroNotification(){
		unopenedClues = 0;
		notificationPin.SetActive(false);
		notificationText.text = unopenedClues.ToString();
	}

	public void updateTotal(){
		totalCluesFound = playerController.player.foundMessages.Count;
		totalCluesText.text = totalCluesFound.ToString () + " / " + database.localMessages.Count.ToString ();
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
