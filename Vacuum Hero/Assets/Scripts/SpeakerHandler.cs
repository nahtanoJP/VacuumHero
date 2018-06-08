using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeakerHandler : MonoBehaviour {
	[SerializeField] private Sprite portrait2, background2;

	public GameObject imgUIPortrait, imgUIBackground, txtUIText, txtUIContinue;
	private RectTransform dialogueHandler;
	public bool playing = false, waitingForInput = true, raiseDialogueUI = false;
	private float timeUntilNextLetter = 0.05f;
	private int currentLetterPosition = 0;
	private string speakerDialogue;
	public Queue<Dialogue> queue = new Queue<Dialogue>();

	public static SpeakerHandler instance = null;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		imgUIPortrait = GameObject.Find ("imgUISpeakingPortrait");
		imgUIBackground = GameObject.Find ("imgUISpeakingBackground");
		txtUIText = GameObject.Find ("txtUISpeakingText");
		txtUIContinue = GameObject.Find ("txtUIContinue");
		dialogueHandler = GameObject.Find ("DialogueHandler").GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene ().name == "MainMenu") {
			playing = false;
			Destroy (gameObject);
		}

		if (raiseDialogueUI) {
			if (dialogueHandler.localPosition.y < 0) {
				dialogueHandler.localPosition = new Vector3 (dialogueHandler.localPosition.x, dialogueHandler.localPosition.y + 30, dialogueHandler.localPosition.z);
			}
		}
		if(queue.Count <= 0 && playing == false) {
			if (dialogueHandler.localPosition.y > -600) {
				dialogueHandler.localPosition = new Vector3 (dialogueHandler.localPosition.x, dialogueHandler.localPosition.y - 20, dialogueHandler.localPosition.z);
			}
		}

		if (dialogueHandler.localPosition.y <= -600) {
			if (queue.Count <= 0) {
				imgUIPortrait.GetComponent<Image> ().enabled = false;
				imgUIBackground.GetComponent<Image> ().enabled = false;
			}
			txtUIText.GetComponent<Text> ().enabled = false;
			txtUIContinue.GetComponent<Text> ().enabled = false;
		}

		if(queue.Count > 0 && playing == false ) {
			initSpeaker (queue.Dequeue());
		}

		if (playing) {
			timeUntilNextLetter -= Time.deltaTime;
			if (currentLetterPosition <= speakerDialogue.Length) {
				if (timeUntilNextLetter <= 0) {
					txtUIText.GetComponent<Text> ().text = speakerDialogue.Substring (0, currentLetterPosition);
					currentLetterPosition++;
					timeUntilNextLetter = 0.04f;
				}
			} else {
				txtUIContinue.GetComponent<Text> ().enabled = true;
			}
		}

		if(Input.touchCount > 0) {
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
				hideDialogue ();
			}
		}
		if(Input.GetMouseButtonDown(0)) {
			if(speakerDialogue!=null) {
				if (currentLetterPosition < speakerDialogue.Length) {
					currentLetterPosition = speakerDialogue.Length;
				} else {
					hideDialogue ();
				}
			}
		}
	}
	public void addToQueue(Dialogue d) {
		queue.Enqueue (d);
	}

	public void initSpeaker(Dialogue d) {
		imgUIPortrait.GetComponent<Image> ().sprite = d.Portrait;
		imgUIBackground.GetComponent<Image> ().sprite = d.Background;
		this.speakerDialogue = d.Text;
		if(d.PortraitSide == 0) {
			imgUIPortrait.GetComponent<RectTransform> ().localPosition = new Vector2 (-446, -882);
			imgUIPortrait.GetComponent<RectTransform> ().localRotation.Set (0,0,0,0);
			txtUIContinue.GetComponent<RectTransform> ().localPosition = new Vector2 (288, -712);
			txtUIText.GetComponent<RectTransform> ().localPosition = new Vector2 (97, -852);
		}
		if(d.PortraitSide == 1) {
			imgUIPortrait.GetComponent<RectTransform> ().localPosition = new Vector2 (390, -882);
			imgUIPortrait.GetComponent<RectTransform> ().localRotation.Set (0,180,0,0);
			txtUIContinue.GetComponent<RectTransform> ().localPosition = new Vector2 (-276, -712);
			txtUIText.GetComponent<RectTransform> ().localPosition = new Vector2 (-120, -852);
		}

		raiseDialogueUI = true;

		imgUIPortrait.GetComponent<Image> ().enabled = true;
		imgUIBackground.GetComponent<Image> ().enabled = true;
		txtUIText.GetComponent<Text> ().enabled = true;
		play ();
	}

	void play() {
		playing = true;
	}

	void hideDialogue() {
		playing = false;
		raiseDialogueUI = false;
		currentLetterPosition = 0;
	}
}

