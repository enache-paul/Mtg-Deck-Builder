$(document).ready(function () {
    $("#add-to-deck").click(function () {
        $.ajax({
            url: "/Session/AddCardToSession",
            method: "POST",
            data: { cardId : parseInt($("#add-to-deck").attr("data-id")) },
            success: function() {
                console.log("Card added to session");
            },
            error: function(error) {
                console.log("Error adding card to session: " + error.toString());
            }
        });
    });
});
