var gklistboxDivID;
var gklistboxPlayer;

function initGKListbox(divID, player){
	gklistboxDivID = divID;
	gklistboxPlayer = player;
	gklistboxPlayer.jwAddEventListener("jwplayerPlaylistItem","gkListboxplItem");
	buildListbox();
}

function buildListbox(){
	var arrItem = gklistboxPlayer.jwGetPlaylist();
	var totalItem = arrItem.length;
	var curIndex = gklistboxPlayer.jwGetPlaylistIndex();
	
	var txtList = '<div style="background-color:#000;width:'+player.width+'px">';
	for(var i=0;i<totalItem;i++){
		var title = arrItem[i].title;
		if(i==curIndex){
			txtList += '<a id="gklbindex'+i+'" style="background-color:#000000;color:#FF0000;width:20px" onclick="gkListboxItemClick(this)">'+title+'</a> ';
		}else{
			txtList += '<a id="gklbindex'+i+'" style="background-color:#000000;color:#CCCCCC;width:20px" onclick="gkListboxItemClick(this)">'+title+'</a> ';
		}
	}
	txtList += "</div>";
	
	var div = document.getElementById(gklistboxDivID);
	div.innerHTML = txtList;
}

function gkListboxItemClick(item){
	var index = Number(item.id.split("gklbindex")[1]);
	gklistboxPlayer.jwPlaylistItem(index);
}



function gkListboxplItem(){
	buildListbox();
}