using UnityEngine;
using System.Collections;

public class RankAppSample : RankPin.RankMono
{
	void Awake()
	{
		Debug.Log("Awake");
		if(this.rankApp == null)
			return;

		// Your default profile image.
		//Texture2D tex = Resources.Load("RankPin/default_profile") as Texture2D;
		//if(tex != null)
		//	RankPin.RankProfileInstance.instance.defaultProfile = tex;

		// ********************************************************
		// Setting Test Application. (You must delete this line.)
		this.rankApp.useSampleApp();
		// ********************************************************

		// Setting user id.
		this.rankApp.userId = "TestUser-41";


		// GUI event handler.
		RankSampleGUI gui = this.gameObject.GetComponent<RankSampleGUI>();
		if(gui != null)
		{
			gui.userId = this.rankApp.userId;
			gui.eTotalRank += onTotalRank;
			gui.eFriendsRank += onFriendsRank;
			gui.eContextRank += onContextRank;

			gui.eUpdateUser += onUpdateUser;
			gui.eUpdateScore += onUpdateScore;
			gui.eUpdateFriends += onUpdateFriends;

			gui.eRankPrev += onRankPrev;
			gui.eRankNext += onRankNext;
			gui.eHasRankPrev += enableRankPrev;
			gui.eHasRankNext += enableRankNext;
		}
	}
	// Use this for initialization
	void Start()
	{}
	
	// Update is called once per frame
	void Update()
	{}


	// Request Total Rank and Me.
	private void onTotalRank(RankSampleGUI obj)
	{
		this.rankMe("me");
		this.rank("total");
	}
	// Request Friends Rank and Me.
	private void onFriendsRank(RankSampleGUI obj)
	{
		this.requestRank("friends-me");
		this.requestRank("friends");
	}
	
	// Request Context Rank and Me.
	private void onContextRank(RankSampleGUI obj)
	{
		this.requestRank("me");
		this.requestRank("context");
	}
	// Request Previous Rank.
	private void onRankPrev(RankSampleGUI obj, string uniqueName)
	{
		this.rankPrev(uniqueName);
	}
	// Request Next Rank.
	private void onRankNext(RankSampleGUI obj, string uniqueName)
	{
		this.rankNext(uniqueName);
	}
	// Rank response.
	public override void onSuccessRank(int total, ArrayList users)
	{
		base.onSuccessRank(total, users);

		RankSampleGUI gui = this.gameObject.GetComponent<RankSampleGUI>();
		if(gui != null)
			gui.onSuccessRank(total, users);
	}
	public override void onFailRank(string message)
	{
		base.onFailRank(message);

		RankSampleGUI gui = this.gameObject.GetComponent<RankSampleGUI>();
		if(gui != null)
			gui.onFailRank(message);
	}

	// Ranking me response.
	public override void onSuccesMe(int total, int rank, int score, Hashtable data)
	{
		base.onSuccesMe(total, rank, score, data);

		RankSampleGUI gui = this.gameObject.GetComponent<RankSampleGUI>();
		if(gui != null)
			gui.onSuccesMe(total, rank, score, data);
	}
	public override void onFailMe(string message)
	{
		base.onFailMe(message);

		RankSampleGUI gui = this.gameObject.GetComponent<RankSampleGUI>();
		if(gui != null)
			gui.onFailMe(message);
	}


	// Update user.
	private void onUpdateUser(RankSampleGUI obj)
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
		this.setMessage("Success update user data......");
	}
	public override void onFailUser(string message)
	{
		base.onFailUser(message);
		this.setMessage("Fail update user data......" + message);
	}


	// Update score.
	private void onUpdateScore(RankSampleGUI obj)
	{
		//int score = Random.Range(0, int.MaxValue/10);
		int score = 2345;
		this.updateScore((uint)score);
	}
	public override void onSuccessScore()
	{
		base.onSuccessScore();
		this.setMessage("Success update score......");
	}
	public override void onFailScore(string message)
	{
		base.onFailScore(message);
		this.setMessage("Fail update score......" + message);
	}


	// Update friends.
	private void onUpdateFriends(RankSampleGUI obj)
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
		this.setMessage("Success update friend list......");
	}
	public override void onFailFriends(string message)
	{
		base.onFailFriends(message);
		this.setMessage("Fail update friend list......" + message);

	}



	// Check prev / next rank.
	private bool enableRankPrev(RankSampleGUI obj, string uniqueName)
	{
		return this.hasRankPrev(uniqueName);
	}
	private bool enableRankNext(RankSampleGUI obj, string uniqueName)
	{
		return this.hasRankNext(uniqueName);
	}

	// Setting log message.
	private void setMessage(string message)
	{
		RankSampleGUI gui = this.gameObject.GetComponent<RankSampleGUI>();
		if(gui == null)
			return;
		gui.message = message;
	}
}
