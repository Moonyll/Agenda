var current_page = 1;
var records_per_page = 5; //Données par page
var objJson = [
    { adName: "AdName 1" },
    { adName: "AdName 2" },
    { adName: "AdName 3" },
    { adName: "AdName 4" },
    { adName: "AdName 5" },
    { adName: "AdName 6" },
    { adName: "AdName 7" },
    { adName: "AdName 8" },
    { adName: "AdName 9" },
    { adName: "AdName 10" }
];
// Can be obtained from another source, such as your objJson variable
// Fonction page précédente
function prevPage() {
    if (current_page > 1) {
        current_page--;
        changePage(current_page);
    }
}
// Fonction page suivante
function nextPage() {
    if (current_page < numPages()) {
        current_page++;
        changePage(current_page);
    }
}
// Fonction page courante
function changePage(page) {
    var btn_next = document.getElementById("btn_next");
    var btn_prev = document.getElementById("btn_prev");
    var listing_table = document.getElementById("listing");
    var page_span = document.getElementById("page");
    // Validate page
    if (page < 1) page = 1;
    if (page > numPages()) page = numPages();

    listing_table.innerHTML = "";

    for (var i = (page - 1) * records_per_page; i < (page * records_per_page); i++) {
        listing_table.innerHTML += objJson[i].adName + "<br>"; //Remettre adName
    }
    page_span.innerHTML = page;

    if (page == 1) {
        btn_prev.style.visibility = "hidden";
    } else {
        btn_prev.style.visibility = "visible";
    }

    if (page == numPages()) {
        btn_next.style.visibility = "hidden";
    } else {
        btn_next.style.visibility = "visible";
    }
}
// Nombre total de pages par rapport à la taille totale de l'objet
function numPages() {
    return Math.ceil(objJson.length / records_per_page);
}
// Chargement de la page et affichage de la première page de données
window.onload = function () {
    changePage(1);
};