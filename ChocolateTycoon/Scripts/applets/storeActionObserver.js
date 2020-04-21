﻿function storeActionObserver() { // Observes user interaction with "Restock" and "Sell" button.

    let url = window.location.search;
    url = url.replace("?", ''); // remove the ? in the url link.
    console.log(url);

    if ($('#salesForTodayCompleted').length) 
    {
        $("#restockButton").addClass("disabled");
        $("#sellButton").addClass("disabled");

        $("#salesForTodayCompleted").removeClass("hidden");
    }

    if (url === "restocked=False") {
        $("#restockFail").removeClass("hidden");
        $("#chocolateZero").addClass("hidden");
        console.log("Restock function failed.")
    } else {
        console.log("Restock function is not called.")
    }

    if (url === "restocked=True") {
        $("#chocolateRestockSuccess").removeClass("hidden");
        console.log("Restock function success.")
    } else {
        console.log("Restock function is not called.")
    }

    if (url === "sold=True") {
        $("#sellSuccess").removeClass("hidden");
        console.log("Sell function success.")
    } else {
        console.log("Sell function is not called.")
    }



}