var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
var today = new Date();
d = today.toLocaleDateString("fr-FR", options).toUpperCase();
t = today.toLocaleTimeString();

document.getElementById("date").textContent = d; // Affichage de la date dans la page d'accueil
document.getElementById("time").textContent = t; // Affichage de l'heure date dans la page d'accueil

