$(document).ready(function () {

    $("#ContactListTable").DataTable({
        //"processing": true, // for show progress bar
        processing: true,
        "language": {
            "processing": $('#loaderMvcConatc').append("<img  src='../images/XtaS.gif' width='100px' height='60px' />"),  //add a loading image,simply putting tag.
        },
        searching: true,
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/MVCContacts/GetContactList",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
                { "data": "ContactsId", "name": "ContactsId", "autoWidth": true },
                { "data": "ContactName", "name": "ContactName", "autoWidth": true },
                { "data": "ContactAddress", "name": "ContactAddress", "autoWidth": true },
                { "data": "Type", "name": "Type", "autoWidth": true },
                { "data": "BillingPersonName", "name": "BillingPersonName", "autoWidth": true },
                { "data": "BillingCompanyName", "name": "BillingCompanyName", "autoWidth": true },
                { "data": "BillingVatTRN", "name": "BillingVatTRN", "autoWidth": true },
                { "data": "Status", "name": "Status", "autoWidth": true },
                {
                    render: function (data, type, row) {
                        return '<a href="" class="btn btn-info btn-sm" data-id="Id" onclick="ContactEdit(' + row.ContactsId + ')"><i class="fa fa-pencil"></i></a>|<a href="" class="btn btn-danger btn-xs" onclick="DeleteContact(' + row.ContactsId + ')"><i class="fa fa-trash"></i></a>|<a href="" class="btn btn-success btn-sm" onclick="Delete(' + row.ContactsId + ')"><i class="fa fa-eye"></i></a>'
                    }
                }          

        ]
    });
});



function ContactEdit(contactId) {
    window.location.href = "/MVCContacts/AddOrEdit/" + contactId;
}


function DeleteContact(id) {    
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: 'btn-danger',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: "No, cancel plx!",
        closeOnClickOutside: false,
        closeOnConfirm: false,
        closeOnCancel: false
    },
      function (isConfirm) {
          debugger;
          if (isConfirm) {
              window.location.href = "MVCContacts/Delete/" + id;
              swal("Deleted!", "Your contact file has been deleted!", "success");
          }
          else {
              swal("Cancelled", "Your contact file is safe :)", "error");
          }
      });
}

