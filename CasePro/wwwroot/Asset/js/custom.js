

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

    if ($.inArray(ext, ['jpg', 'jpeg', 'gif', 'img', 'png']) == -1) {
        errorMessageElement.html('Invalid file type. Only JPEG, JPG, GIF, or IMG files are allowed.');
        $(this).val('');
    } else {
        errorMessageElement.html('');
    }
});



var fileInputValueYard;

$('.yardTipping').on('blur', 'input, select,textarea,button', function (e) {

    const target = e.target || e.srcElement;
    
    // Check if the target element or any of its ancestors is the file input
    if (target.id === 'fileInputyard' || $(target).closest('#fileInputyard').length > 0 || target.id === 'yardbarrierdateloaded' || target.id === 'yardtsupplier' || target.id === 'yardTrailerNumber' || target.id === 'yardunit' || target.id === 'yardVehicleReg' || target.id === 'yardTrailerdeport' || target.id === 'yardloadedby' || target.id === 'saveButton') {
        return; // Do nothing if the file input or its child elements triggered the event
    }

    var actid = globalactivityId;
    //var fileInputValue;
    if (actid == "" || actid == null || actid == undefined || actid == 0) {
        actid = $('#activityid').val();

        var filesInput = document.getElementById('fileInputyard');
        if (filesInput && filesInput.files && filesInput.files.length > 0) {
            fileInputValue = filesInput.files;
        }
        else {

        }
    }
    var isValid = true;
    //if ($(this).is('#fileInput')) {
    //    fileInputValue = $('#fileInput')[0].files[0];
    //}
    var Siteformdata = {
        Startandfinishtime: $('#yardbstartfinishTime').val(),
        LiftingEquipmentUsed: $('#yardblifting').val(),
        ChainLiftingequipmenttobeused: $('#yardchainlifteq').val(),
        IncidentReporting: $('#yardIncidentreporting').val(),
        AnyNearMissOccurrences: $('#txtyardAnymiss').val(),
        BarrierConditionChecks: $('#txtbarriercondition').val(),
        AllRelevantActivityRams: $('#txtAllrelevant').val(),
        ActivityId: actid || 0,
        Id: $('.ytid').val(),
        // Add other form fields here
    };

    // Append other form data fields to the formData
    var formData = new FormData();
    // Append all files selected to the formData

    if (filesInput.files.length > 0) {
        for (var i = 0; i < filesInput.files.length; i++) {
            formData.append('SiteImages', filesInput.files[i]);
        }
    }
    //$.each($('#fileInput')[0].files, function (index, file) {
    //    formData.append('SiteImages', file);
    //});


    // Append other form data fields to the formData
    $.each(Siteformdata, function (key, value) {
        formData.append('Sitedata.' + key, value);
    });

    $.each(Siteformdata, function (key, value) {
        if (!value) {
            // Set error message based on the field
            switch (key) {
                case 'Startandfinishtime':
                    $('#bstartfinishTimeError').text('Start & finish Time is required.');
                    break;
                case 'LiftingEquipmentUsed':
                    $('#bliftingError').text('Lifting equipment used is required.');
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
                // Add cases for other fields
                default:
                    break;
            }
        }
    });

    if (filesInput.files.length == 0) {
        var IsimageCount = document.querySelectorAll('#UploadPath');
        if (IsimageCount.length > 0) {
            $('#yardfileInputyardError').text('');
        }
        else {
            $('#yardfileInputyardError').text('Site/Layout drawings/site images is required.');
            isValid = false;
        }
    } else {
        $('#yardfileInputyardError').text('');
    }

    // Move AJAX call inside the isValid block
    if (isValid) {
        $.ajax({
            url: '/activity/SaveDataactivitydetails',
            method: 'post',
            processData: false, // Prevent jQuery from processing the data
            contentType: false, // Prevent jQuery from setting contentType
            data: formData,
            success: function (response) {
                if (response.success) {
                    if ($('.ytid').val() != 0) {
                        $('#sitesuccessupdatemessage').fadeIn().delay(2000).fadeOut();
                        
                    } else {
                        $('#sitesuccessmessage').fadeIn().delay(2000).fadeOut();
                    }
                }
            },
            error: function (xhr, status, error) {
                $('#siteerrormessage').fadeIn().delay(2000).fadeOut();
            }
        });
    }
});




$(document).ready(function () {
    var isDropdownOrTextboxFocused = false;

    $('#myTable1').on('focus', 'input[type="text"], select', function () {
        isDropdownOrTextboxFocused = true;
    });

    $('#myTable1').on('blur', 'input[type="text"], select', function () {
        var $currentRow = $(this).closest('tr');
        var aid = $('#activityid').val();

        if (!aid) {
            alert("Please fill basic details");
            return;
        }

        clearValidationErrors($currentRow);

        var isValid = validateInputFields($currentRow);

        if (isValid && !isDropdownOrTextboxFocused) {
            var requestData = {
                aid: aid,
                shift: $currentRow.find('select[name="Shift"]').val(),
                date: $currentRow.find('input[name="Date"]').val(),
                summaryOfWorks: $currentRow.find('input[name="SummaryOfWorks"]').val(),
                pid: $currentRow.find('#pid').val()
            };

            $.ajax({
                url: '/activity/ProductData',
                method: 'POST',
                data: requestData,
                success: function (response) {
                    if (response.success) {
                        if (response.pid > 0) {

                            $('#Productupdatemessage').fadeIn().delay(2000).fadeOut();
                        } else {
                            $('#Productsuccessmessage').fadeIn().delay(2000).fadeOut();
                        }
                    }
                },
                error: function (xhr, status, error) {
                    $('#Producterrormessage').fadeIn().delay(2000).fadeOut();
                }
            });
        }
    });

    $('#myTable1').on('click', function () {
        isDropdownOrTextboxFocused = false;
        $('.text-danger').empty();
    });

    function clearValidationErrors($row) {
        $row.find('.text-danger').empty();
    }

    function validateInputFields($row) {
        var isValid = true;

        $row.find('input[name="SummaryOfWorks"], input[name="Date"] ,select').each(function () {
            var $input = $(this);
            var fieldName = $input.attr('name');
            var fieldValue = $input.val();

            if (!fieldValue.trim()) {
                isValid = false;
                $row.find('#' + fieldName + 'Error').text(fieldName + ' is required');
            }
        });

        return isValid;
    }
});




$(document).ready(function () {
    var isDropdownOrTextboxFocused = false;

    $('#myTable').on('focus', 'input[type="text"], select', function () {
        isDropdownOrTextboxFocused = true;
    });

    $('#myTable').on('blur', ' input[type="text"], select', function () {
        var $currentRow = $(this).closest('tr');
        var aid = $('#activityid').val();

        if (!aid) {
            alert("Please fill basic details");
            return;
        }

        clearValidationErrors($currentRow);

        var isValid = validateInputFields($currentRow);

        if (isValid && !isDropdownOrTextboxFocused) {
            var requestData = {
                resourcetype: $currentRow.find('select[name="ResourceType"]').val(),
                shift: $currentRow.find('select[name="Shift"]').val(),
                daynight: $currentRow.find('select[name="DayNight"]').val(),
                name: $currentRow.find('input[name="Name"]').val(),
                comment: $currentRow.find('input[name="Comments"]').val(),
                rid: $currentRow.find('#rid').val(),
                aid: aid
            };

            // Perform AJAX request to save the data
            $.ajax({
                url: '/activity/ResourseData',
                method: 'POST',
                data: requestData,
                success: function (response) {
                    if (response.success) {
                        if (response.rid > 0) {

                            $('#resourseupdatemessage').fadeIn().delay(2000).fadeOut();
                        } else {
                            $('#resoursesuccessmessage').fadeIn().delay(2000).fadeOut();
                        }
                    }
                },
                error: function (xhr, status, error) {
                    $('#resourseerrormessage').fadeIn().delay(2000).fadeOut();
                }
            });
        }

    });


    $('#myTable').on('click', function () {
        isDropdownOrTextboxFocused = false;
        $('.text-danger').empty();
    });

    function clearValidationErrors($row) {
        $row.find('.text-danger').empty();
    }

    function validateInputFields($row) {
        var isValid = true;

        $row.find('select ,input[name="Name"], input[name="Comments"]').each(function () {
            var $input = $(this);
            var fieldName = $input.attr('name');
            var fieldValue = $input.val();

            if (!fieldValue.trim()) {
                isValid = false;
                $row.find('#' + fieldName ).text(fieldName + ' is required');
            }
        });

        return isValid;
    }


})


// Function to add a new row to the table

function addRowformanage(button) {
   
    var table = document.getElementById("manageTable");
    var currentRow = button.closest('table').rows;
    var latestRow = currentRow[currentRow.length - 1];
    var inputs = latestRow.querySelectorAll('.form-control');
    var errorSpans = latestRow.querySelectorAll('.text-danger');
    var canAddRow = true;
    var instruid = $('#InstructorId').val();

    inputs.forEach(function (input, index) {
        var errorSpan = input.parentNode.querySelector('.text-danger');

        if (input.value.trim() === '') {
            // Input is empty, show error message
            if (!errorSpan) {
                // Create error span if it doesn't exist
                errorSpan = document.createElement('span');
                errorSpan.classList.add('text-danger');
                input.parentNode.appendChild(errorSpan);
            }
            // Set error message text
            errorSpan.innerText = "Please fill out this field.";
            canAddRow = false; // Set flag to prevent further action

        } else {
            // Input is not empty, remove error message if exists
            if (errorSpan) {
                input.parentNode.removeChild(errorSpan);
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
        cell4.innerHTML = '<input type="date" class="form-control" id="Date1">' + '<input type="text" hidden="" id="InstructorId" value="' + instruid + '">';

        inputs.forEach(function (input, index) {
            var errorSpan = input.parentNode.querySelector('.text-danger');
            if (errorSpan) {
                input.parentNode.removeChild(errorSpan);
            }
        });
    }
}


function addRow(button) {
    var aid = $('#activityid').val();
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

        cell1.innerHTML = '<select class="form-control" id="type" name="ResourceType">' +
            '<option value="">Select an Resource Type</option>' +
            '<option>Supervisor</option>' +
            '<option>Operative</option>' +
            '<option>Labour Man</option>' +
            '<option>HGV</option>' +
            '<option>HGV+Crane</option>' +
            '<option>Trailer</option>' +
            '<option>Loader</option>' +
            '<option>Work Van</option>' +
            '</select>' +
            '<span id="ResourceType" class="text-danger"></span>';
        cell2.innerHTML = '<select class="form-control" id="ReShift" name="Shift">' +
            '<option value="">Select an Shift type</option>' +
            '<option>Shift 1</option>' +
            '<option>Shift 2</option>' +
            '<option>Shift 3</option>' +
            '<option>Shift 4</option>' +
            '<option>Shift 5</option>' +
            '<option>Shift 6</option>' +
            '<option>Shift 7</option>' +
            '<option>Shift 8</option>' +
            '</select>' +
            '<span id="Shift" class="text-danger"></span>';
        cell3.innerHTML = '<select class="form-control" id="daynight" name="DayNight">' +
            '<option value="">Select an Shift</option>' +
            '<option>Day</option>' +
            '<option>Night</option>' +
            '</select>' +
            '<span id="DayNight" class="text-danger"></span>';
        cell4.innerHTML = '<input value="" type="text" class="form-control" id="name" name="Name">' +'<span id="Name" class="text-danger"></span>';
        cell5.innerHTML = '<input type="text" class="form-control" width="500" id="comment" name="Comments">' +'<span id="Comments" class="text-danger"></span>';
        cell6.innerHTML = '<input value="0" type="text" id="rid" style="display: none;">' + '<input value="' + aid + '" type="text" id="activityid" style="display: none;" /> ';
    }

}
function addRow1(button) {

    var aid = $('#activityid').val();
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
            '<option value="">Select an Shift type</option>' +
            '<option>Shift 1</option>' +
            '<option>Shift 2</option>' +
            '<option>Shift 3</option>' +
            '<option>Shift 4</option>' +
            '<option>Shift 5</option>' +
            '<option>Shift 6</option>' +
            '<option>Shift 7</option>' +
            '<option>Shift 8</option>' +
            '</select>' +
            '<span id="ShiftError" class="text-danger"></span>';
        cell2.innerHTML = '<input value="" type="date" class="form-control" name="Date" id="Date" style="width: 150px;"> ' +
            '<span id="DateError" class="text-danger"></span>';
        cell3.innerHTML = '<input value="" type="text" class="form-control" name="SummaryOfWorks" id="SummaryOfWorks">' + '<span id="SummaryOfWorksError" class="text-danger"></span>';

        cell4.innerHTML = '<input value="0" type="text" id="pid" style="display: none;" />' + '<input value="' + aid + '" type="text" id="activityid" style="display: none;" /> ';

    }

}



//ADD sign of and update has submit
function addsignoff(button) {

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
                if (response.success) {


                    $('#signoffsuccessmessage').fadeIn(function () {
                        // Once message is fully displayed, redirect after a delay
                        setTimeout(function () {
                            window.location.reload();
                        }, 2000); // Adjust delay time as needed (in milliseconds)
                    });
                    
                 }
               },
            error: function (xhr, status, error) {
                $('#signofferrormessage').fadeIn().delay(2000).fadeOut();
            }
        });
    }
    else {
        alert("Please Fill atleast One row!");
    }


}


$('.siteInstallation').on('blur', 'input, select,textarea,button', function (e) {

    const target = e.target || e.srcElement;

    // Check if the target element or any of its ancestors is the file input
    if (target.id === 'fileInput' || $(target).closest('#fileInput').length > 0) {
        return; // Do nothing if the file input or its child elements triggered the event
    }

    var actid = globalactivityId;
    //var fileInputValue;
    if (actid == "" || actid == null || actid == undefined || actid == 0) {
        actid = $('#activityid').val();

        var filesInput = document.getElementById('fileInput');
        if (filesInput && filesInput.files && filesInput.files.length > 0) {
            fileInputValue = filesInput.files;
        }
        else {

        }
    }
    var isValid = true;

  
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
        ActivityId: actid,
        Id: $('.sid1').val(),

        // Add other form fields here
    };

    var formData = new FormData();
    // Append all files selected to the formData

    if (filesInput.files.length > 0) {
        for (var i = 0; i < filesInput.files.length; i++) {
            formData.append('SiteImages', filesInput.files[i]);
        }
    }
    
    $.each(Siteformdata, function (key, value) {
        formData.append('Sitedata.' + key, value);
    });

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

    if (filesInput.files.length == 0) {
        var IsimageCount = document.querySelectorAll('#UploadPath');
        if (IsimageCount.length > 0) {
            $('#fileInputError').text('');
        }
        else {
            $('#fileInputError').text('Site/Layout drawings/site images is required.');
            isValid = false;
        }
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
                    if ($('.sid1').val() != 0) {
                        $('#sitesuccessupdatemessage').fadeIn(function () {
                            // Once message is fully displayed, redirect after a delay
                            setTimeout(function () {
                                window.location.href = '/Activity/CreateActivity/' + response.activityId;
                            }, 2000); // Adjust delay time as needed (in milliseconds)
                        });
                    } else {
                        $('#sitesuccessmessage').fadeIn(function () {
                            // Once message is fully displayed, redirect after a delay
                            setTimeout(function () {
                                window.location.href = '/Activity/CreateActivity/' + response.activityId;
                            }, 2000); // Adjust delay time as needed (in milliseconds)
                        });
                    }
                }
            },
            error: function (xhr, status, error) {
                $('#siteerrormessage').fadeIn().delay(2000).fadeOut();
            }
        });
    }

});



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


$(document).ready(function () {
    var isDropdownOrTextboxFocused = false;

    $('#custidfordom').on('focus', 'input[type="text"], select', function () {
        isDropdownOrTextboxFocused = true;
    });

    $('#custidfordom').on('blur', 'input[type="text"], select', function () {
    
        var $currentRow = $(this).closest('.original-row');
        var aid = $('#activityid').val();

        if (!aid) {
            $currentRow.find('#CustomerNameError').text('Please fill basic details');
            $currentRow.find('#ContactNoError').text('Please fill basic details');

            return;
        }



        clearValidationErrors($currentRow);

        var isValid = validateInputFields($currentRow);


        if (isValid && !isDropdownOrTextboxFocused) {

            var custData = {
                ActivityId: aid,
                CustomerName: $currentRow.find('input[name="item.CustomerName"]').val(),
                ContactNo: $currentRow.find('input[name="item.ContactNo"]').val() ,
                custid: $currentRow.find('#custid').val()
            };

            $.ajax({
                url: '/activity/Customersavedata',
                method: 'POST',
                data: custData,
                success: function (response) {
                    if (response.success) {
                        if (response.cid > 0) {

                            $('#customerupdatemessage').fadeIn().delay(2000).fadeOut();
                        }
                        else {
                            $('#customersuccessmessage').fadeIn().delay(2000).fadeOut();
                        }
                    }
                },
                error: function (xhr, status, error) {
                    $('#customererrormessage').fadeIn().delay(2000).fadeOut();
                }
            });
        }


    });

    $('#custidfordom').on('click', function () {
        isDropdownOrTextboxFocused = false;
        $('.text-danger').empty();
    });
    function clearValidationErrors($row) {
        $row.find('.text-danger').empty();
    }


    function validateInputFields($row) {
        var isValid = true;

        $row.find('input[name="item.CustomerName"], input[name="item.ContactNo"] ,select').each(function () {
            var $input = $(this);
            var fieldName = $input.attr('name');
            var fieldValue = $input.val();
            var fieldNameWithoutPrefix = fieldName.replace('item.', '');

            if (!fieldValue.trim()) {
                isValid = false;
                $row.find('#' + fieldNameWithoutPrefix + 'Error').text(fieldNameWithoutPrefix + ' is required');
            }
        });

        return isValid;
    }


});


function addCustRow(button) {
    
    var aid = $('#activityid').val();
    var currentRow = button.closest('.original-row');
    var inputs = currentRow.querySelectorAll('.form-control');
    var errorSpans = currentRow.querySelectorAll('.text-danger');
    var canAddRow = true;

    inputs.forEach(function (input, index) {
        if (input.value.trim() === '' && (input.getAttribute('name') === 'ContactNo' || input.getAttribute('name') === 'CustomerName')) {
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
        var newRow = document.createElement('div');
        newRow.classList.add('panel-body', 'original-row');
        newRow.innerHTML = `
        <div class="row">
        <div class="col-sm-4 col-md-4">
        <div class="form-group clearfix">
        <input type="text" class="form-control custid hidden" name="custid" id="custid">
        <input type="text" class="form-control customer-name" name="item.CustomerName" id="CustomerName">
        <span class="text-danger validation-error" id="CustomerNameError"></span>
        </div>
        </div>
        <div class="col-sm-4 col-md-4">
        <div class="form-group clearfix">
        <input type="text" value="" name="item.ContactNo" pattern ="\d{10}" class="form-control contact-no" id="SAGEOrder">
        <span class="text-danger validation-error" id="ContactNoError"></span>
        </div>
        </div>
        </div>
        `;

        currentRow.parentNode.appendChild(newRow);
    }
}

function Savedate() {

    var isValid = true;
    var flagValue = null;
    var currentUrl = window.location.href;
    var urlParts = currentUrl.split('?');
    var queryParamsPart = urlParts[1];
    // Check if queryParamsPart exists and is not empty
    if (queryParamsPart) {
        // Split the query parameters into an array
        var queryParams = queryParamsPart.split('&');

        // Initialize a variable to store the value of the "flag" parameter
        

        // Loop through each parameter to find the "flag" parameter
        for (var i = 0; i < queryParams.length; i++) {
            var param = queryParams[i].split('=');
            // Check if the parameter name is "flag"
            if (param[0] === "flag") {
                // Store the value of the "flag" parameter
                flagValue = param[1];
                break; // Exit the loop once "flag" parameter is found
            }
        }

        // Output the value of the "flag" parameter to the console
        console.log(flagValue);
    }


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
            flag : flagValue
        };

        $.ajax({

            url: '/activity/Savejobcard',
            method: 'post',
            data: formdata ,
            success: function (response) {
                if (response.success) {
                    console.log($('.jobid').val())
                    if ($('.jobid').val() != 0) {


                        $('#jobcardupdatemessage').fadeIn(function () {
                            // Once message is fully displayed, redirect after a delay
                            setTimeout(function () {
                                document.getElementById("id").value = response.activityId;
                                globalactivityId = response.activityId;
                                globalActivityType = response.activityType;
                                window.location.href = '/Activity/CreateActivity/' + response.activityId;
                            }, 2000); // Adjust delay time as needed (in milliseconds)
                        });


                       //$('#jobcardupdatemessage').fadeIn().delay(2000).fadeOut();
                    }
                    else {

                        $('#jobcardsuccessmessage').fadeIn(function () {
                            // Once message is fully displayed, redirect after a delay
                            setTimeout(function () {
                                document.getElementById("id").value = response.activityId;
                                globalactivityId = response.activityId;
                                globalActivityType = response.activityType;
                                window.location.href = '/Activity/CreateActivity/' + response.activityId;
                            }, 2000); // Adjust delay time as needed (in milliseconds)
                        });
                       // $('#jobcardsuccessmessage').fadeIn().delay(2000).fadeOut();
                    }
                    //document.getElementById("id").value = response.activityId;
                    //globalactivityId = response.activityId;
                    //globalActivityType = response.activityType;
                    //window.location.href = '/Activity/CreateActivity/' + response.activityId;
                   
                    
                }
            },
            error: function (xhr, status, error) {
                $('#jobcarderrormessage').fadeIn().delay(2000).fadeOut();
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

function displayThumbnails(event) {
    var files = event.target.files;
    var thumbnailsContainer = document.getElementById('thumbnails');
    thumbnailsContainer.innerHTML = ''; // Clear previous thumbnails

    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        if (file.type.startsWith('image/')) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var thumbnail = document.createElement('img');
                thumbnail.src = e.target.result;
                thumbnail.style.width = '100px'; // Adjust thumbnail size as needed
                thumbnail.style.marginRight = '10px'; // Adjust spacing between thumbnails
                thumbnail.classList.add('thumbnail'); // Add a class for easier selection
                thumbnailsContainer.appendChild(thumbnail);

                thumbnail.addEventListener('click', function () {
                    // Create a Blob from the base64 data
                    var blob = dataURItoBlob(this.src);
                    var blobUrl = URL.createObjectURL(blob);
                    // Open the Blob URL in a new tab
                    window.open(blobUrl, '_blank');
                });
            };
            reader.readAsDataURL(file);
        }
    }
}

// Function to convert data URI to Blob
function dataURItoBlob(dataURI) {
    var byteString = atob(dataURI.split(',')[1]);
    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ab], { type: mimeString });
}
$('.yardLoading').on('blur', 'input, select,textarea,button', function (e) {

    const target = e.target || e.srcElement;

    // Check if the target element or any of its ancestors is the file input
    if (target.id === 'fileInputyard' || $(target).closest('#fileInputyard').length > 0 || target.id === 'barrierdate' || target.id === 'Trailersupplier' || target.id === 'TrailerSupNum' || target.id === 'LoadPositioned' || target.id === 'UnitLoad' || target.id === 'Vehicle' || target.id === 'TrailerDepart' || target.id === 'TrailerDepartTo' || target.id === 'Loaded' || target.id === 'isOutbound' || target.id === 'buttonSave') {
        return; // Do nothing if the file input or its child elements triggered the event
    }

    var actid = globalactivityId;
    //var fileInputValue;
    if (actid == "" || actid == null || actid == undefined || actid == 0) {
        actid = $('#activityid').val();

        var filesInput = document.getElementById('fileInputloding');
        if (filesInput && filesInput.files && filesInput.files.length > 0) {
            fileInputValue = filesInput.files;
        }
        else {

        }
    }
    var isValid = true;
    //if ($(this).is('#fileInput')) {
    //    fileInputValue = $('#fileInput')[0].files[0];
    //}

    var Yardformdata = {
        Startandfinishtime: $('#bstartfinishTime').val(),
        LiftingEquipmentUsed: $('#blifting').val(),
        ChainLiftingequipmenttobeused: $('#chainlifteq').val(),
        IncidentReporting: $('#LIncireporting').val(),
        AnyNearMissOccurrences: $('#txtanymissoccure').val(),
        BarrierConditionChecks: $('#conditions').val(),
        AllRelevantActivityRams: $('#Rams').val(),
        ActivityId: actid,
        Id: $('.ylid').val()

        // Add other form fields here
    };
    var formData = new FormData();
    // Append all files selected to the formData

    if (filesInput.files.length > 0) {
        for (var i = 0; i < filesInput.files.length; i++) {
            formData.append('SiteImages', filesInput.files[i]);
        }
    }
    //$.each($('#fileInput')[0].files, function (index, file) {
    //    formData.append('SiteImages', file);
    //});


    // Append other form data fields to the formData
    $.each(Yardformdata, function (key, value) {
        formData.append('Sitedata.' + key, value);
    });

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


    if (filesInput.files.length == 0) {
        var IsimageCount = document.querySelectorAll('#UploadPath');
        if (IsimageCount.length > 0) {
            $('#fileInputlError').text('');
        }
        else {
            $('#fileInputlError').text('Site/Layout drawings/site images is required.');
            isValid = false;
        }
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
                    if ($('.ylid').val() != 0) {

                        $('#sitesuccessupdatemessage').fadeIn().delay(2000).fadeOut();
                    } else {
                        $('#sitesuccessmessage').fadeIn().delay(2000).fadeOut();
                    }
                }
            },
            error: function (xhr, status, error) {
                $('#siteerrormessage').fadeIn().delay(2000).fadeOut();
            }
        });
    }


});




