if (window.location.hash.length > 1) {
    const path = window.location.hash.replace('#', '').replace('?', '');
    history.pushState({ page: 1 }, "apb97.github.io", 'https://apb97.github.io' + path);
}