
let list = "";
let table2;
    $(document).ready(function () {
        const departments = $('#dataTableDepartment');
        
        $.ajax({
        type: 'GET',
                url: 'api/department',
            headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
                dataType: 'Json',
                success: function (data) {
                    let content = "<tbody>";
                    $.each(data,
                        function (index, val) {

            content += '<tr>' +
            "<td id=' id" + val.id + "'>" + val.id + "</td>" +
                "<td id='nameD" + val.id + "' contenteditable = 'false'>" + val.name + '</td>' +
                "<th style ='float: center;'><span >" +
                "<button type = 'button' id= 'cancelD" + val.id + "' style='float:right; display:none' class = 'btn btn-link js-edit' data-department-id='" + val.id + "'>Cancel</button>" +
                "<button type = 'button' id = 'saveD" + val.id + "' style='float:right;display:none' class = 'btn btn-link js-save' data-department-id='" + val.id + "'>Save</button>" +
                "<button  type = 'button'  id = 'delD" + val.id + "' style='float:right; display:'';' class = 'btn btn-link js-delete' data-department-id='" + val.id + "'>Delete</button>" +
                "<button type='button' id ='editD" + val.id + "' class = 'btn btn-link js-edit' data-department-id='" + val.id + "' style='float:right; display:'';'>Edit</button></span></th>" +
            '</tr>';
                            list += " <option id='" + val.id + "'>" + val.name + "</option>";
                        });
                    content += " </tbody>";
                    departments.append(content);
                    $("#list").append(list);

                    table2 = $('#dataTableDepartment').DataTable();
                }

            });
            //delete function for department table 
            $("#dataTableDepartment").on("click", ".js-delete", function () {

                const button = $(this);
                bootbox.confirm("Are you sure you want to delete this department", function (result) {

                    if (result) {
        $.ajax({
            url: "/api/department/" + button.attr("data-department-id"),
            method: "DELETE",
            headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
            success: function () {
                table2.row(button.parents("tr")).remove().draw();
            }
        });
                    }
                });
            });
           
        });

        $(document).ready(function () {
        $('#addD').on("click", function (e) {
            e.preventDefault();
            const id = $('#idD').val();
            const name = $('#nameD').val();

            $(".error").remove();

            if (id < 1) {
                $('#idD').after('<span class="error">This field is required</span>');
                return;
            }
            if (name.length < 1) {
                $('#nameD').after('<span class="error">This field is required</span>');
                return;
            }
            $.ajax({
                type: "POST",
                headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
                url: "api/department/AddDepartment",
                data: {
                    id: id,
                    name: name
                }, success:
                    location.reload()


        });
        });
        $("#dataTableDepartment").on("click",
            ".js-edit",

            function () {
                const button = $(this);
                const id = button.attr("data-department-id");
                const save = document.getElementById("saveD" + id);
                const del = document.getElementById("delD" + id);
                const edit = document.getElementById('editD' + id);
                const cancel = document.getElementById('cancelD' + id);
                const name = document.getElementById('nameD' + id);
                const compare = del.style.display;
                if (compare !== "none") {
                    edit.style.display = "none";
                    console.log(edit);
                    del.style.display = "none";
                    save.style.display = "";
                    cancel.style.display = "";
                    name.contentEditable = 'true';
                } else {
                    console.log("else");
                    edit.style.display = "";
                    del.style.display = "";
                    cancel.style.display = "none";
                    save.style.display = "none";
                    name.contentEditable = 'false';
                }
            });

        $("#dataTableDepartment").on("click", ".js-save", function () {
            const button = $(this);
            const tr = button.parents("tr");


            if (tr.find("td:eq(1)").html().length === 0) {
        bootbox.confirm("Please Enter Correct name", function() {});
        return;
    }

    bootbox.confirm("Are you sure you want to edit this department?", function (result) {
        if (result) {
            $.ajax({
                url: "/api/department/" + button.attr("data-department-id"),
                method: "PUT",
                headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
                dataType: "JSON",
                data: {
                    name: tr.find("td:eq(1)").html().trim()
                },
                success: function () {
                    location.reload();
                }
            });
        }
    });

            const id = button.attr("data-department-id");
            const save = document.getElementById("saveD" + id);
            const del = document.getElementById("delD" + id);
            const edit = document.getElementById('editD' + id);
            const cancel = document.getElementById('cancelD' + id);
            const name = document.getElementById('nameD' + id);
            const compare = del.style.display;
            if (compare !== "none") {
        edit.style.display = "none";
        del.style.display = "none";
        save.style.display = "";
        cancel.style.display = "";
        name.contentEditable = 'true';
        //button.parents("tr").style = "color:black";
    } else {
        edit.style.display = "";
        del.style.display = "";
        cancel.style.display = "none";
        save.style.display = "none";
        name.contentEditable = 'false';
    }
        });
});
//////////////////////////////////////////////////////////////////////////////////
    let table;
    $(document).ready(function () {
            console.log(sessionStorage.getItem("token"));
            const employees = $('#dataTableEmployee');
            $.ajax({
        type: 'GET',
                url: 'api/employee',
                headers: {Authorization: "Bearer "+sessionStorage.getItem("token") },
                success: function (data) {
                    let content = "<tbody>";
                    $.each(data,
                        function (index, val) {
                            content += "<tr id ='tr" + val.id + "'>" +
            '<td id =\'id\'  contenteditable = \'false\'>' + val.id + '</td>' +
            "<td id = 'name" + val.id + "' contenteditable = 'false'>" + val.name + "</td>" +
            "<td id = 'date" + val.id + "' contenteditable = 'false'>" + val.date_of_hiring.substring(0, 10) + "</td>" +
            "<td  id = 'sex" + val.id + "' contenteditable = 'false'>" + val.sex + "</td>" +
                                "<td id = 'dep" + val.id + "' contenteditable = \'false\'> " + val.department1.name + "</td><th style ='float: center'><span>" +
            "<button id= 'cancel" + val.id + "' style='float: right; display:none' class = 'btn btn-link js-edit' data-employee-id='" + val.id + "'>Cancel</button>" +
            "<button  type='button' id = 'save" + val.id + "' style='float:right;display:none' class = 'btn btn-link js-save' data-employee-id='" + val.id + "' style='position: relative; right: 0;'>Save</button>" +
            "<button  type='button' id = 'del" + val.id + "'  class = 'btn btn-link js-delete' data-employee-id='" + val.id + "' style='float: right;'>Delete</button>" +
            "<button  type='button' id ='edit" + val.id + "' class = 'btn btn-link js-edit' data-employee-id='" + val.id + "' style='float: right;'>Edit</button></span></th></tr>"; });
                    content += " </tbody>";
                    employees.append(content);
                    table = $('#dataTableEmployee').DataTable();
                }
            });
            $("#dataTableEmployee").on("click", ".js-delete", function () {
                const button = $(this);
                bootbox.confirm("Are you sure you want to delete this employee?", function (result) {

                    if (result) {
        $.ajax({
            url: "/api/employee/" + button.attr("data-employee-id"),
           
            method: "DELETE",
            headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
            success: function () {
                table.row(button.parents("tr")).remove().draw();
            }
        });
                    }
                });
            });
            $("#dataTableEmployee").on("click", ".js-save", function () {
                const button = $(this);
                const tr = button.parents("tr");

                console.log(tr.find("td:eq(2)").html());
                console.log();
                if (!(new RegExp(/^(\d{4})(\/|-)(\d{1,2})(\/|-)(\d{1,2})$/).test(tr.find("td:eq(2)").html().trim()))) {
        bootbox.confirm("Please Enter Correct Date, A Correct Date Format is {YYYY-MM-DD}", function () { });
                    return;
                }
                if (tr.find("td:eq(3)").html().toLowerCase().trim() !== "male" && (tr.find("td:eq(3)").html().toLowerCase().trim()) !== "female") {
        bootbox.confirm("Please Enter Male or Female ", function () { });
                    return;

                }

                if ($("#list option:contains(" + tr.find("td:eq(4)").html().trim() + ")").length === 0) {
        bootbox.confirm("Please Enter an existing Department ", function () { });
                    return;
                }

                bootbox.confirm("Are you sure you want to edit this employee?", function (result) {
                    if (result) {
        $.ajax({
            url: "/api/employee/" + button.attr("data-employee-id"),
            method: "PUT",
            headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
            dataType: "JSON",
            data: {
                name: tr.find("td:eq(1)").html(),
                date_of_hiring: tr.find("td:eq(2)").html(),
                sex: tr.find("td:eq(3)").html(),
                departmentName: tr.find("td:eq(4)").html().trim()
            },
            success: function () {
                location.reload();
            }
        });
                    }
                });

               
                const id = button.attr("data-employee-id");
                const save = document.getElementById("save" + id);
                const del = document.getElementById("del" + id);
                const edit = document.getElementById('edit' + id);
                const cancel = document.getElementById('cancel' + id);
                const name = document.getElementById('name' + id);
                const date = document.getElementById('date' + id);
                const dep = document.getElementById('dep' + id);
                const sex = document.getElementById('sex' + id);
                const compare = del.style.display;
                if (compare !== "none") {
                edit.style.display = "none";
                    del.style.display = "none";
                    save.style.display = "";
                    cancel.style.display = "";
                    name.contentEditable = 'true';
                    date.contentEditable = 'true';
                    dep.contentEditable = 'true';
                    sex.contentEditable = 'true';
                } else {
        edit.style.display = "";
                    del.style.display = "";
                    cancel.style.display = "none";
                    save.style.display = "none";
                    name.contentEditable = 'false';
                    date.contentEditable = 'false';
                    dep.contentEditable = 'false';
                    sex.contentEditable = 'false';
                }
            });
            $("#dataTableEmployee").on("click",
                ".js-edit",

                function () {

                    const button = $(this);
                    const id = button.attr("data-employee-id");
                    const save = document.getElementById("save" + id);
                    const del = document.getElementById("del" + id);
                    const edit = document.getElementById('edit' + id);
                    const cancel = document.getElementById('cancel' + id);
                    const name = document.getElementById('name' + id);
                    const date = document.getElementById('date' + id);
                    const dep = document.getElementById('dep' + id);
                    const sex = document.getElementById('sex' + id);
                    const compare = del.style.display;
                    if (compare !== "none") {
                         edit.style.display = "none";
                        del.style.display = "none";
                        save.style.display = "";
                        cancel.style.display = "";
                        name.contentEditable = 'true';
                        date.contentEditable = 'true';
                        dep.contentEditable = 'true';
                        sex.contentEditable = 'true';
                        button.parents("tr").bgColor = "black";
                    } else {
                        edit.style.display = "";
                        del.style.display = "";
                        cancel.style.display = "none";
                        save.style.display = "none";
                        name.contentEditable = 'false';
                        date.contentEditable = 'false';
                        dep.contentEditable = 'false';
                        sex.contentEditable = 'false';
                    }
                });
        $('#addE').on("click", function (e) {
            e.preventDefault();
            const id = $('#id').val();
            const name = $('#name').val();
            const gender = $('#gender').children(":selected").attr("id");
            const date = $('#date').val();
            const dep = $("#list").children(":selected").attr("id");

            $(".error").remove();

            if (id < 1) {
                $('#id').after('<span class="error">This field is required</span>');
                return;
            }
            if (name.length < 1) {
                $('#name').after('<span class="error">This field is required</span>');
                return;
            }
            if (date.length < 8) {
                $('#date').after('<span class="error">Please Enter Date Of Hiring</span>');
                return;
            }
            if (gender === "0") {
                $('#gender').after('<span class="error">This field is required</span>');
                return;
            }
            if (dep === "none") {
                $('#list').after('<span class="error">This field is required</span>');
                return;
            }
            $.ajax({
                type: "POST",
                url: "api/employee/AddEmployees",
                headers: { Authorization: "Bearer " + sessionStorage.getItem("token") },
                data: {
                    id: id,
                    name: name,
                    date_of_hiring: date,
                    sex: gender === "1" ? "Male" : "Female",
                    department: dep
                },
                success:
                    location.reload()

            });
           
        });
        });
