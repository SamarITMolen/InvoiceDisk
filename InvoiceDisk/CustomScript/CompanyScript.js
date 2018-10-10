

//loading data to datatable
$(document).ready(function () {
    $("#CompanyListTable").DataTable({
        // "processing": true, // for show progress bar
        processing: true,
        "language": {
            "processing": $('#loader').append("<img src='../images/XtaS.gif' width='100px' height='60px' />"),  //add a loading image,simply putting tag.
        },
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once

        searching: true,
        "ajax": {
            "url": "/Company/Index1",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
                { "data": "CompanyId", "name": "CompanyId", "autoWidth": true },
                { "data": "ComapanyName", "name": "ComapanyName", "autoWidth": true },
                { "data": "CompanyAddress", "name": "CompanyAddress", "autoWidth": true },
                { "data": "CompanyPhone", "name": "CompanyPhone", "autoWidth": true },
                { "data": "CompanyCell", "name": "CompanyCell", "autoWidth": true },
                { "data": "CompanyEmail", "name": "CompanyEmail", "autoWidth": true },
               { "data": "CompanyTRN", "name": "CompanyTRN", "autoWidth": true },
               {
                   'data': 'CompanyLogo',
                   render: function (logo) {
                       if (logo) {
                           return '<img height="25px" width="25px" src="' + logo + '" />'
                       }
                       else { return 'No Image'; }
                   }
               },
              {
                  render: function (data, type, row) {
                      return '<a href="#" class="btn btn-info btn-sm" data-id="Id" onclick="Edit(' + row.CompanyId + ')"><i class="fa fa-pencil"></i></a>|<a href="#" class="btn btn-danger btn-sm" onclick="DeleteCompany(' + row.CompanyId + ')"><i class="fa fa-trash"></i></a>|<a href="#" class="btn btn-success btn-sm" onclick="Delete(' + row.CompanyId + ')"><i class="fa fa-eye"></i></a>'
                  }
              }

        ]
    });
});


//Company edit function

function Edit(companyId) {
    window.location.href = "AddOrEdit/" + companyId;
}


//company Delete Function 
function DeleteCompany(id) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: 'btn-danger',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: "No, cancel plx!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
      function (isConfirm) {
          if (isConfirm) {
              window.location.href = "Company/Delete/" + id;
              swal("Deleted!", "Your imaginary file has been deleted!", "success");
          }
          else {
              swal("Cancelled", "Your imaginary file is safe :)", "error");
          }
      });


}
