export function activateCurrentLink(url) {
    document.querySelectorAll('a.nav-link.active')?.forEach(n => n.classList.remove('active'));
    document.querySelector(`a.nav-link[href='${url.replace(/\//, '')}']`)?.classList?.add('active');
}