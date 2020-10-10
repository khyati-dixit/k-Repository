
function InsertUser() {
    debugger;
    var userName = $("#AName").val();
    var Email = $("#ALat").val();
    var Address = $("#ALong").val();
    //var image = $("#image").val();
    ///  get var values 
    var CivilIdNumber = $("#AID").val();
    var imagenBase64 = $("#pImageBase64").html();
    var carLicense = $("#cLicense").val()
    //var url = "@Url.Action("InsertuS", "Index")" 
    $.ajax({
        url: '/Index/InsertuS',
        type: 'POST',
        data: JSON.stringify({
            FullName: userName,
            UserEmail: Email,
            Address: Address,
            CivilIdNumber: CivilIdNumber,
            ProfilePic: imagenBase64,
            CarLicense: carLicense
        }),
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        success: function (data) {
            window.reload();
            console.log(data.success);
            if (data.success === 1) {
                // loop through all modal's and call the Bootstrap
                // modal jQuery extension's 'hide' method
                $('.modal').each(function () {
                    $(this).modal('hide');
                });
                console.log('success');
            } else {
                console.log('failure');
            }
        }
            
    });
}
function encodeImagetoBase64(element) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        $("#image").attr("src", reader.result);
        $("#pImageBase64").html(reader.result);

    }
    reader.readAsDataURL(file);
}

function DeleteUser(Id) {
    debugger;
    var result = confirm('Are you sure you wish to delete this record?');
    if (result) {
        $.ajax({
            url: '/Index/Delete?id=' + Id,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (data) {
                window.reload();
                //$("#addUser").hide();
                //$("#addUser").addClass('hide');
            }

        });
    }

}