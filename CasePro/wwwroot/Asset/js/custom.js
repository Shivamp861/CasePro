

var globalactivityId;
var globalActivityType;
var handleBlurEvent = true;
$('.Jobcard input, .Jobcard textarea, .Jobcard button,.Jobcard select').on('blur', function () {
    Savedate();
});
$('.Installation, .loading, .Tipping').change(function () {
    var fileName = $(this).val();
    var ext = fileName.split('.').pop().toLowerCase();
    var errorMessageElement = $(this).closest('.form-group').find('.fileError');

    if ($.inArray(ext, ['jpg', 'jpeg', 'gif', 'img']) == -1) {
        errorMessageElement.html('Invalid file type. Only JPEG, JPG, GIF, or IMG files are allowed.');
        $(this).val('');
    } else {
        errorMessageElement.html('');
    }
});

var fileInputValueYard;
$('.yardTipping').on('blur', 'input, select,textarea,button', function () {

    var isValid = true;
    var id = globalactivityId
    if (id == "" || id == null || id == undefined || id == 0) {
        id = $('#activityid').val();
    }

    if ($(this).is('#fileInputyard')) {
        fileInputValueYard = $('#fileInputyard')[0].files[0];
    }

    var formData = new FormData();
    formData.append('SiteImage', $('#fileInputyard')[0].files[0]);

    var Siteformdata = {
        Startandfinishtime: $('#yardbstartfinishTime').val(),
        LiftingEquipmentUsed: $('#yardblifting').val(),
        ChainLiftingequipmenttobeused: $('#yardchainlifteq').val(),
        IncidentReporting: $('#yardIncidentreporting').val(),
        AnyNearMissOccurrences: $('#txtyardAnymiss').val(),
        BarrierConditionChecks: $('#txtbarriercondition').val(),
        AllRelevantActivityRams: $('#txtAllrelevant').val(),
        ActivityId: id || 0,
        Id: $('#id').val(),
        // Add other form fields here
    };

    formData.append('Sitedata', Siteformdata);


    $.each(Siteformdata, function (key, value) {
        // Check if the value is empty or null
        if (!value) {
            // Set error message based on the field
            switch (key) {
                case 'Startandfinishtime':
                    $('#bstartfinishTimeError').text('Start & finish Time is required.');
                    break;
                case 'LiftingEquipmentUsed':
                    $('#bliftingError').text('Lifting equipment used is required.');
                    break;
                case 'ChainLiftingequipmenttobeused':
                    $('#lifteqError').text('Chain / lifting equipment to be used is required.');
                    break;
                case 'IncidentReporting':
                    $('#yardIncidentreportingError').text('Incident reporting is required.');
                    break;
                case 'AnyNearMissOccurrences':
                    $('#txtAnymissError').text('Any Near Miss / N/C / I/O occurrences is required.');
                    break;
                case 'AllRelevantActivityRams':
                    $('#txtAllrelevantError').text('Barrier condition checks/suitability/damage is required.');
                    break;
                case 'BarrierStartAndFinishLocation':
                    $('#txtbarrierconditionError').text('Barrier start and finish location is required.');
                    break;

                // Add cases for other fields
                default:
                    break;
            }
            isValid = false; // Set isValid to false
        } else {
            // Clear error message if the field is not empty
            switch (key) {
                case 'Startandfinishtime':
                    $('#bstartfinishTimeError').text('');
                    break;
                case 'LiftingEquipmentUsed':
                    $('#bliftingError').text('');
                    break;
                case 'ChainLiftingequipmenttobeused':
                    $('#lifteqError').text('');
                    break;
                case 'IncidentReporting':
                    $('#yardIncidentreportingError').text('');
                    break;
                case 'AnyNearMissOccurrences':
                    $('#txtAnymissError ').text('');
                    break;
                case 'AllRelevantActivityRams':
                    $('#txtAllrelevantError').text('');
                    break;
                case 'BarrierStartAndFinishLocation':
                    $('#txtbarrierconditionError').text('');
                    break;

                // Add cases for other fields
                default:
                    break;
            }
        }
    });

    if (!fileInputValueYard) {
        $('#yardfileInputyardError').text('Site/Layout drawings/site images is required.');
        isValid = false;
    } else {
        $('#yardfileInputyardError').text('');
    }

    // Move AJAX call inside the isValid block
    if (isValid) {
        $.each(Siteformdata, function (key, value) {
            formData.append('Sitedata.' + key, value);
        });

        $.ajax({
            url: '/activity/SaveDataactivitydetails',
            method: 'post',
            processData: false, // Prevent jQuery from processing the data
            contentType: false, // Prevent jQuery from setting contentType
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.errorMessage); // or response.message if you want to display a message
                    window.location.href = '/Activity/CreateActivity/' + response.activityId;
                }
            },
            error: function (xhr, status, error) {
                console.error('error saving data:', error);
            }
        });
    }




});




$(document).on('blur', '#myTable1 input[type="text"], #myTable1 select', function () {
    
    if (!handleBlurEvent) {
        return; // Exit the event handler if handleBlurEvent is false
    }

    var isValid = true;
    var $currentRow = $(this).closest('tr');
    var aid = $('#activityid').val(); // Assuming you have an element with id="activityid" to get the ActivityId
    if (aid == null || aid == '') {
        alert("Please fill basic details");
        return;
    }

    $currentRow.find('.text-danger').empty();

    // Get data from the current row
    var shift = $currentRow.find('select[name="Shift"]').val();
    var date = $currentRow.find('input[name="Date"]').val();
    var summaryOfWorks = $currentRow.find('input[name="SummaryOfWorks"]').val();
    var pid = $currentRow.find('#pid').val();

    // Validation for shift
    if (!shift.trim()) {
        isValid = false;
        $currentRow.find('#ShiftError').text('Shift is required');
    }

    // Validation for date
    if (!date.trim()) {
        isValid = false;
        $currentRow.find('#DateError').text('Date is required');
    }

    // Validation for summary of works
    if (!summaryOfWorks.trim()) {
        isValid = false;
        $currentRow.find('#SummaryError').text('Summary of Works is required');
    }

    // Proceed only if the data is valid
    if (isValid) {
        var requestData = {
            aid: aid,
            shift: shift,
            date: date,
            summaryOfWorks: summaryOfWorks,
            pid: pid
        };

        // Perform AJAX request to save data
        $.ajax({
            url: '/activity/ProductData',
            method: 'POST',
            data: requestData,
            success: function (response) {
                alert("Product Data Saved Successfully"); // Display success message
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error('Error saving data:', error);
            }
        });
    }
});

$(document).on('blur', '#myTable input[type="text"], #myTable select', function () {
    
    var $currentRow = $(this).closest('tr');

    var isValid = true;
    var aid = $('#activityid').val(); // Assuming you have an element with id="activityid" to get the ActivityId
    if (aid == null || aid == '') {
        alert("Please fill basic details");
        return;
    }
    
    // Perform validation for each input field in the row
    $currentRow.find('input[type="text"], select').each(function () {
        var value = $(this).val().trim();
        if (!value) {
            isValid = false;
            $(this).addClass('error'); // Add a class to highlight the invalid input
        } else {
            $(this).removeClass('error');
        }
    });

    // Proceed only if all input fields are valid
    if (isValid) {
        var requestData = {
            resourcetype: $currentRow.find('#type').val(),
            shift: $currentRow.find('#Shift').val(),
            daynight: $currentRow.find('#daynight').val(),
            name: $currentRow.find('#name').val(),
            comment: $currentRow.find('#comment').val(),
            rid: $currentRow.find('#rid').val(),
            aid: aid
        };

        // Perform AJAX request to save the data
        $.ajax({
            url: '/activity/ResourseData',
            method: 'POST',
            data: requestData,
            success: function (response) {
                alert("Resourse Data Saved Successfully")
            },
            error: function (xhr, status, error) {
                console.error('Error saving data:', error);
            }
        });
    }
});

// Function to add a new row to the table

function addRowformanage(button) {
    debugger;
    var table = document.getElementById("manageTable");
    var currentRow = button.parentNode.parentNode; // Get the current row
    var inputs = currentRow.querySelectorAll('.form-control');
    var errorSpans = currentRow.querySelectorAll('.text-danger');
    var canAddRow = true;

    inputs.forEach(function (input, index) {
        if (input.value.trim() === '') {
            canAddRow = false;
            if (errorSpans[index]) {
                errorSpans[index].innerText = "Please fill out this field.";
            } else {
                var errorSpan = document.createElement('span');
                errorSpan.classList.add('text-danger');
                errorSpan.innerText = "Please fill out this field.";
                input.parentNode.appendChild(errorSpan);
            }
        } else {
            if (errorSpans[index]) {
                errorSpans[index].innerText = "";
            }
        }
    });

    if (canAddRow) {
        var newRow = table.insertRow(-1); // Insert row at the top (reversed)
        var cell1 = newRow.insertCell(0);
        var cell2 = newRow.insertCell(1);
        var cell3 = newRow.insertCell(2);
        var cell4 = newRow.insertCell(3);

        cell1.innerHTML = '<input type="date" class="form-control" id="Completeion1Date1">';
        cell2.innerHTML = '<input type="text" class="form-control" id="Signature1">';
        cell3.innerHTML = '<input type="text" class="form-control" id="Name1">';
        cell4.innerHTML = '<input type="date" class="form-control" id="Date1">' + '<input type="text" hidden="" id="InstructorId" value="1013">';

        // Reapply validation logic to the new row inputs
        var newInputs = newRow.querySelectorAll('.form-control');
        newInputs.forEach(function (input) {
            input.addEventListener('blur', function () {
                // Your validation logic here
            });
        });
    }
}

 
function addRow(button) {
    
    var table = document.getElementById("myTable");
    var currentRow = button.parentNode.parentNode; // Get the current row
    var inputs = currentRow.querySelectorAll('.form-control');
    var errorSpans = currentRow.querySelectorAll('.text-danger');
    var canAddRow = true;

    inputs.forEach(function (input, index) {
        if (input.value.trim() === '') {
            canAddRow = false;
            if (errorSpans[index]) {
                errorSpans[index].innerText = "Please fill out this field.";
            } else {
                var errorSpan = document.createElement('span');
                errorSpan.classList.add('text-danger');
                errorSpan.innerText = "Please fill out this field.";
                input.parentNode.appendChild(errorSpan);
            }
        } else {
            if (errorSpans[index]) {
                errorSpans[index].innerText = "";
            }
        }
    });

    // Check if any dropdown is not filled out
    var dropdowns = currentRow.querySelectorAll('select');
    dropdowns.forEach(function (dropdown) {
        if (dropdown.value.trim() === '') {
            canAddRow = false;
            var errorSpan = document.createElement('span');
            errorSpan.classList.add('text-danger');
            errorSpan.innerText = "Please select an option.";
            dropdown.parentNode.appendChild(errorSpan);
        }
    });

    if (canAddRow) {
        var newRow = table.insertRow(-1); // Insert row at the top (reversed)
        var cell1 = newRow.insertCell(0);
        var cell2 = newRow.insertCell(1);
        var cell3 = newRow.insertCell(2);
        var cell4 = newRow.insertCell(3);
        var cell5 = newRow.insertCell(4);
        var cell6 = newRow.insertCell(5);

        cell1.innerHTML = '<select class="form-control" id="type">' +
            '<option>Supervisor</option>' +
            '<option>Operative</option>' +
            '<option>Labour Man</option>' +
            '<option>HGV</option>' +
            '<option>HGV+Crane</option>' +
            '<option>Trailer</option>' +
            '<option>Loader</option>' +
            '<option>Work Van</option>' +
            '</select>';
        cell2.innerHTML = '<select class="form-control" id="Shift">' +
            '<option>Shift 1</option>' +
            '<option>Shift 2</option>' +
            '<option>Shift 3</option>' +
            '<option>Shift 4</option>' +
            '<option>Shift 5</option>' +
            '<option>Shift 6</option>' +
            '<option>Shift 7</option>' +
            '<option>Shift 8</option>' +
            '</select>';
        cell3.innerHTML = '<select class="form-control" id="daynight">' +
            '<option>Day</option>' +
            '<option>Night</option>' +
            '</select>';
        cell4.innerHTML = '<input value="" type="text" class="form-control" id="name">';
        cell5.innerHTML = '<input type="text" class="form-control" width="500" id="comment">';
        cell6.innerHTML = '<input value="0" type="text" id="rid" style="display: none;">' + '<input value="' + aid + '" type="text" id="activityid" style="display: none;" /> ' ;
    }

}
function addRow1(button) {
   

    var table = document.getElementById("myTable1");
    var currentRow = button.parentNode.parentNode; // Get the current row
    var inputs = currentRow.querySelectorAll('.form-control');
    var errorSpans = currentRow.querySelectorAll('.text-danger');
    var canAddRow = true;

    inputs.forEach(function (input, index) {
        if (input.value.trim() === '') {
            canAddRow = false;
            if (errorSpans[index]) {
                errorSpans[index].innerText = "Please fill out this field.";
            } else {
                var errorSpan = document.createElement('span');
                errorSpan.classList.add('text-danger');
                errorSpan.innerText = "Please fill out this field.";
                input.parentNode.appendChild(errorSpan);
            }
        } else {
            if (errorSpans[index]) {
                errorSpans[index].innerText = "";
            }
        }
    });

    // Check if any dropdown is not filled out
    var dropdowns = currentRow.querySelectorAll('select');
    dropdowns.forEach(function (dropdown) {
        if (dropdown.value.trim() === '') {
            canAddRow = false;
            var errorSpan = document.createElement('span');
            errorSpan.classList.add('text-danger');
            errorSpan.innerText = "Please select an option.";
            dropdown.parentNode.appendChild(errorSpan);
        }
    });

    if (canAddRow) {

        var newRow = table.insertRow(-1); // Insert row at the top (reversed)
        var cell1 = newRow.insertCell(0);
        var cell2 = newRow.insertCell(1);
        var cell3 = newRow.insertCell(2);
        var cell4 = newRow.insertCell(3);



        cell1.innerHTML = '<select class="form-control" name="Shift" id="Shift">' +
            '<option>Shift 1</option>' +
            '<option>Shift 2</option>' +
            '<option>Shift 3</option>' +
            '<option>Shift 4</option>' +
            '<option>Shift 5</option>' +
            '<option>Shift 6</option>' +
            '<option>Shift 7</option>' +
            '<option>Shift 8</option>' +
            '</select>';
        cell2.innerHTML = '<input value="" type="date" class="form-control" name="Date" id="Date" style="width: 150px;">';
        cell3.innerHTML = '<input value="" type="text" class="form-control" name="SummaryOfWorks" id="SummaryOfWorks">';

        cell4.innerHTML = '<input value="0" type="text" id="pid" style="display: none;" />' + '<input value="' + aid + '" type="text" id="activityid" style="display: none;" /> ';

    }

}



//ADD sign of and update has submit
function addsignoff(button) {
    debugger;
    var actid = $('#id').val(); // Assuming you have an element with id="id" to get the actid

    var signOffData = [];

    // Get all table rows except the header row
    var rows = $('#manageTable tbody tr');

    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var CompetionDate = $(row).find('input[type="date"]').eq(0).val();
        var Signature = $(row).find('input[type="text"]').eq(0).val();
        var PrintName = $(row).find('input[type="text"]').eq(1).val();
        var SignOffDate = $(row).find('input[type="date"]').eq(1).val();
        var InstructorId = $(row).find('#InstructorId').val();

        // Check if any field is blank or null
        if (CompetionDate && Signature && PrintName && SignOffDate && InstructorId) {
            var rowData = {
                CompetionDate: CompetionDate,
                Signature: Signature,
                PrintName: PrintName,
                SignOffDate: SignOffDate,
                InstructorId: InstructorId,
                ActivityId: actid
            };
            signOffData.push(rowData);
        }
    }

    if (signOffData.length > 0) {
        $.ajax({
            type: 'POST',
            url: '/Activity/SaveSignOff',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(signOffData),
            datatype: 'json',
            success: function (response) {
                console.log(response);

                alert('Manager Sign Off Saved');
                window.location.reload();

            },
            error: function (xhr, status, error) {
                // Handle error response from the server
                console.error(xhr.responseText);
            }
        });
    }
    else {
        alert("Please Fill atleast One row!");
    }


}


$('.siteInstallation').on('blur', 'input, select,textarea,button', function () {
    var id = globalactivityId;
    var fileInputValue;
    if (id == "" || id == null || id == undefined || id == 0) {
        id = $('#activityid').val();
    }
    var isValid = true;
    if ($(this).is('#fileInput')) {
        fileInputValue = $('#fileInput')[0].files[0];
    }
    var Siteformdata = {
        MeetingSite: $('#sitemeetingDate').val(),
        LabourSupplier: $('#sitelabourSupplier').val(),
        SupplierContact: $('#sitesupplierContact').val(),
        NoOfPersoneSupplied: $('#Numberofpersons').val(),
        BarrierType: $('#btype').val(),
        BarrierQty: $('#bquantity').val(),
        BarrierStartAndFinishLocation: $('#blocations').val(),
        BarrierPerformance: $('#bPerformance').val(),
        LengthOfRuns: $('#blength').val(),
        AnchoringDetails: $('#anchordetails').val(),
        Isapermittobreakgroundrequired: $('#bpermitreq').val(),
        ChainLiftingequipmenttobeused: $('#bpermitreqchain').val(),
        IncidentReporting: $('#Incidentreporting').val(),
        OtherResourcesEquipmentUsed: $('#txtresources').val(),
        AllRelevantActivityRams: $('#txtrelevantactivity').val(),
        AnySpecialInstructions: $('#txtspecialinstruction').val(),
        ActivityId: id,
        Id: $('#id').val(),

        // Add other form fields here
    };

    var formData = new FormData();
    formData.append('SiteImage', $('#fileInput')[0].files[0]);
    formData.append('Sitedata', Siteformdata);

    $.each(Siteformdata, function (key, value) {
        // Check if the value is empty or null
        if (!value) {
            // Set error message based on the field
            switch (key) {
                case 'MeetingSite':
                    $('#sitemeetingError').text('Meeting Site is required.');
                    break;
                case 'LabourSupplier':
                    $('#sitelabourSupplierError').text('Labour Supplier is required.');
                    break;
                case 'SupplierContact':
                    $('#sitesupplierError').text('Supplier Contact is required.');
                    break;
                case 'NoOfPersoneSupplied':
                    $('#numofpersonserror').text('Number of Persons Supplied is required.');
                    break;
                case 'BarrierType':
                    $('#btypeerror').text('Barrier Type is required.');
                    break;
                case 'BarrierQty':
                    $('#qtyBarriersError').text('Barrier Qty is required.');
                    break;
                case 'BarrierStartAndFinishLocation':
                    $('#blocationError').text('Barrier start and finish location is required.');
                    break;
                case 'BarrierPerformance':
                    $('#bperformanceError').text('Barrier Performance is required.');
                    break;
                case 'LengthOfRuns':
                    $('#blengthError').text('Length of runs is required.');
                    break;
                case 'AnchoringDetails':
                    $('#anchodetailsError').text('Anchoring details is required.');
                    break;
                case 'Isapermittobreakgroundrequired':
                    $('#permitreqError').text('Is a permit to break ground required is required.');
                    break;
                case 'ChainLiftingequipmenttobeused':
                    $('#permitreqchainError').text('Chain / lifting equipment to be used is required.');
                    break;
                case 'IncidentReporting':
                    $('#IncidentreportingError').text('Incident reporting is required.');
                    break;
                case 'OtherResourcesEquipmentUsed':
                    $('#txtresourcesError').text('Other resources / equipment used is required.');
                    break;
                case 'AllRelevantActivityRams':
                    $('#txtrelevantactivityError').text('All relevant activity RAMS, lift plans and SSOW is required.');
                    break;
                case 'AnySpecialInstructions':
                    $('#txtspecialinstructionError').text('Any special instructions / site restrictions / specific PPE is required');
                    break;
                // Add cases for other fields
                default:
                    break;
            }
            isValid = false; // Set isValid to false
        } else {
            // Clear error message if the field is not empty
            switch (key) {
                case 'MeetingSite':
                    $('#sitemeetingError').text('');
                    break;
                case 'LabourSupplier':
                    $('#sitelabourSupplierError').text('');
                    break;
                case 'SupplierContact':
                    $('#sitesupplierError').text('');
                    break;
                case 'NoOfPersoneSupplied':
                    $('#numofpersonserror').text('');
                    break;
                case 'BarrierType':
                    $('#btypeerror ').text('');
                    break;
                case 'BarrierQty':
                    $('#qtyBarriersError').text('');
                    break;
                case 'BarrierStartAndFinishLocation':
                    $('#blocationError').text('');
                    break;
                case 'BarrierPerformance':
                    $('#bperformanceError').text('');
                    break;
                case 'LengthOfRuns':
                    $('#blengthError').text('');
                    break;
                case 'AnchoringDetails':
                    $('#anchodetailsError').text('');
                    break;
                case 'Isapermittobreakgroundrequired':
                    $('#permitreqError').text('');
                    break;
                case 'ChainLiftingequipmenttobeused':
                    $('#permitreqchainError').text('');
                    break;
                case 'IncidentReporting':
                    $('#IncidentreportingError').text('');
                    break;
                case 'OtherResourcesEquipmentUsed':
                    $('#txtresourcesError').text('');
                    break;
                case 'AllRelevantActivityRams':
                    $('#txtrelevantactivityError').text('');
                    break;
                case 'AnySpecialInstructions':
                    $('#txtspecialinstructionError').text('');
                    break;
                // Add cases for other fields
                default:
                    break;
            }
        }
    });

    if (!fileInputValue) {
        $('#fileInputError').text('Site/Layout drawings/site images is required.');
        isValid = false;
    } else {
        $('#fileInputError').text('');
    }

    if (isValid) {
        $.each(Siteformdata, function (key, value) {
            formData.append('Sitedata.' + key, value);
        });

        $.ajax({
            url: '/activity/SaveDataactivitydetails',
            method: 'post',
            processData: false, // Prevent jQuery from processing the data
            contentType: false, // Prevent jQuery from setting contentType
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.errorMessage); // or response.message if you want to display a message
                    window.location.href = '/Activity/CreateActivity/' + response.activityId;
                }
            },
            error: function (xhr, status, error) {
                console.error('error saving data:', error);
            }
        });
    }

});


//for Login toster
document.addEventListener('DOMContentLoaded', function () {
    // Find the error message element
    var errorMessageElement = document.getElementById('errorMessage');

    // If the element exists
    if (errorMessageElement) {
        // Set a timeout to hide the element after 3 seconds
        setTimeout(function () {
            errorMessageElement.style.display = 'none';
        }, 3000); // 3000 milliseconds = 3 seconds
    }
});
//for add save and Update Product data


//for add row save and Update Resource data


//for save customer data
$(document).on('blur', '#content1 .panel-default.customer input:not(.add-customer), #content1 .panel-default.customer textarea:not(.add-customer)', function () {

    if (!handleBlurEvent) {
        return; // Exit the event handler if handleBlurEvent is false
    }


    var isValid = true;
    var $currentRow = $(this).closest('.original-row');
    // Assuming you have an element with id="id" to get the ActivityId
    var activityId = globalactivityId;
    if (activityId == null) {
        activityId = $('#id').val();
    }
    if (activityId == '') {
        alert("Please fill basic details")
    }

    // Get data from the current row
    var customerName = $currentRow.find('.customer-name').val();
    var contactNo = $currentRow.find('.contact-no').val();
    var Custid = $currentRow.find('#custid').val();
    $currentRow.find('.text-danger').empty();

    // Validation for customerName
    if (!customerName.trim()) {
        isValid = false;
        // Display error message for customerName
        $currentRow.find('.customer-name').siblings('.text-danger').text('Customer Name is required');
    }

    // Validation for contactNo
    if (!contactNo.trim()) {
        isValid = false;
        // Display error message for contactNo
        $currentRow.find('.contact-no').siblings('.text-danger').text('Contact Number is required');
    }
    if (!contactNo.trim() || isNaN(contactNo) || contactNo.length !== 10 || !/^\d{10}$/.test(contactNo)) {
        isValid = false;
        // Display error message for contactNo
        $currentRow.find('.contact-no').siblings('.text-danger').text('Contact Number must be a 10-digit number');
    }

    // Proceed only if the data is valid
    if (isValid) {
        var requestData = {
            ActivityId: activityId,
            custid: Custid,
            CustomerName: customerName,
            ContactNo: contactNo
        };

        // Proceed with AJAX request to save the data
        //$.ajax({

        //    url: '/activity/Customersavedata',
        //    method: 'POST',
        //    data: requestData,
        //    success: function (response) {

        //        // Handle success response
        //        if (response.success) {
        //            document.getElementById("custid").value = response.activityId;
        //            $currentRow.find('input').prop('disabled', true);
        //            $('#addcustomer').prop('disabled', false);
        //            // Display success message
        //            alert(response.errorMessage); // or response.message if you want to display a message
        //        }
        //    },
        //    error: function (xhr, status, error) {
        //        // Handle error response
        //        console.error('Error saving data:', error);
        //    }
        //});
        $('#addcustomer').on('click', function () {
            // Perform AJAX request to save data
            $.ajax({
                url: '/activity/Customersavedata',
                method: 'POST',
                data: requestData,
                success: function (response) {
                    // Handle success response
                    if (response.success) {
                        document.getElementById("custid").value = response.activityId;
                        $('.original-row input').prop('disabled', false);
                        $('#addcustomer').prop('disabled', false); // Disable button after successful submission
                        // Display success message
                        alert(response.errorMessage); // or response.message if you want to display a message
                    }
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    console.error('Error saving data:', error);
                }
            });
        });



    }
});
//for add customer row
//$(document).on('click', ".add-customer", function () {
//    handleBlurEvent = false;
//    // Validate all existing rows before adding a new one
//    var isValid = true;
//    $('.original-row').each(function () {
//        var $row = $(this);
//        var customerName = $row.find('.customer-name').val();
//        var contactNo = $row.find('.contact-no').val();

//        // Check if any required field is empty
//        if (!customerName.trim() || !contactNo.trim()) {
//            isValid = false;
//            return false; // Exit the loop early if any row is invalid
//        }
//    });

//    if (isValid) {
//        // Proceed to add a new row
//        var $originalRow = $(this).closest('.original-row');
//        var $clonedRow = $originalRow.clone();

//        // Clear the custid value for the cloned row
//        $clonedRow.find('#custid').val('0');
//        // Update IDs in the cloned row with unique indices
//        var rowCount = $('.original-row').length + 1;
//        $clonedRow.find('.customer-name').attr('id', 'CustomerName_' + rowCount);
//        $clonedRow.find('.contact-no').attr('id', 'contactno_' + rowCount);

//        // Add hidden class to the custid input field in the cloned row
//        $clonedRow.find('.custid').addClass('hidden');

//        // Append error spans below the cloned row
//        $clonedRow.find('.customer-name').after('<span class="text-danger validation-error" id="customerNameError_' + rowCount + '"></span>');
//        $clonedRow.find('.contact-no').after('<span class="text-danger validation-error" id="contactNoError_' + rowCount + '"></span>');

//        $clonedRow.find('.customer-name, .contact-no').removeAttr('disabled');
//        $clonedRow.find('.add-customer').removeAttr('disabled');

//        rowCount++;

//        $originalRow.after($clonedRow);
//        $clonedRow.find('.customer-name').val('');
//        $clonedRow.find('.contact-no').val('');
//        //$originalRow.find('.add-customer').prop('disabled', true);
//        handleBlurEvent = true;
//    } else {
//        alert("Please fill in all required fields in the existing rows before adding a new one.");
//    }
//});
$(document).on('click', ".add-customer", function () {
    handleBlurEvent = false;
    // Validate all existing rows before adding a new one
    var isValid = true;
    $('.original-row').each(function () {
        var $row = $(this);
        var customerName = $row.find('.customer-name').val();
        var contactNo = $row.find('.contact-no').val();

        // Check if any required field is empty
        if (!customerName.trim() || !contactNo.trim()) {
            isValid = false;
            return false; // Exit the loop early if any row is invalid
        }
    });

    if (isValid) {
        // Proceed to add a new row
        var $originalRow = $(this).closest('.original-row');
        var $clonedRow = $originalRow.clone();

        // Clear the custid value for the cloned row
        $clonedRow.find('#custid').val('0');
        // Update IDs in the cloned row with unique indices
        var rowCount = $('.original-row').length + 1;
        $clonedRow.find('.customer-name').attr('id', 'CustomerName_' + rowCount);
        $clonedRow.find('.contact-no').attr('id', 'contactno_' + rowCount);

        // Add hidden class to the custid input field in the cloned row
        $clonedRow.find('.custid').addClass('hidden');

        // Append error spans below the cloned row
        $clonedRow.find('.customer-name').after('<span class="text-danger validation-error" id="customerNameError_' + rowCount + '"></span>');
        $clonedRow.find('.contact-no').after('<span class="text-danger validation-error" id="contactNoError_' + rowCount + '"></span>');

        // Enable fields for the new row
        $clonedRow.find('.customer-name, .contact-no').removeAttr('disabled');

        rowCount++;

        $originalRow.after($clonedRow);
        $clonedRow.find('.customer-name').val('');
        $clonedRow.find('.contact-no').val('');
        //$originalRow.find('.add-customer').prop('disabled', true);
        handleBlurEvent = true;
    } else {
        alert("Please fill in all required fields in the existing rows before adding a new one.");
    }
});

function Savedate() {
    var isValid = true;

    if (!$('#Customer').val()) {
        $('#customerOrderNumberError').text('Customer Order Number is required.');
        isValid = false;
    } else {
        $('#customerOrderNumberError').text('');
    }

    // Validation for SAGE Order Number
    if (!$('#SAGEOrder').val()) {
        $('#sageOrderNumberError').text('SAGE Order Number is required.');
        isValid = false;
    } else {
        $('#sageOrderNumberError').text('');
    }

    // Validation for HC Job Number
    if (!$('#HCJob').val()) {
        $('#hcorderNumberError').text('HC Job Number is required.');
        isValid = false;
    } else {
        $('#hcorderNumberError').text('');
    }

    // Validation for activityType
    if (!$('#activityType').val()) {
        $('#activityTypeError').text('Activity Type is required.');
        isValid = false;
    } else {
        $('#activityTypeError').text('');
    }

    if (!$('#Date').val()) {
        $('#DateError').text('Date is required.');
        isValid = false;
    } else {
        $('#DateError').text('');
    }

    if (!$('#RaisedBy').val()) {
        $('#RaiseError').text('RaiseBy is required.');
        isValid = false;
    } else {
        $('#RaiseError').text('');
    }

    if (!$('#SiteAddress').val()) {
        $('#SiteAddressError').text('SiteAddress is required.');
        isValid = false;
    } else {
        $('#SiteAddressError').text('');
    }

    var outOfHours = $('#OutofHours').val();
    if (!outOfHours) {
        $('#OutofHoursError').text('Emergency Contact is required.');
        isValid = false;
    } else if (!/^\d{10}$/.test(outOfHours)) {
        $('#OutofHoursError').text('Emergency Contact must be a 10-digit number.');
        isValid = false;
    } else {
        $('#OutofHoursError').text('');
    }

    if (!$('#Nearest').val()) {
        $('#NearestError').text('Nearest is required.');
        isValid = false;
    } else {
        $('#NearestError').text('');
    }

    if (isValid) {
        var formdata = {
            ActivityId: $('#id').val() || 0,
            CustomerOrderNumber: $('#Customer').val(),
            SageOrderNumber: $('#SAGEOrder').val(),
            HcorderNumber: $('#HCJob').val(),
            ActivityType: $('#activityType').val(),
            DateRaised: $('#Date').val(),
            RaisedBy: $('#RaisedBy').val(),
            SiteAddress: $('#SiteAddress').val(),
            OutofhoursEmrgContact: $('#OutofHours').val(),
            NearestAE: $('#Nearest').val(),

        };

        $.ajax({

            url: '/activity/Savejobcard',
            method: 'post',
            data: formdata,
            success: function (response) {
                if (response.success) {
                    document.getElementById("id").value = response.activityId;
                    globalactivityId = response.activityId;
                    globalActivityType = response.activityType;
                    window.location.href = '/Activity/CreateActivity/' + response.activityId;
                    alert(response.errorMessage);
                }
            },
            error: function (xhr, status, error) {

                console.error('error saving data:', error);
            }
        });

    }
}

function saveTrailerdata() {
    var isValid = true;
    var id = globalactivityId;
    var fileInputValue;
    if (id == "" || id == null || id == undefined || id == 0) {
        id = $('#activityid').val();
    }

    var tippingData = {
        Date: $('#yardbarrierdateloaded').val(),
        TrailerSupplier: $('#yardtsupplier').val(),
        TrailerNumber: $('#yardTrailerNumber').val(),
        Quantity: $('#yardunit').val(),
        VehicleReg: $('#yardVehicleReg').val(),
        LoadDepot: $('#yardTrailerdeport').val(),
        LoadedTippedBy: $('#yardloadedby').val(),
        Activityid: id
    };

    $.each(tippingData, function (key, value) {
        // Check if the value is empty or null
        if (!value) {
            // Set error message based on the field
            switch (key) {
                case 'Date':
                    $('#yardbarrierdateloadedError').text('Tipping date is required.');
                    break;
                case 'TrailerSupplier':
                    $('#yardtsupplierError').text('Trailer supplier is required.');
                    break;
                case 'TrailerNumber':
                    $('#yardTrailerNumberError').text('Trailer number is required.');
                    break;
                case 'Quantity':
                    $('#yardunitError').text('Unit is required.');
                    break;
                case 'VehicleReg':
                    $('#yardVehicleRegError').text('Vehicle Reg is required.');
                    break;
                case 'LoadDepot':
                    $('#yardTrailerdeportError').text('Trailer depot is required.');
                    break;
                case 'LoadedTippedBy':
                    $('#yardloadedbyError').text('Tipped by is required.');
                    break;

                // Add cases for other fields
                default:
                    break;
            }
            isValid = false; // Set isValid to false
        } else {
            // Clear error message if the field is not empty
            switch (key) {
                case 'Date':
                    $('#yardbarrierdateloadedError').text('');
                    break;
                case 'TrailerSupplier':
                    $('#yardtsupplierError').text('');
                    break;
                case 'TrailerNumber':
                    $('#yardTrailerNumberError').text('');
                    break;
                case 'Quantity':
                    $('#yardunitError').text('');
                    break;
                case 'VehicleReg':
                    $('#yardVehicleRegError').text('');
                    break;
                case 'LoadDepot':
                    $('#yardTrailerdeportError').text('');
                    break;
                case 'LoadedTippedBy':
                    $('#yardloadedbyError').text('');
                    break;

                // Add cases for other fields
                default:
                    break;
            }
        }
    });
    if (isValid) {
        $.ajax({
            url: '/activity/SaveTrailerTipping',
            type: 'POST',
            data: tippingData,
            success: function (response) {
                if (response != null) {
                    var rowCount = $('#yardTTrailerDetails tbody tr').length;
                    // Increment the last used id for the next row
                    var nextId = rowCount + 1;
                    var newRow = '<tr>' +
                        '<td>' + nextId + '</td>' +
                        '<td>' + response.trailerSupplier + '</td>' +
                        '<td>' + response.trailerNumber + '</td>' +
                        '<td>' + response.quantity + '</td>' +
                        '<td>' + response.loadDepot + '</td>' +
                        '<td>' + response.loadedTippedBy + '</td>' +
                        '</tr>';
                    $('#yardTTrailerDetails tbody').append(newRow);

                    alert('Trailer tipping details saved successfully.');

                    $('#yardbarrierdateloaded').val('');
                    $('#yardtsupplier').val('');
                    $('#yardTrailerNumber').val('');
                    $('#yardunit').val('');
                    $('#yardVehicleReg').val('');
                    $('#yardTrailerdeport').val('');
                    $('#yardloadedby').val('');
                } else {
                    alert('Failed to save tipping details. Please try again.');
                }
            },
            error: function () {
                alert('An error occurred while saving tipping details. Please try again.');
            }
        });
    }

}

function SaveInBoundTrailerData() {
    var isValid = true;
    var id = globalactivityId;
    var fileInputValue;
    if (id == "" || id == null || id == undefined || id == 0) {
        id = $('#activityid').val();
    }

    var isOutBound = $('input[id="isOutbound"]:checked').val();
    if (isOutBound == undefined || isOutBound == '') {
        isOutBound = 'off';
    } else {
        isOutBound = $('input[id="isOutbound"]:checked').val();
    }

    var loadingData = {
        Date: $('#barrierdate').val(),
        TrailerSupplier: $('#Trailersupplier').val(),
        TrailerNumber: $('#TrailerSupNum').val(),
        LoadPositioned: $('#LoadPositioned').val(),
        Quantity: $('#UnitLoad').val(),
        VehicleReg: $('#Vehicle').val(),
        DepartFrom: $('#TrailerDepart').val(),
        LoadDepot: $('#TrailerDepartTo').val(),
        LoadedTippedBy: $('#Loaded').val(),
        IsOutBound: isOutBound,
        Activityid: id
    };
    $.each(loadingData, function (key, value) {
        // Check if the value is empty or null
        if (!value) {
            // Set error message based on the field
            switch (key) {
                case 'Date':
                    $('#BarrierDate').text('Barrier date is required.');
                    break;
                case 'TrailerSupplier':
                    $('#TSupllier').text('Trailer supplier is required.');
                    break;
                case 'TrailerNumber':
                    $('#TrailerNumber').text('Trailer number is required.');
                    break;
                case 'LoadPositioned':
                    $('#LoadPos').text('Trailer number is required.');
                    break;
                case 'Quantity':
                    $('#UnitLoaded').text('Unit is required.');
                    break;
                case 'VehicleReg':
                    $('#Vehicalreg').text('Vehicle Reg is required.');
                    break;
                case 'DepartFrom':
                    $('#TrailerDepartFrom').text('Trailer Depart Form is required');
                    break;
                case 'LoadDepot':
                    $('#Trailerdepart').text('Trailer depot is required.');
                    break;
                case 'LoadedTippedBy':
                    $('#LoadTrailer').text('Loaded by is required.');
                    break;

                // Add cases for other fields
                default:
                    break;
            }
            isValid = false; // Set isValid to false
        } else {
            // Clear error message if the field is not empty
            switch (key) {
                case 'Date':
                    $('#BarrierDate').text('');
                    break;
                case 'TrailerSupplier':
                    $('#TSupllier').text('');
                    break;
                case 'TrailerNumber':
                    $('#TrailerNumber').text('');
                    break;
                case 'LoadPositioned':
                    $('#LoadPos').text('');
                    break;
                case 'Quantity':
                    $('#UnitLoaded').text('');
                    break;
                case 'VehicleReg':
                    $('#Vehicalreg').text('');
                    break;
                case 'DepartFrom':
                    $('#TrailerDepartFrom').text('');
                    break;
                case 'LoadDepot':
                    $('#Trailerdepart').text('');
                    break;
                case 'LoadedTippedBy':
                    $('#LoadTrailer').text('');
                    break;

                // Add cases for other fields
                default:
                    break;
            }
        }
    });

    if (isValid) {
        $.ajax({
            url: '/activity/SaveTrailerTipping',
            type: 'POST',
            data: loadingData,
            success: function (response) {
                if (response != null) {
                    //if (response.isOutBound == 'on') {
                    //    var rowCount = $('#TrailerDetails1 tbody tr').length;
                    //    var nextId = rowCount + 1;
                    //    var newRow = '<tr>' +
                    //        '<td>' + nextId + '</td>' +
                    //        '<td>' + response.trailerSupplier + '</td>' +
                    //        '<td>' + response.trailerNumber + '</td>' +
                    //        '<td>' + response.quantity + '</td>' +
                    //        '<td>' + response.loadDepot + '</td>' +
                    //        '<td>' + response.departFrom + '</td>' +
                    //        '<td>' + response.date + '</td>' +
                    //        '<td>' + response.loadedTippedBy + '</td>' +
                    //        '</tr>';
                    //    $('#TrailerDetails1 tbody').append(newRow);
                    //    alert('Trailer Details Out Bound Saved successfully.');
                    //}
                    if (response.isOutBound == 'on') {
                        var rowCount = $('#TrailerDetails1 tbody tr').length;
                        var nextId = rowCount + 1;

                        // Extracting the date part from response.date and formatting it
                        var datePart = response.date.split('T')[0];

                        // Appending a new row with the date part only
                        var newRow = '<tr>' +
                            '<td>' + nextId + '</td>' +
                            '<td>' + response.trailerSupplier + '</td>' +
                            '<td>' + response.trailerNumber + '</td>' +
                            '<td>' + response.quantity + '</td>' +
                            '<td>' + response.loadDepot + '</td>' +
                            '<td>' + response.departFrom + '</td>' +
                            '<td>' + datePart + '</td>' + // Displaying only the date part
                            '<td>' + response.loadedTippedBy + '</td>' +
                            '</tr>';

                        // Appending the new row to the table body
                        $('#TrailerDetails1 tbody').append(newRow);

                        alert('Trailer Details Out Bound Saved successfully.');
                    }

                  
                    else {
                        var rowcount2 = $('#TrailerDetails2 tbody tr').length;
                        var nextId1 = rowcount2 + 1;
                        var datePart = response.date.split('T')[0];
                        var newRow1 = '<tr>' +
                            '<td>' + nextId1 + '</td>' +
                            '<td>' + response.trailerSupplier + '</td>' +
                            '<td>' + response.trailerNumber + '</td>' +
                            '<td>' + response.quantity + '</td>' +
                            '<td>' + response.loadDepot + '</td>' +
                            '<td>' + datePart + '</td>' +
                            '</tr>';
                        $('#TrailerDetails2 tbody').append(newRow1);
                        alert('Tripping Details Return Saved successfully.')
                    }

                   

                    // Clear input fields
                    $('#Trailersupplier').val('');
                    $('#TrailerSupNum').val('');
                    $('#UnitLoad').val('');
                    $('#TrailerDepart').val('');
                    $('#TrailerDepartTo').val('');
                    $('#Loaded').val('');
                    $('#Vehicle').val('');
                    $('#barrierdate').val('');
                } else {
                    alert('Failed to save tipping details. Please try again.');
                }
            },
            error: function () {
                alert('An error occurred while saving tipping details. Please try again.');
            }
        });
    }
}

$('.yardLoading').on('blur', 'input, select,textarea,button', function () {

    var isValid = true;

    // Check if the changed element is an input or select within the .siteInstallation panel

    var id = globalactivityId;
    var fileInputYardL;
    if (id == "" || id == null || id == undefined || id == 0) {
        id = $('#activityid').val();
    }

    if ($(this).is('#fileInputloding')) {
        fileInputYardL = $('#fileInputloding')[0].files[0];

    }

    var formData = new FormData();
    formData.append('SiteImage', $('#fileInputloding')[0].files[0]);

    var Yardformdata = {
        Startandfinishtime: $('#bstartfinishTime').val(),
        LiftingEquipmentUsed: $('#blifting').val(),
        ChainLiftingequipmenttobeused: $('#chainlifteq').val(),
        IncidentReporting: $('#LIncireporting').val(),
        AnyNearMissOccurrences: $('#txtanymissoccure').val(),
        BarrierConditionChecks: $('#conditions').val(),
        AllRelevantActivityRams: $('#Rams').val(),
        ActivityId: id,
        Id: $('#id').val(),

        // Add other form fields here
    };

    formData.append('Sitedata', Yardformdata);


    $.each(Yardformdata, function (key, value) {
        // Check if the value is empty or null
        if (!value) {
            // Set error message based on the field
            switch (key) {
                case 'Startandfinishtime':
                    $('#starttime').text('Start & finish Time is required.');
                    break;
                case 'LiftingEquipmentUsed':
                    $('#liftingeqp').text('Lifting equipment used is required.');
                    break;
                case 'ChainLiftingequipmenttobeused':
                    $('#chainliftingL').text('Chain / lifting equipment to be used is required.');
                    break;
                case 'IncidentReporting':
                    $('#increporting').text('Incident reporting is required.');
                    break;
                case 'AnyNearMissOccurrences':
                    $('#txtanymissoccureError').text('Any Near Miss / N/C / I/O occurrences is required.');
                    break;
                case 'BarrierConditionChecks':
                    $('#barriercon').text('Barrier condition checks / suitability / damage is required.');
                    break;
                case 'AllRelevantActivityRams':
                    $('#allrelavant').text('All relevant activity RAMS, lift plans and SSOW is required.');
                // Add cases for other fields
                default:
                    break;
            }
            isValid = false; // Set isValid to false
        } else {
            // Clear error message if the field is not empty
            switch (key) {
                case 'Startandfinishtime':
                    $('#starttime').text('');
                    break;
                case 'LiftingEquipmentUsed':
                    $('#liftingeqp').text('');
                    break;
                case 'ChainLiftingequipmenttobeused':
                    $('#chainliftingL').text('');
                    break;
                case 'IncidentReporting':
                    $('#increporting').text('');
                    break;
                case 'AnyNearMissOccurrences':
                    $('#txtanymissoccureError ').text('');
                    break;
                case 'BarrierConditionChecks':
                    $('#barriercon').text('');
                    break;
                case 'AllRelevantActivityRams':
                    $('#allrelavant').text('');
                    break;
                default:
                    break;
            }
        }
    });

    if (!fileInputYardL) {
        $('#fileInputlError').text('Site/Layout drawings/site images is required.');
        isValid = false;
    } else {
        $('#fileInputlError').text('');
    }

    if (isValid) {
        $.each(Yardformdata, function (key, value) {
            formData.append('Sitedata.' + key, value);
        });
        $.ajax({
            url: '/Activity/SaveDataactivitydetails',
            method: 'post',
            processData: false, // Prevent jQuery from processing the data
            contentType: false, // Prevent jQuery from setting contentType
            data: formData,
            success: function (response) {
                if (response.success) {
                    //alert('Data saved successfully.');
                    alert(response.errorMessage); // or response.message if you want to display a message
                    window.location.href = '/Activity/CreateActivity/' + response.activityId;
                }
            },
            error: function (xhr, status, error) {
                console.error('error saving data:', error);
            }
        });
    }


});




