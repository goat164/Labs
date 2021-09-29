const uri = "https://localhost:44354/api/People";
let persons = null;
function getCount(data) {
    const el = $("#counter");
    let name = "to-do";
    if (data) {
        if (data > 1) {
            name = "to-dos";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}
$(document).ready(function () {
    getData();
});
function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#persons");
            $(tBody).empty();
            getCount(data.length);
            let i = 1;
            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(`Pozycja ${i}`))
                    .append($("<td></td>").text(`${item.name}`))
                    .append($("<td></td>").text(item.surname))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                );
                i++;
                tr.appendTo(tBody);
            });
            persons = data;
        }
    });
}
function addItem() {
    const item = {
        name: $("#add-name").val(),
        surname: $("#add-surname").val()
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/CreatePerson',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
            $("#add-surname").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(persons, function (key, item) {
        if (item.id === id) {
            $("#edit-name").val(item.name);
            $("#edit-surname").val(item.surname);
            $("#edit-id").val(item.id);
        }
    });
    $("#spoiler").css({ display: "block" });
}

function updatePerson() {
    var id = parseInt($("#edit-id").val(), 10);
    const item = {
        id: id,
        name: $("#edit-name").val(),
        surname: $("#edit-surname").val(),
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/UpdatePerson',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            closeInput();
        }
    });
}

function closeInput() {
    $("#spoiler").css({ display: "none" });
}