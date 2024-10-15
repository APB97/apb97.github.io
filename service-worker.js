// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

const CACHE_VERSION = "2.0.1";

const MAX_TTL = {
    '/': 3600,
    html: 43200,
    json: 43200,
    js: 86400,
    css: 86400,
    resx: 3600
};

const CACHE_BLACKLIST = [
    (str) =>  !/https:\/\/apb97[.]github[.]io\/?/i.test(str)
];

self.importScripts('./service-worker-assets.js');
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}-${CACHE_VERSION}`;
const offlineAssetsInclude = [ /\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/, /\.resx$/ ];
const offlineAssetsExclude = [ /^service-worker\.js$/ ];

// Replace with your base path if you are hosting on a subfolder. Ensure there is a trailing '/'.
const base = "/";
const baseUrl = new URL(base, self.origin);
const manifestUrlList = self.assetsManifest.assets.map(asset => new URL(asset.url, baseUrl).href);

async function onInstall(event) {
    console.info('Service worker: Install');

    // Fetch and cache all matching items from the assets manifest
    const assetsRequests = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !CACHE_BLACKLIST[0](asset.url))
        .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));
    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests)).then(() => self.skipWaiting());
}

async function onActivate(event) {
    console.info('Service worker: Activate');

    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}

async function onFetch(event) {
    let cachedResponse = null;
    if (event.request.method === 'GET') {
        // For all navigation requests, try to serve index.html from cache,
        // unless that request is for an offline resource.
        // If you need some URLs to be server-rendered, edit the following check to exclude those URLs
        const shouldServeIndexHtml = event.request.mode === 'navigate'
            && !manifestUrlList.some(url => url === event.request.url)
            && !/\/WebSudoku(.*|\/)$/.test(event.request.url);

        const request = shouldServeIndexHtml ? 'index.html' : event.request;
        const cache = await caches.open(cacheName);
        cachedResponse = await cache.match(request);

        var matches = event.request.url.match(/\..*$/g);
        var ttl = MAX_TTL[matches?.[matches.length - 1]];
        if (ttl && parseInt((new Date().getTime() - new Date(cachedResponse.headers.get('date')).getTime()) / 1000) > ttl) {
            cachedResponse = null;
        }

        if (/\/WebSudoku\/(sudoku|rules|printMultiple)/.test(event.request.url)) {
            return fetch('/WebSudoku/');
        }
        else if (/\/service-worker.js$/.test(event.request.url)) {
            return fetch('/service-worker.js');
        }
        else if (/\/404.html$/.test(event.request.url))
        {
            return fetch(event.request.url);
        }
        else if (/\/WebSudoku\/(js|_framework)\/.*/.test(event.request.url)) {
            return fetch(event.request.url.replace('https://apb97.github.io', ''));
        }
    }

    return cachedResponse || fetch(event.request);
}
/* Manifest version: zynUDbKW */
