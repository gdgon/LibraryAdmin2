//// PARENT:
// Add author button 
$("#btn-add-author").click(function (e) {
    e.preventDefault();
    var line = $(".bookauthor-form-line").first();
    $("#bookauthor-form").append(line.clone());
})

// Remove button
$("#bookauthor-form").on("click", ".btn-remove-author", function (e) {
    e.preventDefault();
    if ($(".bookauthor-form-line").length > 1)
        $(this).closest(".bookauthor-form-line").remove();
})