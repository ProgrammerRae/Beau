/*
document.getElementById('sidebarToggleBtn').addEventListener('click', function () {
    document.getElementById('sidebar').classList.toggle('active');
});
*/

//3dot menu
function menu(button) {

    const buttonRect = button.getBoundingClientRect();

    const top = buttonRect.bottom;
    const left = buttonRect.left;

    document.getElementById("menu-panel").style.top = `${top}px`;
    document.getElementById("menu-panel").style.left = `${left - 230}px` ;

    document.getElementById("menu-panel").classList.add("dot-menu");
    document.getElementById("info").classList.add("showinfo");

    document.getElementById('main-container').classList.add('no-scroll');
    document.getElementById('parent-container').classList.add('no-scroll');

}

function exit() {
    document.getElementById("menu-panel").classList.remove("dot-menu");
    document.getElementById("info").classList.remove("showinfo");

    document.getElementById('main-container').classList.remove('no-scroll');
    document.getElementById('parent-container').classList.remove('no-scroll');

}