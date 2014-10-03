using UnityEngine;
using System.Collections;

public class RankSampleGUI : MonoBehaviour
{
	private const float GUI_BUTTON_HEIGHT = 20f;
	private enum eSelect
	{
		TOTAL,
		FRIENDS,
		CONTEXT,
		NONE
	}

	// Select menu.
	private eSelect _selectMenu = eSelect.NONE;

	// Scroll position.
	private Vector2 _scrollPostion = Vector2.zero;

	// Me data.
	private int _meRank = 0;
	private int _meScore = 0;
	private int _meTotal = 0;
	private Hashtable _meData = null;
	
	// Rank data.
	private int _totalCount = 0;
	private ArrayList _rankList = null;

	// Message.
	private string _message = "None";
	public string message
	{
		set	{	this._message = value;		}
	}

	// User Id.
	private string _userId = null;
	public string userId
	{
		set	{	this._userId = value;		}
	}


	// Event & Delegate.
	public delegate void onTotalRank(RankSampleGUI obj);
	public event onTotalRank eTotalRank;

	public delegate void onFriendsRank(RankSampleGUI obj);
	public event onFriendsRank eFriendsRank;

	public delegate void onContextRank(RankSampleGUI obj);
	public event onContextRank eContextRank;


	public delegate void onUpdateUser(RankSampleGUI obj);
	public event onUpdateUser eUpdateUser;

	public delegate void onUpdateScore(RankSampleGUI obj);
	public event onUpdateScore eUpdateScore;

	public delegate void onUpdateFriends(RankSampleGUI obj);
	public event onUpdateFriends eUpdateFriends;


	public delegate void onRankPrev(RankSampleGUI obj, string uniqueName);
	public event onRankPrev eRankPrev;
	public delegate void onRankNext(RankSampleGUI obj, string uniqueName);
	public event onRankNext eRankNext;


	public delegate bool onHasRankPrev(RankSampleGUI obj, string uniqueName);
	public event onHasRankPrev eHasRankPrev;
	public delegate bool onHasRankNext(RankSampleGUI obj, string uniqueName);
	public event onHasRankNext eHasRankNext;

	void OnGUI()
	{
		this.drawRequestRankGUI();
		this.drawRankListGUI();
		this.drawRequestUpdateGUI();
		this.drawMessageGUI();
	}
	
	
	private void drawRequestRankGUI()
	{
		float height = GUI_BUTTON_HEIGHT + 10f;
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, height), GUI.skin.textArea);
		GUILayout.BeginHorizontal();
		{
			//GUILayout.FlexibleSpace();
			if(this._selectMenu == eSelect.TOTAL)
				GUI.enabled = false;
			if(GUILayout.Button("Rank", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				if(eTotalRank != null)
				{
					this._selectMenu = eSelect.TOTAL;
					this._message = "Request total rank......";
					this.clear();
					eTotalRank(this);
				}
			}
			GUI.enabled = true;
			
			if(this._selectMenu == eSelect.FRIENDS)
				GUI.enabled = false;
			if(GUILayout.Button("Friends", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				if(eFriendsRank != null)
				{
					this._selectMenu = eSelect.FRIENDS;
					this._message = "Request friends rank......";
					this.clear();
					eFriendsRank(this);
				}
			}
			GUI.enabled = true;
			
			if(this._selectMenu == eSelect.CONTEXT)
				GUI.enabled = false;
			if(GUILayout.Button("Context", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				if(eContextRank != null)
				{
					this._selectMenu = eSelect.CONTEXT;
					this._message = "Request context rank......";
					this.clear();
					eContextRank(this);
				}
			}
			GUI.enabled = true;
			
			//GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
	}
	private void drawMessageGUI()
	{
		
		float height = GUI_BUTTON_HEIGHT + 10f;
		GUILayout.BeginArea(new Rect(0, Screen.height-height, Screen.width, height), GUI.skin.box);
		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();
			GUILayout.Label(this._message);
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
	}
	private void drawRequestUpdateGUI()
	{
		float height = GUI_BUTTON_HEIGHT + 10f;
		GUILayout.BeginArea(new Rect(0, Screen.height-height*2, Screen.width, height));
		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();
			if(GUILayout.Button("Update User", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				if(eUpdateUser != null)
				{
					this._message = "Update user data......";
					eUpdateUser(this);
				}
			}
			if(GUILayout.Button("Update Score", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				if(eUpdateScore != null)
				{
					this._message = "Update score......";
					eUpdateScore(this);
				}
			}
			if(GUILayout.Button("Update Friends", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				if(eUpdateFriends != null)
				{
					this._message = "Update friends list......";
					eUpdateFriends(this);
				}
			}
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();		
	}
	private void drawRankListGUI()
	{
		float height = GUI_BUTTON_HEIGHT + 10f;
		float offset = 5f;
		Rect listRect = new Rect(0+offset, height,
		                         Screen.width-offset*2, Screen.height-height*3-offset);
		GUILayout.BeginArea(listRect, GUI.skin.box);
		
		float rankWidth = 60f;
		float scoreWidth = 100f;
		
		GUIStyle textStyle = new GUIStyle(GUI.skin.GetStyle("textField"));
		
		// Me data.
		if(this._meData != null)
		{
			GUI.color = Color.yellow;
			GUILayout.BeginHorizontal(textStyle, GUILayout.MaxHeight(48f));
			{
				GUILayoutOption[] option = { GUILayout.MaxWidth(48f), GUILayout.MaxHeight(48f) };
				
				string url = (string)this._meData["url"];
				Texture2D tex = RankPin.RankProfileInstance.instance.find(this._userId, url);
				
				// Rank.
				GUILayout.BeginVertical(GUILayout.MaxWidth(rankWidth));
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label(this._meRank.ToString(), GUI.skin.box, GUILayout.MaxWidth(rankWidth));
					float percent = (float)this._meRank/(float)this._meTotal * 100f;
					GUILayout.Label(percent.ToString("0.#") + "%", GUI.skin.box, GUILayout.MaxWidth(rankWidth));
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndVertical();
				// Profile Image.
				GUILayout.Label(tex, GUI.skin.box, option);
				// User Name.
				GUILayout.Label((string)this._meData["name"]);
				// Rank Score.
				GUILayout.Label(this._meScore.ToString(), GUI.skin.box, GUILayout.MaxWidth(scoreWidth));
			}
			GUILayout.EndHorizontal();
			GUI.color = Color.white;
		}
		
		// Rank data.
		if(this._rankList != null && this._rankList.Count >= 0)
		{
			GUI.backgroundColor = Color.yellow;
			GUI.enabled = this.enableRankPrev();
			if(GUILayout.Button("Previous", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				this.requestRankPrev();
			}
			GUI.enabled = true;
			GUI.backgroundColor = Color.white;
			
			this._scrollPostion = GUILayout.BeginScrollView(this._scrollPostion);
			{
				GUILayout.BeginHorizontal(GUI.skin.box);
				{
					GUILayout.Label("RANK", GUI.skin.box, GUILayout.MaxWidth(rankWidth));
					GUILayout.Label("USER", GUI.skin.box);
					GUILayout.Label("SCORE", GUI.skin.box, GUILayout.MaxWidth(scoreWidth));
				}
				GUILayout.EndHorizontal();
				
				GUILayoutOption[] option = { GUILayout.MaxWidth(38f), GUILayout.MaxHeight(38f) };
				foreach(Hashtable data in this._rankList)
				{
					object obj = data[RankPin.RankConstants.KEY_RANK];

					int rank = int.Parse(obj.ToString()) + 1;
					obj = data[RankPin.RankConstants.KEY_SCORE];
					int score = int.Parse(obj.ToString());
					string uId = (string)data[RankPin.RankConstants.KEY_USER_ID];
					
					GUIStyle style = GUI.skin.box;
					if(string.Equals(this._userId, uId) == true)
						style = textStyle;
					
					Hashtable user = (Hashtable)data[RankPin.RankConstants.KEY_DATA];
					if(user == null)
						continue;
					
					string url = (string)user["url"];
					Texture2D tex = RankPin.RankProfileInstance.instance.find(uId, url);
					GUILayout.BeginHorizontal(style);
					{
						GUI.color = Color.yellow;
						// Rank.
						float percent = (float)rank/(float)this._totalCount * 100f;
						GUILayout.Label(rank.ToString() + "\n(" + percent.ToString("0.#") + "%)",
						                GUI.skin.box, GUILayout.MaxWidth(rankWidth));
						GUI.color = Color.white;
						// Profile Image.
						GUILayout.Label(tex, GUI.skin.box, option);
						// User Name.
						GUILayout.Label((string)user["name"]);
						// Rank Score.
						GUILayout.Label(score.ToString(), GUI.skin.box, GUILayout.MaxWidth(scoreWidth));
					}
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();
			
			GUI.backgroundColor = Color.yellow;
			GUI.enabled = this.enableRankNext();
			if(GUILayout.Button("Next", GUILayout.MinHeight(GUI_BUTTON_HEIGHT)) == true)
			{
				this.requestRankNext();
			}
			GUI.enabled = true;
			GUI.backgroundColor = Color.white;
		}
		GUILayout.EndArea();
	}

	public void onSuccessRank(int total, ArrayList users)
	{
		this._totalCount = total;
		if(this._rankList == null)
		{
			this._rankList = users;
		}
		else
		{
			foreach(Hashtable table in users)
			{
				this._rankList.Add(table);
			}
			RankPin.RankSorter.sort(ref this._rankList);
		}
		this._message = "Success rank data.....";
	}
	public void onFailRank(string message)
	{
		this.clearRank();
		this._selectMenu = eSelect.NONE;
		this._message = "Fail rank data.....";
	}

	// Ranking me.
	public void onSuccesMe(int total, int rank, int score, Hashtable data)
	{
		this._meRank = rank + 1;
		this._meScore = score;
		this._meTotal = total;
		this._meData = data;
	}
	public void onFailMe(string message)
	{
		this.clearMe();
	}



	private void requestRankPrev()
	{
		if(eRankPrev == null)
			return;

		switch(this._selectMenu)
		{
		case eSelect.TOTAL :	eRankPrev(this, "total");		break;
		case eSelect.FRIENDS :	eRankPrev(this, "friends");		break;
		case eSelect.CONTEXT :	eRankPrev(this, "context");		break;
		}
	}
	private void requestRankNext()
	{
		if(eRankNext == null)
			return;

		switch(this._selectMenu)
		{
		case eSelect.TOTAL :	eRankNext(this, "total");		break;
		case eSelect.FRIENDS :	eRankNext(this, "friends");		break;
		case eSelect.CONTEXT :	eRankNext(this, "context");		break;
		}
	}

	private bool enableRankPrev()
	{
		if(eHasRankPrev == null)
			return false;
		switch(this._selectMenu)
		{
		case eSelect.TOTAL :	return eHasRankPrev(this, "total");
		case eSelect.FRIENDS :	return eHasRankPrev(this, "friends");
		case eSelect.CONTEXT :	return eHasRankPrev(this, "context");
		}
		return false;
	}
	private bool enableRankNext()
	{
		if(eHasRankNext == null)
			return false;
		switch(this._selectMenu)
		{
		case eSelect.TOTAL :	return eHasRankNext(this, "total");
		case eSelect.FRIENDS :	return eHasRankNext(this, "friends");
		case eSelect.CONTEXT :	return eHasRankNext(this, "context");
		}
		return false;
	}

	private void clear()
	{
		this.clearMe();
		this.clearRank();
	}
	private void clearMe()
	{
		this._meRank = 0;
		this._meScore = 0;
		this._meTotal = 1;
		this._meData = null;
	}
	private void clearRank()
	{
		this._totalCount = 0;
		this._rankList = null;
		this._scrollPostion = Vector2.zero;
	}
}
