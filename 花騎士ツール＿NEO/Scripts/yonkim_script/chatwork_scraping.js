function timeConvert(timestamp) {
    var a = new Date(timestamp * 1000);
    var year = a.getFullYear();
    var month = a.getMonth() + 1;
    var date = a.getDate()
    var hour = a.getHours()
    var min = a.getMinutes()
    var time = `${year}年${month}月${date}日 ${hour}:${min}`
    return time;
}

    var arr = [
        ['date', 'orgName', 'name', 'iconSrc', 'message']
    ]
    $('._message').each((idx, item) => {
        var field = []
        if ($(item).find('._timeStamp').length > 0) {
            field.push(timeConvert($(item).find('._timeStamp').data('tm')))
        } else {
            return;
        }
        if ($(item).find('.timelineMessage__orgName').length > 0) {
            field.push($(item).find('.timelineMessage__orgName').text())
        } else {
            field.push('')
        }
        if ($(item).find('._speakerName').length > 0) {
            field.push($(item).find('._speakerName').text())
            field.push($(item).find('._speaker img').attr('src'))
        } else {
            field.push($('._message').eq(idx - 1).find('._speakerName').text())
            field.push($('._message').eq(idx - 1).find('._speaker img').attr('src'))
        }
        field.push($(item).find('.timelineMessage__message').text().replace(/,/gm, '、').replace(/(rn|n|r)/gm, ""))
        arr.push(field)
    })
    window.open(encodeURI("data:text/csv;charset=utf-8," + arr.map(e => e.join(",")).join("n")))
