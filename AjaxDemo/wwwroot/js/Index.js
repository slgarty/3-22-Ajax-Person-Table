$(() => {

    function fillTable() {
        $("tbody").empty();
        $.get('/home/getall', function (ppl) {
            ppl.forEach(p => {
                $("tbody").append(`
<tr>
    <td>${p.firstName}</td>
    <td>${p.lastName}</td>
    <td>${p.age}</td>
    <td>
        <button class='btn btn-warning edit' data-person-id='${p.id}' data-first-name='${p.firstName}' data-last-name='${p.lastName}' data-age='${p.age}'>Edit</button>
        <button class='btn btn-danger delete' data-person-id='${p.id}'>Delete</button>
    </td>
</tr>`);
            });
        });
    }
    function emptyModal() {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
    }
    fillTable();

    $("#add").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();

        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');

        $.post('/home/add', { firstName, lastName, age }, function (p) {
            fillTable();
        });

    });
    $(".table").on('click', '.delete', function () {
        const button = $(this);
        let id = button.data('person-id');
        $.post('/home/delete', { id }, function (p) {
            fillTable();
        });
    })

    $(".table").on('click', '.edit', function () {
        $(".modal").modal();
        const button = $(this);
        let id = button.data('person-id');
        $("#firstName").val(button.data('first-name'));
        $("#lastName").val(button.data('last-name'));
        console.log(button.data('age'));
        $("#personage").val(button.data('age'));
        $("#update").data('person-id', button.data('person-id'));
        $.post('/home/edit', { id }, function (p) {

        });
    })
    $("#update").on('click', function () {
        const id = $(this).data('person-id');
        const person = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            age: $("#age").val(),
            id
        };
        $.post('/home/update', person, function () {
            fillTable();
            $(".modal").modal('hide');
            emptyModal();
        });
    });
});


