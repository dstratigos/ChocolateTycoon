function storeActionObserver() { // Observes user interaction with "Restock" and "Sell" button.

    let url = window.location.search;
    url = url.replace("?", ''); // remove the ? in the url link.
    console.log(url);



    var index = url.indexOf("&");
    var soldStatus = url.substr(0, index); 
    var dailyEarningsTxt = url.substr(index + 1);
    let dailyEarnings = dailyEarningsTxt.indexOf("=");
    dailyEarnings = dailyEarningsTxt.substr(dailyEarnings + 1).replace("%2C", ",");

    console.log(soldStatus);
    console.log(dailyEarnings);



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

    if ($('#chocolateZero').length) {
        $("#sellButton").addClass("disabled");
    }

    if (url === "restocked=True") {
        $("#chocolateRestockSuccess").removeClass("hidden");
        console.log("Restock function success.")
    } else {
        console.log("Restock function is not called.")
    }

    if (soldStatus === "sold=True") {
        $("#sellSuccess").removeClass("hidden");
        $("#dailyEarnings").text(dailyEarnings);
        console.log("Sell function success.")
    } else {
        console.log("Sell function is not called.")
    }

    if ($('#adequateStaff').length) {
        $("#restockButton").addClass("disabled");
        $("#sellButton").addClass("disabled");
    }

    

}