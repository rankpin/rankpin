using UnityEngine;
using System.Collections;

public class RankAppUpdate : RankPin.RankMono
{
	void Awake()
	{
		if(this.rankApp == null)
			return;

		// ********************************************************
		// Setting Test Application. (You must delete this line.)
		this.rankApp.useSampleApp();
		// ********************************************************
		
		// Setting user id.
		this.rankApp.userId = "TestUser-41";		
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
	{}

	
	// Update user informations.
	private void updateUser()
	{
		// Update user data.
		Hashtable data = new Hashtable();
		data.Add("name", "Daniel");
		data.Add("url", "https://s3.amazonaws.com/rankpin-static/8.jpg");
		this.updateUser(data);
	}
	public override void onSuccessUser()
	{
		base.onSuccessUser();
		Debug.Log("[Update User Success] Update user informations.!");
	}
	public override void onFailUser(string message)
	{
		base.onFailUser(message);
		Debug.LogWarning("[Update User Error] " + message);
	}
	
	
	// Update friends.
	private void updateFriends()
	{
		// Update friends
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
		this.updateFriends(friends);
	}
	public override void onSuccessFriends()
	{
		base.onSuccessFriends();
		Debug.Log("[Update Friends Success] Update friends list.!");
	}
	public override void onFailFriends(string message)
	{
		base.onFailFriends(message);
		Debug.LogWarning("[Update Friends Error] " + message);
	}


	// Update score.
	private void updateScore()
	{
		//int score = Random.Range(0, int.MaxValue/10);
		int score = 3345;
		this.updateScore((uint)score);
	}
	public override void onSuccessScore()
	{
		base.onSuccessScore();
		Debug.Log("[Update Score Success] Update score.!");
	}
	public override void onFailScore(string message)
	{
		base.onFailScore(message);
		Debug.LogWarning("[Update Score Error] " + message);
	}
}
