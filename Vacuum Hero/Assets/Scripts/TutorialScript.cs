using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

	private enum sides {
		left, right
	};
	private enum gender {
		male, female
	};
	private int TutorialPositionHandler = 0, igender = 0;
	private GameObject TutorialOldMan, PlayerMale, PlayerFemale, player;
	private RectTransform UpwardsPanel;
	private bool moving1 = false, moving2= false;
	public static List<GameObject> itemsOnScreen = new List<GameObject>();
	[SerializeField] private GameObject[] GenderUI;
	[SerializeField] private GameObject ItemPrefab;
	[SerializeField] private RuntimeAnimatorController femaleAnim;

	// Use this for initialization
	void Start () {
		TutorialOldMan = GameObject.Find ("TutorialOldMan");
		UpwardsPanel = GameObject.Find ("imgGenderSelect").GetComponent<RectTransform> ();
		player = GameObject.Find ("Player_01");
		StartCoroutine (startTutorial());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moving1) {
			if (player.transform.localPosition.x >= -4) {
				player.transform.localPosition = new Vector3 (player.transform.localPosition.x - 0.1f, player.transform.localPosition.y, player.transform.localPosition.z);
			} else {
				moving1 = false;
				moving2 = true;
				player.GetComponent<Animator> ().runtimeAnimatorController = femaleAnim;
			}
		}
		if(moving2) {
			if (player.transform.localPosition.x <= -1.5) {
				player.transform.localPosition = new Vector3 (player.transform.localPosition.x + 0.1f, player.transform.localPosition.y, player.transform.localPosition.z);
			} else {
				moving1 = false;
				moving2 = false;
			}
		}



		if (TutorialPositionHandler == 1) {
			if (TutorialOldMan.transform.position.x > 1.5f) {
				TutorialOldMan.transform.position = new Vector2 (TutorialOldMan.transform.position.x - 0.03f, TutorialOldMan.transform.position.y);
			} else {
				TutorialPositionHandler = 2;
			}
		}
		if (TutorialPositionHandler == 2) {
			SpeakerHandler.instance.addToQueue (new Dialogue (TutorialOldMan.GetComponent<SpriteRenderer> ().sprite, SpeakerHandler.instance.imgUIBackground.GetComponent<Image> ().sprite, "Hey you! Youngling!", (int)sides.right));
			SpeakerHandler.instance.addToQueue (new Dialogue (TutorialOldMan.GetComponent<SpriteRenderer> ().sprite, SpeakerHandler.instance.imgUIBackground.GetComponent<Image> ().sprite, "Welcome to NovTerra!", (int)sides.right));
			SpeakerHandler.instance.addToQueue (new Dialogue (TutorialOldMan.GetComponent<SpriteRenderer> ().sprite, SpeakerHandler.instance.imgUIBackground.GetComponent<Image> ().sprite, "In this world monsters and beasts live all around us. Scientist have built advanced vacuums that...", (int)sides.right));
			SpeakerHandler.instance.addToQueue (new Dialogue (TutorialOldMan.GetComponent<SpriteRenderer> ().sprite, SpeakerHandler.instance.imgUIBackground.GetComponent<Image> ().sprite, "... extract souls from creatures and mold the energy in the physical objects. Kinda gruesome eh?", (int)sides.right));
			SpeakerHandler.instance.addToQueue (new Dialogue (TutorialOldMan.GetComponent<SpriteRenderer> ().sprite, SpeakerHandler.instance.imgUIBackground.GetComponent<Image> ().sprite, "Let me ask you, are you male or female?", (int)sides.right));
			TutorialPositionHandler = 3;
		}
		if (TutorialPositionHandler == 3 && SpeakerHandler.instance.queue.Count <= 0 && SpeakerHandler.instance.playing == false) {
			foreach(GameObject g in GenderUI) {
				g.GetComponent<Image> ().enabled = true;
			}
			if (UpwardsPanel.localPosition.y <= 0) {
				UpwardsPanel.localPosition = new Vector3 (UpwardsPanel.localPosition.x, UpwardsPanel.localPosition.y + 20, UpwardsPanel.localPosition.z);
			}
		}
		if (TutorialPositionHandler == 4) {
			if (UpwardsPanel.localPosition.y >= -700) {
				UpwardsPanel.localPosition = new Vector3 (UpwardsPanel.localPosition.x, UpwardsPanel.localPosition.y - 20, UpwardsPanel.localPosition.z);
			} else {
				foreach(GameObject g in GenderUI) {
					g.GetComponent<Image> ().enabled = false;
				}
				TutorialPositionHandler = 5;
			}
		}
		if (TutorialPositionHandler == 5) {
			SpeakerHandler.instance.addToQueue (new Dialogue (TutorialOldMan.GetComponent<SpriteRenderer> ().sprite, SpeakerHandler.instance.imgUIBackground.GetComponent<Image> ().sprite, "Take my old gear, since I have grown old I am unable to fight anymore. It should fit just fine!", (int)sides.right));
			TutorialPositionHandler = 6;
		}
		if (TutorialPositionHandler == 6 && SpeakerHandler.instance.queue.Count <= 0 && SpeakerHandler.instance.playing == false) {
			TutorialPositionHandler = 7;
			GameObject noobHelmet = Instantiate (ItemPrefab, TutorialOldMan.transform.position, Quaternion.identity);
			GameObject noobVacuum = Instantiate (ItemPrefab, TutorialOldMan.transform.position, Quaternion.identity);

			noobVacuum.GetComponent<ItemSpawner> ().setItem (new Vacuum(0, 0, "Noob Vacuum", 1, false, "NoobVacuum", 1, 1, 1, 1, 1, 1, 1, 1, 1));
			noobHelmet.GetComponent<ItemSpawner> ().setItem (new Helmet (0, 0, "Noob Helmet", 1, false, "NoobHelmet", 1, 1));
			itemsOnScreen.Add (noobHelmet);
			itemsOnScreen.Add (noobVacuum);

		}
		if (TutorialPositionHandler == 7) {
			if(itemsOnScreen.Count == 0) {
				TutorialPositionHandler = 8;
			}
		}
		if (TutorialPositionHandler == 8) {
			StartCoroutine (finishTutorial());
			TutorialPositionHandler = 9;
		}
	}

	IEnumerator finishTutorial() {
		GameManager.tutorialON = false;
		yield return new WaitForSeconds(2f);
		LoadingScreenTransition.end = true;
		yield return new WaitForSeconds(2f);
		new WWW ("http://jonathanperron.ca/Vacuumhero/UpdateTutorial.php?tutorial=0&user=" + GameManager.lip.Username.ToLower() + "&gender=" + (gender)igender);
		Destroy (TutorialOldMan);
		yield return new WaitForSeconds(1f);
		LoadingScreenTransition.start = true;
	}

	IEnumerator startTutorial() {
		yield return new  WaitForSeconds(3f);
		TutorialPositionHandler = 1;
	}

	public void MaleOnTouch() {
		TutorialPositionHandler = 4;
		igender = (int)gender.male;
	}

	public void FemaleOnTouch() {
		TutorialPositionHandler = 4;
		moving1 = true;
		igender = (int)gender.female;
	}

	public void MaleOnHover(Sprite s) {
		GameObject.Find ("PlayerMale").GetComponent<Image>().sprite = s;
	}

	public void FemaleOnHover(Sprite s) {
		GameObject.Find ("PlayerFemale").GetComponent<Image>().sprite = s;
	}
}
