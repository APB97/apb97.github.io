﻿export function activateCurrentLink(url, hash, tries = DEFAULT_TRIES) {
    if (tries <= 0) return;

    if (url == null && hash == null) {
        url = document.location.href;
        hash = document.location.hash;
    }

    if (hash.length <= 1) return;

    const elementWithId = document.querySelector(hash);
    elementWithId?.scrollIntoView();
    const href = url.replace(document.location.origin, '').replace(/\//, '');
    document.querySelectorAll(`a.nav-link.active:not([href='${href}'])`)
        ?.forEach(navLink => navLink.classList.remove('active'));
    const currentNavLink = document.querySelector(`a.nav-link[href='${href}']`);
    currentNavLink?.classList?.add('active');
    currentNavLink?.click();
    if (elementWithId == null) {
        setTimeout(_ => activateCurrentLink(url, hash, tries - 1), MS_DELAY);
    }
}

const DEFAULT_TRIES = 4;
const MS_DELAY = 150;