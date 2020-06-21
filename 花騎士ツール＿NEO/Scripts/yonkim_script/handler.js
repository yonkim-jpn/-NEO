var handler = (function () {
    var events = {},
        key = 0;

    return {
        addListener: function (target, type, listener, capture) {
            if (window.addEventListener) {
                target.addEventListener(type, listener, capture);
            } else if (window.attachEvent) {
                target.attachEvent('on' + type, listener);
            }
            events[key] = {
                target: target,
                type: type,
                listener: listener,
                capture: capture
            };
            return key++;
        },
        removeListener: function (key) {
            if (key in events) {
                var e = events[key];
                if (window.removeEventListener) {
                    e.target.removeEventListener(e.type, e.listener, e.capture);
                } else if (window.detachEvent) {
                    e.target.detachEvent('on' + e.type, e.listener);
                }
            }
        }
    };
})();