using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public static LoggedInPlayer lip;
	public static Inventory inv = new Inventory();
	private GameObject txtWarning;
	public static bool tutorialON = true;
	[SerializeField] private RuntimeAnimatorController femaleAnim;

	void Awake() {
		Screen.SetResolution (582, 1000, false);
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		if(SceneManager.GetActiveScene ().name == "Game" && lip == null) {
			ChangeScene (0);
		}
		if(SceneManager.GetActiveScene ().name == "Game" && lip != null) {
			txtWarning = GameObject.Find ("txtWarning");
			if(lip.Firstlogin == 0) {
				tutorialON = false;
				Destroy (GameObject.Find("TutorialManager"));
			}
			if (lip.Gender == "female") {
				GameObject.Find("Player_01").GetComponent<Animator> ().runtimeAnimatorController = femaleAnim;
			}
		}
	}

	IEnumerator addinv(ItemStack item, GameObject worldItem) {
		WWW invadd = new WWW ("http://jonathanperron.ca/Vacuumhero/AddItemToInventory.php?userid=" + GameManager.lip.ID + "&name=" + item.Name + "&type=" + item.GetType().ToString() + "&stack=" + Convert.ToInt32 (item.Unique) + "&assetname=" + item.ItemAssetName);
		yield return invadd;
		string[] invaddResult = invadd.text.Split ('|');
		if(invaddResult[0] == "f") {
			int amountAddedToInv = item.Amount - Int32.Parse (invaddResult [1]);
			item.Amount = Int32.Parse(invaddResult [1]);
			txtWarning.GetComponent<Text> ().text = "BAG FULL!";
			Debug.Log ("Added " + amountAddedToInv + " - New amount " + item.Amount);
		}
		if(invaddResult[0] == "s") {
			worldItem.GetComponent<ItemSpawner> ().moveTowardsPlayer = true;
			if(tutorialON) {
				TutorialScript.itemsOnScreen.Remove (worldItem);
			}
			Destroy (worldItem.GetComponent<Rigidbody2D> ());
			Destroy (worldItem.GetComponent<BoxCollider2D> ());
		}
	}

	public void addtoinv(ItemStack item, GameObject worldItem) {
		StartCoroutine (addinv(item, worldItem));
	}

	public void ChangeScene(int scene) {
		StartCoroutine (Transition(scene));
	}

	IEnumerator Transition(int scene) {
		LoadingScreenTransition.end = true;
		yield return new  WaitForSeconds(3f);
		SceneManager.LoadScene (scene);
	}

	public void LogInPlayer(int id, int firstlogin, string username, string email, string lastupdate, string signup, string gender) {
		lip = new LoggedInPlayer (id, firstlogin, username, email, lastupdate, signup, gender);
	}
}