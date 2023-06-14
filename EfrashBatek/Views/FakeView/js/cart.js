const header = document.querySelector("header");

window.addEventListener ("scroll", function(){
    header.classList.toggle ("sticky", this.window.scrollY > 0);
})

let menu = document.querySelector('#menu-icon');
let navmenu = document.querySelector('.navmenu');

menu.onclick = () => {
    menu.classList.toggle('bx-x');
    navmenu.classList.toggle('open');
}
const openbtn =document.querySelector("#cart");
const closebtn=document.querySelector("#close");
const cartcard=document.querySelector(".cartcard");
openbtn.addEventListener('click',opencartcard);
function opencartcard(){
    cartcard.style.display="grid";
}
closebtn.addEventListener('click',closecartcard);
function closecartcard(){
    cartcard.style.display="none";
}