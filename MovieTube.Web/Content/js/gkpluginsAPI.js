var gkplugins_messageHandleType = "custom"; //(removeLinkError/custom/none)
var gkplugins_listboxDivID = "";


function gkpluginsAPI(playerName,playerID){
	player = gkthisMovie(playerName);
	if(!player){
		player = gkthisMovie(playerID);
	}
	if(gkplugins_messageHandleType=="removeLinkError"){
		player.onGKMessageHandle("gkMessageHandleRemoveLinkError");
	}else if(gkplugins_messageHandleType=="custom"){
		player.onGKMessageHandle("gkMessageHandle");
	}
	if(gkplugins_listboxDivID!=""){
		player.onGKPluginsLoadedHandle("gkPluginsLoaded");
	}
	player.onGKNextLocationHandle("gkNextLocation");
	//player.onGKPluginRunHandle("gkPluginRun");
}
var player;

function gkPluginRun(pluginName){
	alert(pluginName);
}

function gkNextLocation(){
	alert("next location");
}

function gkMessageHandle(msg){
	//your custom code
	return msg;
}

function gkPluginsLoaded(){
	try{
		initGKListbox(gkplugins_listboxDivID,player);
	}catch(e){
		alert(e.message);
	}
}

function gkMessageHandleRemoveLinkError(msg){
	var msgError = "File invalid or deleted";
	if(msg.indexOf(msgError)>0){
		msg = msgError;
	}
	return msg;
}

function gkthisMovie(movieName) {
	if(movieName==""){
		return null;
	}
	var p = document.getElementById(movieName)
	if(!p.onGKMessageHandle){
		if (navigator.appName.indexOf("Microsoft") != -1) {
			p = window[movieName];
		}else{
			p = document[movieName];
		}
	}
	return p;
}