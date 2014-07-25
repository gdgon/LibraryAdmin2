//// PARENT:
// Add author button 
$("#btn-add-author").click(function (e) {
    e.preventDefault();
    var result;
    window.open("../Search/PopupAuthor", null, "width=550,height=600,left=300");
})

// Remove button
$("#bookauthor-form").on("click", ".btn-remove-author", function (e) {
    e.preventDefault();
    $(this).closest(".bookauthor-form-line").remove();
})

function AddAuthor(result) {
    if (AuthorNotYetAdded(result["id"])) {
        var line = '<div class="col-md-10 bookauthor-form-line" style="float: right; margin: 0 0 .5em 0">    <input class="authorId" name="authorId" type="hidden" value="' + result["id"] + '" />    <input readonly="readonly" type="text" style="margin: 0 .5em 0 0" value="' + result["firstName"] + " " + result["lastName"] + '" /><a class="btn-remove-author" href="#">Remove</a></div>'
        $("#bookauthor-form").append(line);
    }
}

function AuthorNotYetAdded(id) {
    var retval = true;
    var elements = $('input[name=authorId]');

    if (elements.length === 0)
        return retval;        
    else {
        console.log("In else");
        elements.each(function () {
            console.log("In each. id = " + id);
            if (this.value === id)
                retval = false;
        });
    }
    return retval;
}

//// POPUP
$(".btn-select").click(function (e) {
    e.preventDefault();
    var id = $(this).closest("tr").find(".id").text().trim()
    var firstName = $(this).closest("tr").find(".firstName").text().trim()
    var lastName = $(this).closest("tr").find(".lastName").text().trim()
    var result = new Array();
    result["id"] = id;
    result["firstName"] = firstName;
    result["lastName"] = lastName;
    window.opener.AddAuthor(result);
})