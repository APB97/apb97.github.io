export function activateCurrentLink(url, hash) {
    const h = document.querySelector(hash);
    h?.scrollIntoView();
    const href = url.replace(document.location.origin, '').replace(/\//, '');
    document.querySelectorAll(`a.nav-link.active:not([href='${href}'])`)
        ?.forEach(n => n.classList.remove('active'));
    const currentNavLink = document.querySelector(`a.nav-link[href='${href}']`);
    currentNavLink?.classList?.add('active');
    currentNavLink?.click();
    if (h == null && currentNavLink != null) {
        setTimeout(r => activateCurrentLink(url, hash), 200);
    }
}