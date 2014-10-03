# Introduction

 RankPin is designed to easily apply a ranking system with a simple setting in Unity. It provides a cost efficient method to simplify and speed up the process of Unity application and game development process. With a few clicks you will be able to apply a game ranking system.


# Functions of RankPin

 There are two functions of RankPin. First is to save data onto the server and the second to retrieve the saved data.
Saving data to server
 - User data: Information (e.g. name, image url) required in Ranking UI
 - Score: Accumulated score used for ranking.
 - Friend list: Upload self and friend’s list data.
Retrieving ranking data from server
 - My ranking: My ranking from all users, my ranking between friends.
 - All ranking: Accumulated ranking for all users, weekly all user ranking.
 - Friend ranking: Accumulated all users ranking, weekly friend ranking.
 * Weekly ranking is refreshed on Monday’s.
 
# Documents
```html
Assets/RankPin/Document
```

# Samples
- User Data Update
Update my information on server. Unique ID is required to update.
Alternatively, you can upload various information in a Hashtable format for user data.
(Internally Hashtable data is converted into JSON format and size over 500 bytes will not be saved on server)
```html
* Reqeust.
private void updateUser()
{
	Hashtable data = new Hashtable();
	data.Add("name", "Daniel");
	data.Add("url", “http://xxx.png“);
	this.updateUser(data);
}
* Response.
public override void onSuccessUser()
{
	base.onSuccessUser();
	Debug.Log("Update user informations.!");
}
public override void onFailUser(string message)
{
	base.onFailUser(message);
	Debug.LogWarning(message);
}
```
- Score Update
Perform operation of updating your ranking score information onto server.
Scores are all ranking, weekly ranking and friend ranking data.
(Score data uploads accumulated scores. Weekly rankings automatically determines current week’s score from server)
```html
* Reqeust.
private void updateScore()
{
	int score = 102345;
	this.updateScore((uint)score);
}
* Response.
public override void onSuccessScore()
{
	base.onSuccessScore();
	Debug.Log("Update score.!");
}
public override void onFailScore(string message)
{
	base.onFailScore(message);
	Debug.LogWarning("message);
}
```
- Update Friend List
Your friend’s list must be uploaded to server in order to use friend ranking. If you are not going to use this feature, the list does not need to be uploaded.
Friend’s list must update User ID of friend’s list to server in ArrayList.
```html
* Reqeust.
private void updateFriends()
{
	ArrayList friends = new ArrayList();
	friends.Add("33");
	friends.Add("367");
	friends.Add("369");
	this.updateFriends(friends);
}
* Response.
public override void onSuccessFriends()
{
	base.onSuccessFriends();
	Debug.Log("Update friends list.!");
}
public override void onFailFriends(string message)
{
	base.onFailFriends(message);
	Debug.LogWarning(message);
}
```
- My Ranking
Retrieves my ranking information. You can retrieve my ranking from all, weekly and friend ranking.
```html
* Reqeust.
private void requestRankMe()
{
	this.rankMe("me");
}
* Response.
public override void onSuccesMe(int total, int rank, int score, Hashtable data)
{
	base.onSuccesMe(total, rank, score, data);
	Debug.Log(string.Format("total:{0},rank:{1},score:{2}",total,rank,score));
}
public override void onFailMe(string message)
{
	base.onFailMe(message);
	Debug.LogWarning(message);
}
```
- All ranking
Retrieve ranking information from all users.
You can set offset and limit when retrieving ranking information.
offset : Ranking offset
limit : Numbers of user’s retrieved ranking data (maximum of 50)
```html
* Reqeust.
private void requestRank()
{
	this.rank("total");
}
* Response.
public override void onSuccessRank(int total, ArrayList users)
{
	base.onSuccessRank(total, users);
	Debug.Log(string.Format("total:{0}", total));
	foreach(Hashtable user in users)
	{
		HashtableHelper.print("Rank", user);
		Hashtable data = (Hashtable)user[RankPin.RankConstants.KEY_DATA];
		if(data == null)
			continue;
		HashtableHelper.print("Data", data);
	}
}
public override void onFailRank(string message)
{
	base.onFailRank(message);
	Debug.LogWarning(message);
}
```
