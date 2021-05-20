
document.addEventListener('DOMContentLoaded', function () {
    var selectedEvent = null;
    var calendarEl = document.getElementById('calendar');

    RenderCalendar();
    function RenderCalendar() {
        var calendar = new FullCalendar.Calendar(calendarEl, {
            selectable: true,
            editable: true,
            eventOverlap: false,
            schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
            headerToolbar: { center: 'resourceTimelineDay,resourceTimelineFourDays,resourceTimelineWeek' },
            initialView: 'resourceTimelineFourDays',
            views: {
                resourceTimelineFourDays: {
                    buttontext: '4 dage',
                    type: 'resourceTimeline',
                    duration: { days: 4 }
                }
            },
            resourceAreaWidth: "15%",

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

    }

    //Create new events
    function CreateNewEvent() {

        if (selectedEvent != null) {
            $('#txtCreateStart').val(toDatetimeLocal(selectedEvent.start));
            $('#txtCreateEnd').val(toDatetimeLocal(selectedEvent.end));
            $('#CreateModal').modal();
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
        if (selectedEvent != null) {
            var eventObject = {
                start: selectedEvent.startStr,
                end: selectedEvent.endStr,
                title: selectedEvent.title,
                id: selectedEvent.id,
                resourceId: selectedEvent._def.resourceIds,
                allDay: selectedEvent.allDay,
                addressId: 1
            };
            //console.log(eventObject);
            $.ajax({
                url: "Calendar/UpdateEvent",
                type: "PUT",
                dataType: "JSON",
                data: eventObject,
                success: function (result) {
                    AlertModal("Updated id: " + result)
                },
                error: function (result) {
                    AlertModal("Fejl i opdatering");
                }
            })
        }

    }

    //shows details about event when clicked
    function EventClick() {
        $("#DetailModal #eventTitle").text(selectedEvent.title);
        var $description = $("<div/>");
        $description.append($("<p/>").html("<b>EventID: </b>" + selectedEvent.id));
        $description.append($("<p/>").html("<b>Start: </b>" + selectedEvent.start.toLocaleString()));
        $description.append($("<p/>").html("<b>End: </b>" + selectedEvent.end.toLocaleString()));
        $("#DetailModal #pDetails").empty().html($description);
        $("#DetailModal").modal();
    }

    //alert box (modal)(takes string as parameter)
    function AlertModal(strText) {
        $("#alertText").text(strText);
        $("#AlertModal").modal();
    }

    //saves new event
    $('#btnCreateSave').click(function () {
        //var title = $("#txtTitle").val();
        //console.log(title)
        var startDate = $('#txtCreateStart').val();
        var endDate = $('#txtCreateEnd').val();
        if (selectedEvent != null) {
            var newEvent = {
                resourceId: selectedEvent.resource.id,
                customerId: null,
                allDay: false,
                start: startDate,
                end: endDate,
                title: $("#txtTitle").val(),
                addressId: 1
            }
            //console.log(newEvent);
            $.ajax({
                url: "Calendar/CreateEvent",
                type: "POST",
                dataType: "JSON",
                data: newEvent,
                success: function (result) {
                    AlertModal("Event created")
                    $('#CreateModal').modal('hide');
                     RenderCalendar();
                },
                error: function (result) {
                    AlertModal("Fejl i oprettelse af booking");
                }
            })
        }
    })

    $("#btnEdit").click(function () {
        console.log(selectedEvent);
        if (selectedEvent != null) {
            $("#EditTitle").text("Edit");
            $('#hdEventID').val(selectedEvent.eventID);
            $('#txtSubject').val(selectedEvent.title);
            $('#txtStart').val(toDatetimeLocal(selectedEvent.start));
            $('#txtEnd').val(toDatetimeLocal(selectedEvent.end));
        }
        $('#DetailModal').modal('hide');
        $('#EditModal').modal();
    })

    $("#btnDelete").click(function () {
        //console.log(selectedEvent);
        if (selectedEvent != null && confirm("Vil du slette bookingen")) {
            var deletedEvent = { id: selectedEvent.id }
            //console.log(object);
            $.ajax({
                url: "Calendar/DeleteEvent",
                type: "DELETE",
                dataType: "JSON",
                data: deletedEvent,
                success: function (result) {
                    $('#DetailModal').modal('hide');
                    AlertModal("Booking " + deletedEvent.id + " slettet");
                    RenderCalendar();
                },
                error: function (result) {
                    AlertModal("Fejl i sletning af booking")
                }
            })
        }
    })
    $("#btnSave").click(function () {
        //console.log(selectedEvent);
        var startDate = $('#txtStart').val();
        var endDate = $('#txtEnd').val();
        if (startDate > endDate) {
            AlertModal('Invalid end date');
            return;
        }
        var SaveEvent = {
            start: startDate,
            end: endDate,
            title: selectedEvent.title,
            id: selectedEvent.id,
            resourceId: selectedEvent._def.resourceIds,
            allDay: selectedEvent.allDay,
            addressId: 1
        };
        $.ajax({
            url: "Calendar/UpdateEvent",
            type: "PUT",
            dataType: "JSON",
            data: SaveEvent,
            success: function (result) {
                AlertModal("Ændring gemt");
                $('#EditModal').modal('hide');
                RenderCalendar();
            },
            error: function (result) {
                AlertModal("Fejl - ændring ikke gemt");
            }
        })
    })
});
