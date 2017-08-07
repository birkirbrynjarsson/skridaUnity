using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueControllerScript : MonoBehaviour {

	public static System.Random rand;
	public Transform clueParent;
	public Button addClueButton;
	private List<GameObject> receivedClues = new List<GameObject>();
	public RectTransform cluesContainer;

	public List<Message> messages = new List<Message>(){
		new Message(){
			title = "First Clue",
			content = "contentcontent",
		},
		new Message(){
			title = "Second Clue",
			content = "contentcontent",
		},
		new Message(){
			title = "Third Clue",
			content = "content333",
		}
	};

	// Use this for initialization
	void Start () {
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
		addClueButton.onClick.AddListener (spawnClue);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void spawnClue(){
		int clueNr = rand.Next (messages.Count);
		string title = messages[clueNr].title;
		string content = messages[clueNr].content;
		//clues.RemoveAt (clueNr);

		GameObject clueObject = (GameObject)Resources.Load("Clue");
		receivedClues.Add (Instantiate(clueObject));
		receivedClues[receivedClues.Count-1].transform.SetParent(clueParent, false);
		receivedClues[receivedClues.Count-1].GetComponent<messageScript>().init(title, content);
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
	public string content;
}