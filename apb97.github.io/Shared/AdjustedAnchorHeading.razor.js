export function activateCurrentLink(url, hash) {
    var h = document.querySelector(hash);
    h?.scrollIntoView();
    document.querySelectorAll(`a.nav-link.active:not([href='${url.replace(document.location.origin, '').replace(/\//, '')}'])`)?.forEach(n => n.classList.remove('active'));
    const currentNavLink = document.querySelector(`a.nav-link[href='${url.replace(document.location.origin, '').replace(/\//, '')}']`);
    currentNavLink?.classList?.add('active');
    currentNavLink?.click();
    if (h == null && currentNavLink != null) {
        setTimeout(r => activateCurrentLink(url, hash), 200);
    }
}