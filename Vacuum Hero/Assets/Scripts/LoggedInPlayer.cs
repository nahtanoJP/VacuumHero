using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggedInPlayer {

	private int id, firstlogin;
	private string username, email, lastupdate, signup, gender;

	public LoggedInPlayer(int id, int firstlogin, string username, string email, string lastupdate, string signup, string gender) {
		this.id = id;
		this.firstlogin = firstlogin;
		this.username = username;
		this.email = email;
		this.lastupdate = lastupdate;
		this.signup = signup;
		this.gender = gender;
	}

	public int ID {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public int Firstlogin {
		get {
			return this.firstlogin;
		}
		set {
			firstlogin = value;
		}
	}

	public string Username {
		get {
			return this.username;
		}
		set {
			username = value;
		}
	}

	public string Email {
		get {
			return this.email;
		}
		set {
			email = value;
		}
	}

	public string Lastupdate {
		get {
			return this.lastupdate;
		}
		set {
			lastupdate = value;
		}
	}

	public string Signup {
		get {
			return this.signup;
		}
		set {
			signup = value;
		}
	}

	public string Gender {
		get {
			return this.gender;
		}
		set {
			gender = value;
		}
	}
}
