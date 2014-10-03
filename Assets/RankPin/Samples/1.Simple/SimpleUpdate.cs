using UnityEngine;
using System.Collections;

public class SimpleUpdate : MonoBehaviour
{
	// Application key and secret. (Change your application key and secret code)
	private string APP_KEY = "Your Applcation Key";				// Your application key.
	private string APP_SECRET = "Your Application Secret Key";	// Your application secret code.

	private string _userId = "TestUser-41";

	void Awake()
	{
		// ********************************************************
		// Setting Test Application. (You must delete this line.)
		APP_KEY = RankPin.RankApp.getSampleAppKey();
		APP_SECRET = RankPin.RankApp.getSampleAppSecret();
		// ********************************************************
	}
	
	// Use this for initialization
	void Start()
	{
		// Update user informations.
		this.updateUser();

		// Update friends list.
		this.updateFriends();

		// Update score list.
		this.updateScore();
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	// Update user informations.
	private void updateUser()
	{
		// User data.
		Hashtable data = new Hashtable();
		data.Add("name", "Daniel");
		data.Add("url", "https://s3.amazonaws.com/rankpin-static/8.jpg");

		// # Method 1. Request
		//RankPin.UpdateUser obj =
		//	RankPin.RankHttp.createInstance<RankPin.UpdateUser>(this, APP_KEY, APP_SECRET, this._userId);
		//obj.eUpdateSuccess += onSuccessUpdateUser;
		//obj.eHttpFail += onFailUpdateUser;
		//obj.update(data);

		// # Method 2. Request
		RankPin.UpdateUser obj =
			RankPin.RankHttp.createInstance<RankPin.UpdateUser>(this, APP_KEY, APP_SECRET, this._userId);
		obj.setSuccessDelegate(this, "onSuccessUpdateUser");
		obj.setFailDelegate(this, "onFailUpdateUser");
		obj.update(data);
	}
	public void onSuccessUpdateUser()
	{
		Debug.Log("[Update User Success] Update user informations.!");
	}
	public void onFailUpdateUser(string message)
	{
		Debug.LogWarning("[Update User Error] " + message);
	}


	// Update friends list.
	private void updateFriends()
	{
		// Friends list.
		ArrayList friends = new ArrayList();
		friends.Add("TestUser-50");
		friends.Add("TestUser-121");
		friends.Add("TestUser-123");
		friends.Add("TestUser-136");
		friends.Add("TestUser-139");
		friends.Add("TestUser-251");
		friends.Add("TestUser-262");
		friends.Add("TestUser-271");
		friends.Add("TestUser-311");
		friends.Add("TestUser-322");
		friends.Add("TestUser-342");
		friends.Add("TestUser-343");

		// # Method 1. Request
		//RankPin.UpdateFriends obj =
		//	RankPin.RankHttp.createInstance<RankPin.UpdateFriends>(this, APP_KEY, APP_SECRET, this._userId);
		//obj.eUpdateSuccess += onSuccessUpdateFriends;
		//obj.eHttpFail += onFailUpdateFriends;
		//obj.update(friends);
		
		// # Method 2. Request
		RankPin.UpdateFriends obj =
			RankPin.RankHttp.createInstance<RankPin.UpdateFriends>(this, APP_KEY, APP_SECRET, this._userId);
		obj.setSuccessDelegate(this, "onSuccessUpdateFriends");
		obj.setFailDelegate(this, "onFailUpdateFriends");
		obj.update(friends);
	}
	public void onSuccessUpdateFriends()
	{
		Debug.Log("[Update Friends Success] Update friends list.!");
	}
	public void onFailUpdateFriends(string message)
	{
		Debug.LogWarning("[Update Friends Error] " + message);
	}



	// Update score.
	private void updateScore()
	{
		// Score.
		uint score = 3345;

		// # Method 1. Request
		//RankPin.UpdateScore obj =
		//	RankPin.RankHttp.createInstance<RankPin.UpdateScore>(this, APP_KEY, APP_SECRET, this._userId);
		//obj.eUpdateSuccess += onSuccessUpdateScore;
		//obj.eHttpFail += onFailUpdateScore;
		//obj.update(score);
		
		// # Method 2. Request
		RankPin.UpdateScore obj =
			RankPin.RankHttp.createInstance<RankPin.UpdateScore>(this, APP_KEY, APP_SECRET, this._userId);
		obj.setSuccessDelegate(this, "onSuccessUpdateScore");
		obj.setFailDelegate(this, "onFailUpdateScore");
		obj.update(score);

	}
	public void onSuccessUpdateScore()
	{
		Debug.Log("[Update Score Success] Update score.!");
	}
	public void onFailUpdateScore(string message)
	{
		Debug.LogWarning("[Update Score Error] " + message);
	}
}
