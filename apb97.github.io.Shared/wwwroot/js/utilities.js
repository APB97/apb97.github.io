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

export function downloadFile(filename, contents, mimeType) {
    const file = new File([contents], filename, { type: mimeType });
    const exportUrl = URL.createObjectURL(file);

    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = filename;
    a.target = "_self";
    a.click();

    URL.revokeObjectURL(exportUrl);
}
