using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Skrida.Database {
	public class DatabaseControllerScript : MonoBehaviour {

		private DatabaseReference reference;
		public static IList challenges;

		// Use this for initialization
		void Start () {
			// Set this before calling into the realtime database.
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://skriduklaustur-unity.firebaseio.com/");

			// Get the root reference location of the database.
			reference = FirebaseDatabase.DefaultInstance.RootReference;


			// TESTING FIREBASE STUFF
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
		}
		
		// Update is called once per frame
		void Update () {
			
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