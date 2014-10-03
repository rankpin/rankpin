using UnityEngine;
using System.Collections;

public class SimpleRank : MonoBehaviour
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
		// Request rank me.
		this.requestRankMe();

		// Request rank total.
		this.requestRank();

		// Request context rank.
		this.requestContextRank();
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	// Request rank me.
	private void requestRankMe()
	{
		// # Method 1. Request
		//RankPin.RankMe obj =
		//	RankPin.RankHttp.createInstance<RankPin.RankMe>(this, APP_KEY, APP_SECRET, this._userId);
		//obj.eRankSuccess += onSuccesMe;
		//obj.eHttpFail += onFailMe;
		//obj.request(RankPin.RankConstants.eUserPool.GLOBAL, RankPin.RankConstants.eTerm.ALL);

		// # Method 2. Request
		RankPin.RankMe obj =
			RankPin.RankHttp.createInstance<RankPin.RankMe>(this, APP_KEY, APP_SECRET, this._userId);
		obj.setSuccessDelegate(this, "onSuccesMe");
		obj.setFailDelegate(this, "onFailMe");
		obj.request(RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.ALL);
	}

	// Ranking me response.
	public void onSuccesMe(int total, int rank, int score, Hashtable data)
	{
		Debug.Log(string.Format("[Rank Me] total:{0}, rank:{1}, score:{2}", total, rank, score));
		HashtableHelper.print("Rank Me", data);
	}
	public void onFailMe(string message)
	{
		Debug.LogWarning("[Request Rank Me Error] " + message);
	}

	// Request rank total.
	private void requestRank()
	{
		// # Method 1. Request
		//RankPin.RankTotal obj =
		//	RankPin.RankHttp.createInstance<RankPin.RankTotal>(this, APP_KEY, APP_SECRET, this._userId);
		//obj.eRankSuccess += onSuccessRank;
		//obj.eHttpFail += onFailRank;
		//obj.request(RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.ALL, 0, 10);
		
		// # Method 2. Request
		RankPin.RankTotal obj =
			RankPin.RankHttp.createInstance<RankPin.RankTotal>(this, APP_KEY, APP_SECRET, this._userId);
		obj.setSuccessDelegate(this, "onSuccessRank");
		obj.setFailDelegate(this, "onFailRank");
		obj.request(RankPin.RankConstants.eUserPool.GLOBAL, RankPin.RankConstants.eTerm.ALL, 10, 5);
	}

	// Rank response.
	public void onSuccessRank(int total, ArrayList users)
	{
		Debug.Log(string.Format("[Rank Total] total:{0}", total));
		foreach(Hashtable user in users)
		{
			HashtableHelper.print("--Rank Total", user);
			Hashtable data = (Hashtable)user[RankPin.RankConstants.KEY_DATA];
			if(data == null)
				continue;
			HashtableHelper.print("----Data", data);
		}
	}
	public void onFailRank(string message)
	{
		Debug.LogWarning("[Request Rank Error] " + message);
	}


	// Request context rank.
	private void requestContextRank()
	{
		// # Method 1. Request
		RankPin.RankTotal obj =
			RankPin.RankHttp.createInstance<RankPin.RankTotal>(this, APP_KEY, APP_SECRET, this._userId);
		obj.eRankSuccess += onSuccessContextRank;
		obj.eHttpFail += onFailContextRank;
		obj.request(RankPin.RankConstants.eUserPool.FRIENDS, RankPin.RankConstants.eTerm.ALL, -5, 10);
		
		// # Method 2. Request
		//RankPin.RankTotal obj =
		//	RankPin.RankHttp.createInstance<RankPin.RankTotal>(this, APP_KEY, APP_SECRET, this._userId);
		//obj.setSuccessDelegate(this, "onSuccessContextRank");
		//obj.setFailDelegate(this, "onFailContextRank");
		//obj.request(RankPin.RankConstants.eUserPool.GLOBAL, RankPin.RankConstants.eTerm.ALL,
		//            RankPin.RankConstants.eContext.ME, -5, 10);
	}
	
	// Rank response.
	public void onSuccessContextRank(int total, ArrayList users)
	{
		Debug.Log(string.Format("[Context Rank] total:{0}", total));
		foreach(Hashtable user in users)
		{
			HashtableHelper.print("--Context Rank", user);
			Hashtable data = (Hashtable)user[RankPin.RankConstants.KEY_DATA];
			if(data == null)
				continue;
			HashtableHelper.print("--Data", user);
		}
	}
	public void onFailContextRank(string message)
	{
		Debug.LogWarning("[Request Context Rank Error] " + message);
	}
}
