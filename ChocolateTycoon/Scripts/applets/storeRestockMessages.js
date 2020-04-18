function restock() {

    let url = window.location.search;
    url = url.replace("?", ''); // remove the ?
    console.log(url);
    console.log(url === "restocked=True");


    if (url === "restocked=True") {
        $("#chocolateRestockSuccess").removeClass("hidden");
    //} else if (restock(AdequateChocolate) && url === "restocked=True") {
    //    $("#sellMessage").append("<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'>Error:</span>Not enough stock!</div>");
    //} else if (restock(Success) && url === "restocked=True") {
    //    $("#sellMessage").append("<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'>Error:</span>Restock complete!</div>");
    } else {
        console.log("Something went wrong in restock function.")
    }
}