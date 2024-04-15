

var globalactivityId;
var globalActivityType;
var handleBlurEvent = true;
$('.Jobcard input, .Jobcard textarea, .Jobcard button,.Jobcard select').on('blur', function () {
    Savedate();
});
$('.Installation, .loading, .Tipping').change(function () {
    debugger;
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


//ADD sign of and update has submit
function addsignoff(button) {
    debugger;
    var actid = $('#id').val();

    var rowData1 = new Object();
    var rowData2 = new Object();


    var signOffData = new Array();


    var completionDate1 = $('#Completeion1Date1').val();
    var signature1 = $('#Signature1').val();
    var name1 = $('#Name1').val();
    var date1 = $('#Date1').val();
    var InstruId = $('#InstructorId').val();

    var completionDate2 = $('#Completeion1Date2').val();
    var signature2 = $('#Signature2').val();
    var name2 = $('#Name2').val();
    var date2 = $('#Date2').val();
    var InstruId = $('#InstructorId').val();

    rowData1.CompetionDate = completionDate1
    rowData1.InstructorId = InstruId
    rowData1.PrintName = name1
    rowData1.SignOffDate = date1
    rowData1.Signature = signature1
    rowData1.ActivityId = actid


    rowData2.CompetionDate = completionDate2
    rowData2.InstructorId = InstruId
    rowData2.PrintName = name2
    rowData2.SignOffDate = date2
    rowData2.Signature = signature2
    rowData2.ActivityId = actid



    signOffData.push(rowData1);
    signOffData.push(rowData2);


    $.ajax({
        type: 'POST',
        url: '/Activity/SaveSignOff',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(signOffData),
        datatype: 'json',
        success: function (response) {

            // Handle success response from the server
            $('#Completeion1Date1').val('');
            $('#Signature1').val('');
            $('#Name1').val('');
            $('#Date1').val('');

            $('#Completeion1Date2').val('');
            $('#Signature2').val('');
            $('#Name2').val('');
            $('#Date2').val('');
            alert('Manager Sign Off Saved!');
            console.log(response);
        },
        error: function (xhr, status, error) {
            // Handle error response from the server
            console.error(xhr.responseText);
        }
    });
  

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
function addRow1(button) {

    var aid;
    if (globalactivityId == undefined || globalactivityId == '' || globalactivityId == 0) {
        aid = $('#activityid').val();
    }
    if (aid == '') {
        alert("Please fill basic details")
    } else {


        

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
                    errorSpans[index].innerText = ""; // Clear error message if field is filled
                }
            }
        });

        if (canAddRow) {
            inputs.forEach(function (input) {
                input.disabled = true;
            });
            button.disabled = true;

            var shift = inputs[0].value;
            var date = inputs[1].value;
            var summary = inputs[2].value;
            var pid = currentRow.querySelector("#pid").value;

            $.ajax({
                url: '/Activity/ProductData',
                type: 'POST',
                dataType: 'json',
                data: { shift: shift, date: date, summary: summary, aid: aid, pid: pid },
                success: function (data) {
                    if (data.success) {
                        if (pid == null || pid == "" || pid == '0') {
                            var newRow = table.insertRow(currentRow.rowIndex + 1); // Insert after current row
                            var cell1 = newRow.insertCell(0);
                            var cell2 = newRow.insertCell(1);
                            var cell3 = newRow.insertCell(2);
                            var cell4 = newRow.insertCell(3);


                            cell1.innerHTML = '<select class="form-control" id="Shift1to8">' +
                                '<option>Shift 1</option>' +
                                '<option>Shift 2</option>' +
                                '<option>Shift 3</option>' +
                                '<option>Shift 4</option>' +
                                '<option>Shift 5</option>' +
                                '<option>Shift 6</option>' +
                                '<option>Shift 7</option>' +
                                '<option>Shift 8</option>' +
                                '</select>';
                            cell2.innerHTML = '<input value="" type="date" class="form-control" id="Dateforproduct" style="width: 150px;">';
                            cell3.innerHTML = '<input value="" type="text" class="form-control" id="Summaryforproduct">' + '<input value="0" type="text" id="pid" style="display: none;" />' + '<input value="' + aid + '" type="text" id="activityid" style="display: none;" /> ';

                            cell4.innerHTML = '<button onclick="addRow1(this)" class="btn btn-primary btn-primary">Add New Row</button>';

                        }
                    } else {
                        alert('Data save Succesfully.');
                    }
                },
                error: function () {
                    alert('Error occurred while saving data.');
                }
            });
        }
    }
}

//for add row save and Update Resource data
function addRow(button) {
    
    var aid;
    if (globalactivityId == undefined || globalactivityId == '' || globalactivityId == 0) {
        aid = $('#activityid').val();
    }
    if (aid == '') {
        alert("Please fill basic details")
    } else {

        var table = document.getElementById("myTable");
        var currentRow = button.parentNode.parentNode; // Get the current row
        var inputs = currentRow.querySelectorAll('.form-control');
        var errorSpans = currentRow.querySelectorAll('.text-danger');
        var canAddRow = true;
        //var lastRow = table.rows[table.rows.length - 1];
        //var errorSpans = lastRow.querySelectorAll('.text-danger');
        //var inputs = lastRow.querySelectorAll('.form-control');

        //var canAddRow = true;

        // Check if all input fields in the last row are filled out
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
            var resourcetype = inputs[0].value;
            var shift = inputs[1].value;
            var daynight = inputs[2].value;
            var name = inputs[3].value;
            var comment = inputs[4].value;
            var rid = currentRow.querySelector("#rid").value;

            $.ajax({
                url: '/Activity/ResourseData',
                type: 'POST',
                dataType: 'json',
                data: { resourcetype: resourcetype, shift: shift, daynight: daynight, name: name, comment: comment, aid: aid, rid: rid },
                success: function (data) {
                    if (data.success) {
                        inputs.forEach(function (input) {
                            input.disabled = true;
                        });
                        button.disabled = true;
                        if (rid == null || rid == "" || rid == '0') {


                            // Disable previous row inputs and button
                            var newRow = table.insertRow(-1);
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
                            cell2.innerHTML = '<select class="form-control" id="Shift1">' +
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
                            cell5.innerHTML = '<input type="text" class="form-control" width="500" id="comment">' + '<input value="0" type="text" id="rid" style="display: none;">' + '<input value="' + aid + '" type="text" id="activityid" style="display: none;" /> ';
                            cell6.innerHTML = '<button onclick="addRow(this)" class="btn btn-primary btn-primary">Add New Row</button>';
                        } else {
                            alert('Data updated Succesfully.');
                        }

                    } else {
                        alert('Failed to save data. Please try again.');
                    }
                },
                error: function () {
                    alert('Error occurred while saving data.');
                }
            });
        }

    }
}

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
                    if (response.isOutBound == 'on') {
                        var rowCount = $('#TrailerDetails1 tbody tr').length;
                        var nextId = rowCount + 1;
                        var newRow = '<tr>' +
                            '<td>' + nextId + '</td>' +
                            '<td>' + response.trailerSupplier + '</td>' +
                            '<td>' + response.trailerNumber + '</td>' +
                            '<td>' + response.quantity + '</td>' +
                            '<td>' + response.loadDepot + '</td>' +
                            '<td>' + response.departFrom + '</td>' +
                            '<td>' + response.date + '</td>' +
                            '<td>' + response.loadedTippedBy + '</td>' +
                            '</tr>';
                        $('#TrailerDetails1 tbody').append(newRow);
                        alert('Trailer Details Out Bound Saved successfully.');
                    }
                  
                    else {
                        var rowcount2 = $('#TrailerDetails2 tbody tr').length;
                        var nextId1 = rowcount2 + 1;
                        var newRow1 = '<tr>' +
                            '<td>' + nextId1 + '</td>' +
                            '<td>' + response.trailerSupplier + '</td>' +
                            '<td>' + response.trailerNumber + '</td>' +
                            '<td>' + response.quantity + '</td>' +
                            '<td>' + response.loadDepot + '</td>' +
                            '<td>' + response.date + '</td>' +
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




