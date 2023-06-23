$(document).ready(function () {
    $("#remove-from-deck").click(function () {
        $.ajax({
            url: "/Session/RemoveCardFromSession",
            method: "POST",
            data: { cardId : parseInt($("#remove-from-deck").attr("data-id")) },
            success: function(response) {
                window.location.href = `${response.success.url}?${buildUri()}`;
            },
            error: function(error) {
                console.log("Error removing card from session: " + error.toString());
            }
        });
    });
});


function buildUri() {
    let uri = ``;

    $(".filter").each(function() {
        const id = $(this).attr("id");
        const value = $(this).val();

        uri += `${id}=${value}&`
    });
    
    return uri;
}