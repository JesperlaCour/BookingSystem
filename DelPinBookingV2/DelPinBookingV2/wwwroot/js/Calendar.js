
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

    }

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
        if (selectedEvent != null) {
            $("#EditTitle").text("Edit");
            $("#EditId").val(selectedEvent.id);
            $('#txtSubject').val(selectedEvent.title);
            $('#txtStart').val(toDatetimeLocal(selectedEvent.start));
            $('#txtEnd').val(toDatetimeLocal(selectedEvent.end));
            $("#EditResourceId").val(selectedEvent._def.resourceIds);
            $("EditUserName").val(selectedEvent.UserName);
            $("EditAddressId").val(selectedEvent.addressId);
        }
        $('#DetailModal').modal('hide');
        $('#EditModal').modal();
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


    $("#btnEdit").click(function () {
        console.log(selectedEvent);
        if (selectedEvent != null) {

            $("#EditTitle").text("Edit");
            $("#EditId").val(selectedEvent.id);
            $('#txtSubject').val(selectedEvent.title);
            $('#txtStart').val(toDatetimeLocal(selectedEvent.start));
            $('#txtEnd').val(toDatetimeLocal(selectedEvent.end));
            $("#EditResourceId").val(selectedEvent._def.resourceIds);
            $("EditUsername").val(selectedEvent.UserName);
            $("EditAddressId").val(selectedEvent.addressId);
        }
        $('#DetailModal').modal('hide');
        $('#EditModal').modal();
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
        $("#EditModal").modal("hide");
        RenderCalendar();
    })
});
