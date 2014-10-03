using UnityEngine;
using System.Collections;

public class RankAppRank : RankPin.RankMono
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
		// Request rank me.
		this.requestRankMe();
		
		// Request rank total.
		this.requestRank();
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	// Request rank me.
	private void requestRankMe()
	{
		// # Method 1. Request
		// - Rank me from inspector setting.
		this.rankMe("me");
		// - Rank me from friends.
		//this.rankMe("me", RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.ALL);
		// - Rank me from all user and weekly.
		//this.rankMe("me", RankPin.RankConstants.eUserPool.GLOBAL, RankPin.RankConstants.eTerm.WEEK);
		// - Rank me from friends and weekly.
		//this.rankMe("me", RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.WEEK);

		// # Method 2. Request
		//RankPin.RankMe rank = this.getRankInst<RankPin.RankMe>("me");
		//if(rank != null)
		//{
			// Rank me from friends.
		//rank.request(RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.ALL);
		//}
	}
	// Request total rank.
	private void requestRank()
	{
		// # Method 1. Request
		// - Rank from inspector setting.
		this.rank("total");
		// - Rank from friends and week.
		//this.rank("total", RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.WEEK);
		// - Rank from gloable and week.(offset : 0, limit : 10)
		//this.rank("total", RankPin.RankConstants.eUserPool.GLOBAL, RankPin.RankConstants.eTerm.WEEK, 0, 10);

		// # Method 2. Request
		//RankPin.RankTotal rank = this.getRankInst<RankPin.RankTotal>("total");
		//if(rank != null)
		//{
		// Rank me from friends.
		//rank.request(RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.ALL);
		//}

		// # Method 3. Request
		// - Context Rank.
		//this.rank("total", RankPin.RankConstants.eUserPool.GLOBAL, RankPin.RankConstants.eTerm.ALL,
		//          RankPin.RankConstants.eContext.ME);
	}
	// Rank response.
	public override void onSuccessRank(int total, ArrayList users)
	{
		base.onSuccessRank(total, users);
		Debug.Log(string.Format("[Rank] total:{0}", total));
		foreach(Hashtable user in users)
		{
			HashtableHelper.print("--Rank", user);
			Hashtable data = (Hashtable)user[RankPin.RankConstants.KEY_DATA];
			if(data == null)
				continue;
			HashtableHelper.print("----Data", data);
		}

	}
	public override void onFailRank(string message)
	{
		base.onFailRank(message);
		Debug.LogWarning("[Request Rank Error] " + message);
	}
	
	// Ranking me response.
	public override void onSuccesMe(int total, int rank, int score, Hashtable data)
	{
		base.onSuccesMe(total, rank, score, data);
		Debug.Log(string.Format("[Rank Me] total:{0}, rank:{1}, score:{2}", total, rank, score));
		HashtableHelper.print("Rank Me", data);
	}
	public override void onFailMe(string message)
	{
		base.onFailMe(message);
		Debug.LogWarning("[Request Rank Me Error] " + message);
	}
}
