using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Skrida.Database {
	public class DatabaseControllerScript : MonoBehaviour {

		private DatabaseReference reference;
		private static int MAX_MESSAGES = 100;
		public static IList challenges;
		public static IList messages;
		public List<Message> localMessages;
		public string localMessagesVersion;
		public string remoteMessagesVersion;
		private static string messagesDestination = "/messages.dat";
		private static string messagesVersionDestination = "/messagesVersion.dat";

		void Awake(){
			// Set this before calling into the realtime database.
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://skriduklaustur-unity.firebaseio.com/");

			// Get the root reference location of the database.
			reference = FirebaseDatabase.DefaultInstance.RootReference;
			LoadLocalMessages();
			LoadMessagesVersion();
		}
		// Use this for initialization
		void Start () {
			// TESTING FIREBASE STUFF
			challenges = new List<UnityEngine.Object>();
			StartCoroutine(GetChallenges());
			StartCoroutine(CheckRemoteMessagesVersion());
		}

		public void LoadLocalMessages(){
			FileStream file;
			string messagesDest = Application.persistentDataPath + messagesDestination;
			BinaryFormatter bf = new BinaryFormatter();
			if(File.Exists(messagesDest)){
				file = File.OpenRead(messagesDest);
				this.localMessages = (List<Message>)bf.Deserialize(file);
				foreach(Message message in localMessages){
					Debug.Log(message.ToString());
				}
				file.Close();
			} else {
				this.localMessages = new List<Message>();
				for(int i = 0; i < MAX_MESSAGES; i++){
					this.localMessages.Add(new Message(i));
				}
				SaveMessages();
			}
		}

		private void LoadMessagesVersion(){
			FileStream file;
			string cvDest = Application.persistentDataPath + messagesVersionDestination;
			BinaryFormatter bf = new BinaryFormatter();
			if(File.Exists(cvDest)){
				file = File.OpenRead(cvDest);
				using (StreamReader reader = new StreamReader(file))
				{
					this.localMessagesVersion = reader.ReadToEnd();
				}
				file.Close();
			} else {
				this.localMessagesVersion = "0.0.0";
				Debug.Log("LoadMessagesVersion: " + this.localMessagesVersion);
				SaveMessagesVersion(this.localMessagesVersion);
			}
		}

		void SaveMessages(){
			FileStream file;
			string messagesDest = Application.persistentDataPath + messagesDestination;
			BinaryFormatter bf = new BinaryFormatter();
			if(File.Exists(messagesDest)){
				file = File.OpenWrite(messagesDest);
			} else {
				file = File.Create(messagesDest);
			}
			bf.Serialize(file, this.localMessages);
			file.Close();
		}

		void SaveMessagesVersion(string messagesVersion){
			FileStream file;
			string messVersDest = Application.persistentDataPath + messagesVersionDestination;
			BinaryFormatter bf = new BinaryFormatter();
			if(File.Exists(messVersDest)){
				file = File.OpenWrite(messVersDest);
			} else {
				file = File.Create(messVersDest);
			}
			file.Close();
			using (StreamWriter writer = new StreamWriter(messVersDest))
        	{
            	writer.WriteLine(messagesVersion);
        	}
			this.localMessagesVersion = messagesVersion;
			Debug.Log("SaveMessagesVersion: " + this.localMessagesVersion);
		}

		private IEnumerator CheckRemoteMessagesVersion(){
			Debug.Log("Checking Remote Messages Version, Local: " + this.localMessagesVersion);
			while(this.remoteMessagesVersion == "" || this.remoteMessagesVersion == null){
				FirebaseDatabase.DefaultInstance
				.GetReference("messagesVersion")
				.GetValueAsync().ContinueWith(task => {
					if(task.IsFaulted) {
						// Will have to try again
					} else if (task.IsCompleted){
						this.remoteMessagesVersion = (string)task.Result.Value;
						Debug.Log("Remote Messages Version found: " + this.remoteMessagesVersion);
					}
				});
				if(this.remoteMessagesVersion == "" || this.remoteMessagesVersion == null){
					yield return new WaitForSeconds(5f);
				} 
			}
			if(this.remoteMessagesVersion != this.localMessagesVersion){
				StartCoroutine(LoadRemoteMessages());
			}
		}

		private IEnumerator LoadRemoteMessages(){
			if(this.localMessagesVersion != this.remoteMessagesVersion){
				Debug.Log("Loading Remote Messages: " + this.localMessagesVersion + " : " + this.remoteMessagesVersion);
				// Update locally saved messages
				bool fetchedRemoteMessages = false;
				while(!fetchedRemoteMessages){
					FirebaseDatabase.DefaultInstance
					.GetReference("messages")
					.GetValueAsync().ContinueWith(task => {
						if(task.IsFaulted){
							// Couldn't retreive messages from firebase
						} else if(task.IsCompleted){
							Debug.Log("Task Completed Fetching Messages");
							// List of all messages
							// Each entry is an IDictionary with keys "messageId", "title", "content", "location" and "spritePath"
							// var messages = (IList)task.Result.Value;
							DataSnapshot snapshot = task.Result;
							this.localMessages = new List<Message>();
							for(int i = 0; i < snapshot.ChildrenCount; i++){
								Message m = new Message();
								Int32.TryParse(snapshot.Child(i.ToString()).Child("messageId").Value.ToString(), out m.messageId);
								m.title = snapshot.Child(i.ToString()).Child("title").Value.ToString();
								m.content = snapshot.Child(i.ToString()).Child("content").Value.ToString();
								m.location = snapshot.Child(i.ToString()).Child("location").Value.ToString();
								m.spritePath = snapshot.Child(i.ToString()).Child("spritePath").Value.ToString();
								this.localMessages.Add(m);
							}
							fetchedRemoteMessages = true;
							SaveMessages();
							SaveMessagesVersion(this.remoteMessagesVersion);
						}
					});
					if(!fetchedRemoteMessages){
						Debug.Log("Yielding WaitForSeconds() from LoadRemoteMessages");
						yield return new WaitForSeconds(15f);
					}
				}
			} else {
				Debug.Log("Remote and Local version are Identical!");
			}
		}
		
		public IEnumerator GetChallenges(){
			while(challenges.Count == 0){
				FirebaseDatabase.DefaultInstance
				.GetReference("challenges")
				.GetValueAsync().ContinueWith(task => {
					if (task.IsFaulted) {
						// Handle the error...
						Debug.Log("Failed to get challenges information from Firebase");
					}
					else if (task.IsCompleted) {
						// List of all challenges
						// Each entry is an IDictionary with keys "question" and "answers"
						challenges = (IList)task.Result.Value;				
					}
				});
				yield return new WaitForSeconds(5f);
			}
		}

		public void SavePlayer(GameData playerData){
			string json = JsonUtility.ToJson(playerData);
			reference.Child("players").Child(playerData.playerId).SetRawJsonValueAsync(json).ContinueWith(t => {
				if(t.IsCompleted) {
					reference.Child("players").Child(playerData.playerId).UpdateChildrenAsync(new Dictionary<string, object> {{"timeOfLastUpdate", ServerValue.Timestamp}});
				}
			});
		}
	}
}