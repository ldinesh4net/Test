var table, mainForm, editForm, controls = [];
var type = '';

var Columns = [
 { data: 'Makeid', type: 'integer' },
 { data: 'Description', type: 'string' },
    { data: 'Active', type: 'bool' },

];
var resetForm = function () {
    controls.Makeid.val("");
    controls.Description.val("");
    controls.Active.val(true);

}
var reloadGrid = function () {

};


function setupForm() {
    $("#btnAdd").click(function () {
        resetForm();
        mainForm.hide();
        editForm.show();
        addbtn.hide();
    });

    $("#btnCancel").click(function () {
        mainForm.show();
        editForm.hide();
        addbtn.show();
    });

    $('.ui.checkbox').checkbox().first().checkbox({
        onChecked: function () {
            controls.Active.val(true);
        },
        onUnchecked: function () {
            controls.Active.val(false);
        }
    });
    //table.rows({ selected: true }).count()

    $("#btnSave").click(function () {
        var success = function (data) {
            if (data.Success) {
                table.ajax.reload();
                mainForm.show();
                editForm.hide();
            }
        };
        var url = '@Url.Action("Save", "Make")';
        var data = editForm.serialize();
        ajaxPost(url, data, success);
    });


}
function bindGrid() {
    var url = '@Url.Action("GetMakes", "Make")';
    table = $('#tbl_Make').DataTable(
            {
                "ajax": url,
                "columns": Columns,
                "columnDefs": [
                            {
                                "targets": [0],
                                "visible": false,
                                "searchable": false
                            }
                            ,
                             {
                                 "targets": [2],
                                 "data": "Active",
                                 "render": function (data, type, full, meta) {
                                     if (full['Active'] != true)
                                         return "Yes";
                                     else
                                         return "No";
                                     //if (full['Display'] != true)
                                     //    return '<input type="checkbox" disabled="disabled" />';
                                     //else
                                     //    return '<input type="checkbox" checked="checked" disabled="disabled" />';
                                 }
                             },
                            {
                                "targets": [3],
                                "data": "download_link",
                                "render": function (data, type, full, meta) {
                                    return '<a href="#" onclick="Edit(this)"  data-val="' + full['Makeid'] + '" ><i class="fa fa-pencil"></i></a> &nbsp &nbsp <a href="#" onclick="Delete(this)" data-val="' + full['Makeid'] + '"> <i class="ui-icon ace-icon fa fa-trash-o red"></i></a>';

                                }
                            }



                ],
                "select": true,
                "fnDrawCallback": function (oSettings) {
                    var $dt = $('#tbl_Make').footable({
                        breakpoints:
                        {
                            phone: 320,
                            tablet: 768
                        }
                    });
                    $dt.trigger('footable_resize');
                }
            }

            );




}

//$('#tbl_Make').on('click', 'a.editor_edit', function () {
//    e.preventDefault();
//    alert('hi');
//});

function Edit(e) {

    var Id = $(e).attr('data-val');

    var success = function (data) {
        addbtn.hide();
        var row = data.row;
        if (data.Success && row != null) {
            controls.Makeid.val(row.Id);
            controls.Description.val(row.Description);
            $('#cbActive').checkbox(row.Active ? 'check' : 'uncheck')
            mainForm.hide();
            editForm.show();

        }
    };
    var url = '@Url.Action("GetRowbyId", "Make")';
    var data = { Id: Id };
    ajaxPost(url, data, success);

};

function Delete(e) {

    var buttons = [];
    var okButton = $('<div>');
    okButton.addClass('ui green cancel inverted button');
    okButton.html('Yes');
    var cancelButton = $('<div>');
    cancelButton.addClass('ui red cancel inverted button');
    cancelButton.html('Cancel');
    buttons.push(cancelButton);
    buttons.push(okButton);
    CustomAlert("Alert", "Are you sure want to delete the record ?", buttons);
    okButton.click(function () {
        var success = function (data) {
            if (data.Success) {
                table.ajax.reload();
            }
            else {
                alert("Falied to delete");
            }
        };
        var Id = $(e).attr('data-val');
        var url = '@Url.Action("Delete", "Make")';
        var data = { Id: Id };
        ajaxPost(url, data, success);
    });
}

$(document).ready(function () {
    mainForm = $('#mainForm');
    editForm = $('#EditForm');
    addbtn = $('#btnAdd');
    controls.Makeid = editForm.find('#Makeid');
    controls.Description = editForm.find('#Description');
    controls.Active = editForm.find('#Active');

    bindGrid();
    setupForm();
    editForm.hide();
});