
///////////////////////////////
//
//花騎士登録用
//
///////////////////////////////

function CopyWiki() {

    //クロムの全タブ取得
    chrome.windows.getAll({ populate: true }, getAllOpenWindows);
    function getAllOpenWindows(winData) {

        var tabs = [];
        for (var i in winData) {
            if (winData[i].focused === true) {
                var winTabs = winData[i].tabs;
                var totTabs = winTabs.length;
                for (var j = 0; j < totTabs; j++) {
                    tabs.push(winTabs[j].url);
                }
            }
        }
        console.log(tabs);
    }


    //表示して選ばせる

    //選ばれたタブからデータ取得し、登録フォームにコピー




}
