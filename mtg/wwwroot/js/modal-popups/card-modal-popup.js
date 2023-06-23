$(document).ready(function () {
    $(".card").on("click", function (e) {
        console.log(e.target);
        
        populateCardPopup(e);
        $("#myModal").modal("show");
        
    });
    
    $("#myModal .close-popup").on("click", function (){
        $("#myModal").modal("hide");
    });
});

function populateCardPopup(e) {
    let cardTitle = e.target.alt;
    let cardType = e.target.dataset.type;

    $(".modal-title").html(`
        <span class="card-title">${cardTitle}</span><span class="card-type">${cardType}</span>
    `);
    $(".modal-body").html(`
        <img class="popup-image" src=${e.target.src} alt=${e.target.src}>
        <p class="popup-text">${e.target.dataset.text}</p>
        <p class="popup-flavor">${e.target.dataset.flavor}</p>
`);

    if ($("#add-to-deck").length > 0) {
        $("#add-to-deck").text("Add to deck");
        $("#add-to-deck").attr("data-id", e.target.dataset.id);
    } else {
        $("#remove-from-deck").text("Remove from deck");
        $("#remove-from-deck").attr("data-id", e.target.dataset.id);
    }
    
}