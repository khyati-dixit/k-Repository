function UpdateUser(id) {
    debugger;
    $.ajax({
        url: '/Index/Edit?Id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (data) {
            debugger;
            console.log(data);
            $("#AId0").val(data.UserId);
            $("#AName0").val(data.FullName);
            $("#ALat0").val(data.UserEmail);
            $("#AID0").val(data.CivilIdNumber);
            var sno = 0;
            var div = $('#DivcLicense0');
            div.empty();
            data.CarLicense.forEach(function (event) {
                div.append("<label>CarLicense" + (sno + 1) + "</label>" +
                    "<input class='form-control' id=" + event.Id + " name='cLicense0' type='text' value='" + event.CarLicenseValue + "' />");
                sno++;
            });
        }
    });
}


function UpdateUser1() {
    debugger;
    var UserId = $("#AId0").val();
    var userName = $("#AName0").val();
    var Email = $("#ALat0").val();
    var CivilIdNumber = $("#AID0").val();
    var param = [];
    var v = [];

    $('input[name="cLicense0"]').each(function () {
        // param.push(this.id, $(this).val());
        //param.push({ id: $(this).attr("id"), Values: $(this).val() });
        param.push(this.id);
        //param.push($)
    });

    param.forEach(function (value) {

        v.push({

            Id: value,
            CarLicenseValue: $("#" + value).val()
            
        });
        console.log(v);
    });
$.ajax({
        url: '/Index/Edit',
        type: 'POST',
    data: JSON.stringify({
        UserId: UserId,
            FullName: userName,
            UserEmail: Email,
           // Address: Address,
            CivilIdNumber: CivilIdNumber,
            CarDetails: v,
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
                //$('.modal').each(function () {
                //    $(this).modal('hide');
                //});
                setInterval('location.reload(true);', 5000);
                console.log('success');
            } else {
                console.log('failure');
            }
            
        }

    });
}
