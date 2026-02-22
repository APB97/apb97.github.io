function handleCustomSelectResize(id, displayDownward) {
    let rect = document.querySelector(`#${id}`).getBoundingClientRect();
    let list = document.querySelector(`#${id}-list`);
    list.style['left'] = `${rect.x}px`;
    list.style['width'] = `${rect.width}px`;
    list.style['top'] = `${displayDownward ? rect.y : rect.y - rect.height}px`;
}

export function registerOnResize(elementId, downward = false) {
    handleCustomSelectResize(elementId, downward);
    window.addEventListener("resize", () => handleCustomSelectResize(elementId, downward));
}

export function unregisterOnResize(elementId, downward = false) {
    window.removeEventListener("resize", () => handleCustomSelectResize(elementId, downward));
}