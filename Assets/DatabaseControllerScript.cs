using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DatabaseControllerScript : MonoBehaviour {

	private DatabaseReference reference;

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
				Debug.Log("Failed to get information from Firebase");
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				//snapshot.
				// Do something with snapshot...
				foreach(DataSnapshot child in snapshot.Children) {
					IDictionary dict = (IDictionary)child.Value;
					IList answers = (IList)dict["answers"];

					Debug.Log(dict["question"]);
					foreach (string answer in answers) {
						Debug.Log(answer);
					}
					Debug.Log("----------------------------------");
				}
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
