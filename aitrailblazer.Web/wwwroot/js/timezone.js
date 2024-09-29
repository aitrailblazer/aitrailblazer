window.timezoneHelper = {
    getTimeZone: function () {
        return Intl.DateTimeFormat().resolvedOptions().timeZone;
    },
    setTimeZone: function (timeZone) {
        localStorage.setItem('userTimeZone', timeZone);
    },
    getStoredTimeZone: function () {
        return localStorage.getItem('userTimeZone');
    }
};
