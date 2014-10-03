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
bower install --save react
```
