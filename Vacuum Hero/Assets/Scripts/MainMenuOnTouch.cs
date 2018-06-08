using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;

public class MainMenuOnTouch : MonoBehaviour {

	bool start = false, play = false;
	public static bool end = false;
	RectTransform mainmenuButtons, mainmenuSignInUp;
	Image mainmenuButton;
	[SerializeField] private Sprite spLoading, spPlay;
	private bool needHash = false;
	private GameObject username, password, passConfirm, email, signupComplete, cancel, signin, signup, txtUIErrorMessage, playbtn;
	private TouchScreenKeyboard kb;
	private string[] loginDetailsSplit;
	private AsyncOperation ao;

	// Use this for initialization
	void Start () {
		mainmenuButton = gameObject.GetComponent<Image> ();
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			mainmenuButtons = GameObject.Find ("goUIMainMenuButtons").GetComponent<RectTransform> ();
			mainmenuSignInUp = GameObject.Find ("goUISignInUp").GetComponent<RectTransform>();
		}

		playbtn = GameObject.Find ("imgUIPlay");
		username = GameObject.Find ("ifUIUsername");
		password = GameObject.Find ("ifUIPassword");
		passConfirm = GameObject.Find ("ifUIConfirmPassword");
		email = GameObject.Find ("ifUIEmail");
		signupComplete = GameObject.Find ("imgUISignUpComplete");
		cancel = GameObject.Find ("imgUICancel");
		signin = GameObject.Find ("imgUISignIn");
		signup = GameObject.Find ("imgUISignUp");
		txtUIErrorMessage = GameObject.Find ("txtUIErrorMessage");
	}

	void FixedUpdate () {
		if(start) {
			if (mainmenuButtons.localPosition.x > -1200) {
				mainmenuButtons.localPosition = new Vector3 (mainmenuButtons.localPosition.x - 40, mainmenuButtons.localPosition.y, mainmenuButtons.localPosition.z);
				mainmenuSignInUp.localPosition = new Vector3 (mainmenuSignInUp.localPosition.x - 40, mainmenuSignInUp.localPosition.y, mainmenuSignInUp.localPosition.z);
			} else {
				start = false;
			}
		}
		if(end) {
			if (mainmenuButtons.localPosition.x < 0) {
				mainmenuButtons.localPosition = new Vector3 (mainmenuButtons.localPosition.x + 5, mainmenuButtons.localPosition.y, mainmenuButtons.localPosition.z);
				mainmenuSignInUp.localPosition = new Vector3 (mainmenuSignInUp.localPosition.x + 5, mainmenuSignInUp.localPosition.y, mainmenuSignInUp.localPosition.z);
			} else {
				end = false;
			}
		}

		if(play) {
			if(ao.progress >= 0.9f) {
				ao.allowSceneActivation = true;
			}
		}
	}

	public void OnTouch() {
		mainmenuButton.color = Color.green;
	}

	public void OnRelease() {
		mainmenuButton.color = Color.white;
	}

	public void PlayClicked() {
		if (PlayerPrefs.HasKey ("username") && PlayerPrefs.HasKey ("p")) {
			playbtn.GetComponent<Image> ().sprite = spLoading;
			StartCoroutine (LoginIE (PlayerPrefs.GetString ("username"), PlayerPrefs.GetString ("p")));
		} else {
			moveMainMenuOff ();
		}
	}

	public void SignInClicked() {
		if (username.GetComponent<InputField> ().text.Length > 0 && password.GetComponent<InputField> ().text.Length > 0) {
			needHash = true;
			StartCoroutine (LoginIE (username.GetComponent<InputField> ().text, password.GetComponent<InputField> ().text));
		} else {
			txtUIErrorMessage.GetComponent<Text> ().text = "Please make sure all fields are filled.";
		}
	}

	public void SignUpClicked() {
		passConfirm.GetComponent<Image> ().enabled = true;
		passConfirm.GetComponent<InputField> ().enabled = true;
		passConfirm.GetComponent<InputField> ().placeholder.GetComponent<Text>().text = "Confirm Password";
		email.GetComponent<Image> ().enabled = true;
		email.GetComponent<InputField> ().enabled = true;
		email.GetComponent<InputField> ().placeholder.GetComponent<Text>().text = "Email";
		cancel.GetComponent<Image> ().enabled = true;
		signupComplete.GetComponent<Image> ().enabled = true;
		signin.GetComponent<Image> ().enabled = false;
		signup.GetComponent<Image> ().enabled = false;
		txtUIErrorMessage.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -100, 0);
		txtUIErrorMessage.GetComponent<RectTransform> ().sizeDelta = new Vector2(2000, 125);
		txtUIErrorMessage.GetComponent<Text> ().text = "";
	}

	public void SignUpCompleteClicked() {
		if (username.GetComponent<InputField> ().text.Length > 0 && password.GetComponent<InputField> ().text.Length > 0 && passConfirm.GetComponent<InputField> ().text.Length > 0 && email.GetComponent<InputField> ().text.Length > 0) {
			if (password.GetComponent<InputField> ().text.Equals (passConfirm.GetComponent<InputField> ().text)) {
				if(email.GetComponent<InputField> ().text.Contains("@") && email.GetComponent<InputField> ().text.Contains(".") && email.GetComponent<InputField> ().text.Length > 3) {
					StartCoroutine (SignUpIE (username.GetComponent<InputField> ().text, password.GetComponent<InputField> ().text, passConfirm.GetComponent<InputField> ().text, email.GetComponent<InputField> ().text));
				} else {
					txtUIErrorMessage.GetComponent<Text> ().text = "Please correct the email address.";
				}
			} else {
				txtUIErrorMessage.GetComponent<Text> ().text = "Passwords do not match.";
			}
		} else {
			txtUIErrorMessage.GetComponent<Text> ().text = "Please make sure all fields are filled.";
		}
	}

	public void CancelClicked() {
		passConfirm.GetComponent<Image> ().enabled = false;
		passConfirm.GetComponent<InputField> ().enabled = false;
		passConfirm.GetComponent<InputField> ().placeholder.GetComponent<Text>().text = "";
		passConfirm.GetComponent<InputField> ().text = "";
		email.GetComponent<Image> ().enabled = false;
		email.GetComponent<InputField> ().enabled = false;
		email.GetComponent<InputField> ().placeholder.GetComponent<Text>().text = "";
		email.GetComponent<InputField> ().text = "";
		cancel.GetComponent<Image> ().enabled = false;
		signupComplete.GetComponent<Image> ().enabled = false;
		signin.GetComponent<Image> ().enabled = true;
		signup.GetComponent<Image> ().enabled = true;
		txtUIErrorMessage.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -600, 0);
		txtUIErrorMessage.GetComponent<RectTransform> ().sizeDelta = new Vector2(2000, 250);
		txtUIErrorMessage.GetComponent<Text> ().text = "";
	}

	public void BackClicked() {
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			end = true;
			CancelClicked ();
		}
		if (SceneManager.GetActiveScene ().name == "About") {
			ChangeScene (0);
		}
	}

	public void AboutClicked() {
		ChangeScene (2);
	}

	void moveMainMenuOff() {
		start = true;
	}

	public void ChangeScene(int scene) {
		StartCoroutine (Transition(scene));
	}

	IEnumerator Transition(int scene) {
		LoadingScreenTransition.end = true;
		yield return new WaitForSeconds(1f);
		ao = SceneManager.LoadSceneAsync (scene);
		ao.allowSceneActivation = false;
		play = true;
	}

	IEnumerator LoginIE (string struser, string strpass) {
		if(needHash) {
			strpass = CreateHash (struser.ToLower ()+strpass);
			needHash = false;
		}
		WWW login = new WWW ("http://jonathanperron.ca/Vacuumhero/Login.php?username=" + struser +"&pass=" + strpass);
		yield return login;
		string loginDetails = login.text;
		loginDetailsSplit = loginDetails.Split ('|');

		//01 incorrect login credentials
		//02 empty values given
		if(loginDetailsSplit[0].Equals("Error")) {
			if(loginDetailsSplit[1].Equals("01")) {
				Debug.Log ("Incorrect login");
				txtUIErrorMessage.GetComponent<Text> ().text = "The username/password combination was incorrect.";
			}
			if(loginDetailsSplit[1].Equals("02")) {
				Debug.Log ("Empty values given");
			}
			playbtn.GetComponent<Image> ().sprite = spPlay;
		} else if(loginDetailsSplit[0].Equals("")) {
			playbtn.GetComponent<Image> ().sprite = spPlay;
			Debug.Log ("Failed to retrieve results.");
		} else {
			GameManager.instance.LogInPlayer (int.Parse(loginDetailsSplit[0]), int.Parse(loginDetailsSplit[1]), struser.ToLower(), loginDetailsSplit[2], loginDetailsSplit[3], loginDetailsSplit[4], loginDetailsSplit[5]);
			PlayerPrefs.SetString ("username", struser.ToLower ());
			PlayerPrefs.SetString ("p", strpass);
			ChangeScene(1);
		}
	}

	IEnumerator SignUpIE (string struser, string strpass, string strconfirmpass, string stremail) {
		strpass = CreateHash (struser.ToLower()+strpass);
		Debug.Log ("http://jonathanperron.ca/Vacuumhero/SignUp.php?username=" + struser.ToLower () +"&pass=" + strpass +"&email=" + stremail.ToLower ());
		WWW signup = new WWW ("http://jonathanperron.ca/Vacuumhero/SignUp.php?username=" + struser.ToLower () +"&pass=" + strpass +"&email=" + stremail.ToLower ());
		yield return signup;
		string signupResult = signup.text;

		string[] signupResultSplit = signupResult.Split ('|');

		if(signupResultSplit[0].Equals("Error")) {
			if(signupResultSplit[1].Equals("03")) {
				Debug.Log ("UsernameTaken");
				txtUIErrorMessage.GetComponent<Text> ().text = "The username you entered is taken.";
			} else if(signupResultSplit[1].Equals("04")) {
				txtUIErrorMessage.GetComponent<Text> ().text = "The email you entered is taken.";
			} else {
				txtUIErrorMessage.GetComponent<Text> ().text = "An unknown error occured.";
			}
		}

		if (signupResultSplit [0].Equals ("1")) {

			CancelClicked ();
			txtUIErrorMessage.GetComponent<Text> ().text = "Signup successful!.";
		}
	}

	string CreateHash(string str) { //Create a Hash to send to server
		MD5 md5 = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		byte[] hash = md5.ComputeHash(bytes);

		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < hash.Length; i++) {
			sb.Append(hash[i].ToString("x2"));
		}
		return sb.ToString();
	}
}
