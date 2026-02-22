async function checkForBreakpoint(dotnetHelper, methodName, operator, breakpoint, state) {
    let shouldHide = null;
    switch (operator) {
        case "LT":
            shouldHide = window.innerWidth < breakpoint;
            break;
        case "LTE":
            shouldHide = window.innerWidth <= breakpoint;
            break;
        case "GT":
            shouldHide = window.innerWidth > breakpoint;
            break;
        case "GTE":
            shouldHide = window.innerWidth >= breakpoint;
            break;
    }

    if (state.hidden != shouldHide) {
        await dotnetHelper.invokeMethodAsync(methodName, shouldHide);
        state.hidden = shouldHide;
    }
}

export async function addListeners(dotnetHelper, methodName, operator, breakpoint, hidden) {
    let state = { hidden: hidden }

    window.addEventListener('load', async () => await checkForBreakpoint(dotnetHelper, methodName, operator, breakpoint, state));
    window.addEventListener('resize', async () => await checkForBreakpoint(dotnetHelper, methodName, operator, breakpoint, state));

    await checkForBreakpoint(dotnetHelper, methodName, operator, breakpoint, state);
}

export function removeListeners(dotnetHelper, methodName, operator, breakpoint, hidden) {
    let state = { hidden: hidden }

    window.removeEventListener('load', async () => await checkForBreakpoint(dotnetHelper, methodName, operator, breakpoint, state));
    window.removeEventListener('resize', async () => await checkForBreakpoint(dotnetHelper, methodName, operator, breakpoint, state));
}
