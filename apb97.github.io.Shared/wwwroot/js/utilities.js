export function alert(message) {
    window.alert(message);
}

export function getSetting(key) {
    return localStorage.getItem(key);
}

export function setSetting(key, value) {
    localStorage.setItem(key, value);
}

export function removeSetting(key) {
    localStorage.removeItem(key);
}

export function getSessionSetting(key) {
    return sessionStorage.getItem(key);
}

export function removeSessionSetting(key) {
    sessionStorage.removeItem(key);
}
