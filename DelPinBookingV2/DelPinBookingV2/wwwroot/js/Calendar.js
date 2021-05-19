
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
            resourceAreaWidth: "20%",

            select: function (info) {
                selectedEvent = info;
                console.log(selectedEvent);
                CreateNewEvent()

                //if (confirm('Dato fra ' + info.startStr + ' til ' + info.endStr + ' på ressource ' + info.resource.id)) {
                //    var object = new Object();
                //    object.resourceId = info.resource.id;
                //    object.customerId = null;
                //    object.allDay = false;
                //    object.start = info.startStr;
                //    object.end = info.endStr;
                //    object.title = "Ragnar";
                //    object.addressId = 1;
                //    console.log(object);
                //    $.ajax({
                //        url: "Calendar/CreateEvent",
                //        type: "POST",
                //        dataType: "JSON",
                //        data: object,
                //        success: function (result) {
                //            alert("Updated id: " + result)
                //        }
                //    })
                //}
            },
           

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
            resources: "Resource/getCalendarResources",
            events: "Calendar/getCalendarEvents",


            eventResize: function (info) {
                console.log("EventResize");
                alert("Event is resized");
                var object = new Object();
                object.start = info.event.startStr;
                object.end = info.event.endStr;
                object.title = info.event.title;
                object.id = info.event.id;
                object.resourceId = info.event._def.resourceIds
                object.allDay = info.event.allDay;
                object.addressId = 1;
                console.log(object.id);
                console.log(info);
                console.log(info.event.resourcesIds)
                $.ajax({
                    url: "Calendar/UpdateEvent",
                    type: "PUT",
                    dataType: "JSON",
                    data: object,
                    success: function (result) {
                        alert("Updated id: " + result)
                    }
                })
            },

            eventClick: function (info) {
                console.log("Fejler her", info, typeof info);
                selectedEvent = info.event;
                $("#DetailModal #eventTitle").text(info.event.title);
                var $description = $("<div/>");
                $description.append($("<p/>").html("<b>EventID: </b>" + info.event.id));
                $description.append($("<p/>").html("<b>Start: </b>" + info.event.start.toLocaleString()));
                if (info.event.end != null) {
                    $description.append($("<p/>").html("<b>End: </b>" + info.event.end.toLocaleString()));
                }
                $("#DetailModal #pDetails").empty().html($description);
                $("#DetailModal").modal();
            }

        });
        calendar.render();

    }

    //Create new events
    function CreateNewEvent() {

        if (selectedEvent != null) {
            var start = selectedEvent.start;
            start.setMinutes(start.getMinutes() - start.getTimezoneOffset());
            start = start.toISOString().slice(0, 16);
            console.log(start);
            var end = selectedEvent.end;
            end.setMinutes(end.getMinutes() - end.getTimezoneOffset());
            end = end.toISOString().slice(0, 16);
            console.log(end);
            $('#txtStart2').val(start);
            $('#txtEnd2').val(end);
            $('#CreateModal').modal();
        }
    }

    $('#btnCreateSave').click(function () {
        var title = $("#txtTitle").val();
        console.log(title)
        var startDate = $('#txtStart2').val();
        var endDate = $('#txtEnd2').val();
        if (selectedEvent != null) {
            var object = new Object();
            object.resourceId = selectedEvent.resource.id;
            object.customerId = null;
            object.allDay = false;
            object.start = startDate;
            object.end = endDate;
            object.title = title;
            object.addressId = 1;
            console.log(object);
            $.ajax({
                url: "Calendar/CreateEvent",
                type: "POST",
                dataType: "JSON",
                data: object,
                success: function (result) {
                    alert("Event created: " + result)
                    $('#CreateModal').modal('hide');
                    RenderCalendar();
                }
            })
        }
    })
    

    $("#btnEdit").click(function () {
        console.log(selectedEvent);
        if (selectedEvent != null) {
            $('#hdEventID').val(selectedEvent.eventID);
            $('#txtSubject').val(selectedEvent.title);
            var start = selectedEvent.start;
            start.setMinutes(start.getMinutes() - start.getTimezoneOffset());
            start = start.toISOString().slice(0, 16);
            console.log(start);
            var end = selectedEvent.end;
            end.setMinutes(end.getMinutes() - end.getTimezoneOffset());
            end = end.toISOString().slice(0, 16);
            console.log(end);

            $('#txtStart').val(start);
            
            $('#txtEnd').val(end);
        }
        $('#DetailModal').modal('hide');
        $('#EditModal').modal();
    })

    $("#btnDelete").click(function () {
        console.log(selectedEvent);
        if (selectedEvent != null && confirm("Vil du slette bookingen")) {
            var object = new Object();
            object.id = selectedEvent.id;
            console.log(object);
            $.ajax({
                url: "Calendar/DeleteEvent",
                type: "DELETE",
                dataType: "JSON",
                data: object,
                success: function (result) {
                    $('#DetailModal').modal('hide');
                    alert("Booking slettet");
                    RenderCalendar();
                }
                //error: function (result) {
                //    alert("Fejl i sletning af booking")
                //}
            })
        }
        
        
    })

    $("#btnSave").click(function () {
        console.log(selectedEvent);
        var startDate = $('#txtStart').val();
        var endDate = $('#txtEnd').val();
        if (startDate > endDate) {
            alert('Invalid end date');
            return;
        }
        var object = new Object();
        object.start = startDate
        object.end = endDate
        object.title = selectedEvent.title;
        object.id = selectedEvent.id;
        object.resourceId = selectedEvent._def.resourceIds;
        object.allDay = selectedEvent.allDay;
        object.addressId = 1;
        console.log(object);
        $.ajax({
            url: "Calendar/UpdateEvent",
            type: "PUT",
            dataType: "JSON",
            data: object,
            success: function (result) {
                $('#EditModal').modal('hide');
                RenderCalendar();
            }
        })
        
    })


});
