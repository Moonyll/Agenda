//var mysql = require('mysql');
//var con = mysql.createConnection({
//    host: "localhost",
//    user: "myusername",
//    password: "mypassword",
//    database: "Agenda"
//});
//*********************************************************//
//con.connect(function (err) {
//    if (err) throw err;
//    //Select all customers and return the result object:
//    con.query("SELECT * FROM Customer", function (err, result, fields) {
//        if (err) throw err;
//        console.log(result);
//    });
//});
$(document).ready(function () {
   var arr = db.Customers.SqlQuery("SELECT dbo.[FirstName] FROM [Customers]").ToArray();
    var a = Math.random();
$('#listingTable').html(arr[2]);

});