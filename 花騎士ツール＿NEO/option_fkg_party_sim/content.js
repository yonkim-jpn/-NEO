
///////////////////////////////
//
//�ԋR�m�o�^�p
//
///////////////////////////////

function CopyWiki() {

    //�N�����̑S�^�u�擾
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


    //�\�����đI�΂���

    //�I�΂ꂽ�^�u����f�[�^�擾���A�o�^�t�H�[���ɃR�s�[




}
