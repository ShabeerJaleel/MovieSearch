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
    var msgError = "File invalid or deleted";
    if (msg.indexOf(msgError) > 0) {
        var link = msg.substring(0, msg.indexOf('\n'));
        $.post('/api/query/removeLink?link=' + link);
        msg = msg + ". Please try other links, if any";
    }
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
    if (msg.indexOf(msgError) > 0) {
        var link = msg.substring(0, msg.indexOf(msgError));
        $.post('/api/query/removeLink?link=' + link);
        msg = msg + ". Please try other links, if any";
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