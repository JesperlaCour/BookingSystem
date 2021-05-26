
document.addEventListener('DOMContentLoaded', function () {
    var selectedEvent = null;
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        selectable: true,
        editable: true,
        eventOverlap: false,
        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        headerToolbar: {
            center: 'resourceTimelineDay,resourceTimelineFourDays,resourceTimelineWeek',
            right: 'DatePicker,today prev,next'
        },
        initialView: 'resourceTimelineFourDays',
        views: {
            resourceTimelineFourDays: {
                buttontext: '4 dage',
                type: 'resourceTimeline',
                duration: { days: 4 }
            }
        },
        customButtons: {
            DatePicker: {
                text: 'Vælg dato',
                click: function () {
                    $("#chooseDate").modal()
                }
            }
        },  
        resourceGroupField: 'groupId',
        resourceAreaWidth: "15%",
        height: 'auto',
        buttonText: {
            month: 'måned',
            week: 'uge',
            day: 'dag',
            resourceTimelineFourDays: '4 dage'
        },
        nowIndicator: true,
        businessHours: {
            // days of week. an array of zero-based day of week integers (0=Sunday)
            daysOfWeek: [1, 2, 3, 4, 5], // Monday - Friday

            startTime: '8:00', // a start time (8am in this example)
            endTime: '17:00', // an end time (5pm in this example)
        },

        locale: 'da',
        //filterResourcesWithEvents: true,

        weekNumberCalculation: "ISO",

        //Resources/events call
        resources: "Resource/getCalendarResources",
        events: "Calendar/getCalendarEvents",

        select: function (info) {
            selectedEvent = info;
            //console.log(selectedEvent);
            CreateNewEvent()
        },

        eventResize: function (info) {
            selectedEvent = info.event;
            //console.log(selectedEvent);
            UpdateExistingEvent();

        },
        eventDrop: function (info) {
            selectedEvent = info.event;
            UpdateExistingEvent();
        },
        eventClick: function (info) {
            selectedEvent = info.event;
            EventClick();
        }
    });
    calendar.render();



    //Create new events
    function CreateNewEvent() {

        if (selectedEvent != null) {
            $("#eventTitle").text("Ny booking");
            $('#txtCreateStart').val(toDatetimeLocal(selectedEvent.start));
            $('#txtCreateEnd').val(toDatetimeLocal(selectedEvent.end));
            $('#CreateModal').modal();
            $("#CreateResourceId").val(selectedEvent.resource.id)
        }
    }

    //function to change from selected datetime in calendar to datetime-local used in modal-forms
    function toDatetimeLocal(time) {
        time.setMinutes(time.getMinutes() - time.getTimezoneOffset());
        time = time.toISOString().slice(0, 16);
        return time;
    }

    //Update Existing event
    function UpdateExistingEvent() {
        console.log(selectedEvent);
        $.get("Calendar/GetEvent/" + selectedEvent.id, function (data) {
            if (data != null) {
                console.log(data);
                $("#editTitle").text("Edit");
                $("#editId").val(data.id);
                $('#txtSubject').val(data.title);
                $('#txtStart').val(toDatetimeLocal(selectedEvent.start));
                $('#txtEnd').val(toDatetimeLocal(selectedEvent.end));
                $("#editResourceId").val(selectedEvent._def.resourceIds);
                $("#editUserName").val(data.userName);
                $("#editAddressStr").val(data.addressStr);
            }
        });
        $('#DetailModal').modal('hide');
        $('#EditModal').modal();
    }


    //shows details about event when clicked
    function EventClick() {
        $.get("Calendar/GetEvent/" + selectedEvent.id, function (data) {
            console.log(data)
            $("#detailTitle").text(data.title)
            $("#detailUserName").text(data.userName)
            $("#detailStart").text(data.start)
            $("#detailEnd").text(data.end)
            $("#detailAddress").text(data.addressStr)
        })
        $("#DetailModal").modal();
    }

    //alert box (modal)(takes string as parameter)
    function AlertModal(strText) {
        $("#alertText").text(strText);
        $("#AlertModal").modal();
    }


    $("#btnEdit").click(function () {
        UpdateExistingEvent();
    })

    $("#btnDelete").click(function () {
        if (selectedEvent != null) {
            $("#DetailModal").modal('hide');
            $("#DeleteId").val(selectedEvent.id);
            $("#DeleteEventTitle").text(selectedEvent.title);
            $("#DeleteModal").modal();

        }
    })

    $("#btnEditClose, #btnEditCloseFooter").click(function () {
        $("#EditModal").modal('hide');
        calendar.refetchEvents()
    })

    $("#btnChangeAddress").click(function () {
        $("#EditModal").modal('hide');
        $("#changeAddressModal").modal();
        var existingAddress = $("#editAddressStr").val();
        console.log(existingAddress);
        $("#ExistingAddress").val(existingAddress);
    })

    $("#btnDismissNewAddress").click(function () {
        $("#EditModal").modal('show');
        $("#ChangeAddress").val("");
    })

    $("#NewAddressSave").click(function (data) {
        $("#editAddressStr").val($("#NewAddress").val())
        $("#ChangeAddress").val("");
        $("#changeAddressModal").modal('hide');
        $("#EditModal").modal('show');

    })

    //DAWA autocomplete adresse (https://dataforsyningen.dk/)
    "use strict"
    dawaAutocomplete.dawaAutocomplete(document.getElementById("adresse"), {
        select: function (selected) {
            $("#valgtadresse").val(selected.tekst);
        }
    });

    "use strict"
    dawaAutocomplete.dawaAutocomplete(document.getElementById("ChangeAddress"), {
        select: function (selected) {
            $("#NewAddress").val(selected.tekst);
        }
    });

    //datetime picker

    $("#btnChooseDate").click(function () {
        var date = document.getElementById("datetimePicker");
        console.log(date.value)
        calendar.gotoDate(date.value)
        $("#chooseDate").modal('hide')
    })

});


