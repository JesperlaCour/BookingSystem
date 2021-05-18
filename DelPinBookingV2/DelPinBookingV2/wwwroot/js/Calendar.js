﻿document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
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
            if (confirm('Dato fra ' + info.startStr + ' til ' + info.endStr + ' på ressource ' + info.resource.id)) {
                calendar.addEvent(
                    {
                        resourceId: info.resource.id,
                        allDay: false,
                        title: 'Ny event',
                        start: info.startStr,
                        end: info.endStr
                    })
            }
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

            $("#myModal #eventTitle").text(info.event.title);
            var $description = $("<div/>");
            $description.append($("<p/>").html("<b>EventID: </b>" + info.event.id));
            $description.append($("<p/>").html("<b>Start: </b>" + info.event.start.toLocaleString()));
            if (info.event.end != null) {
                $description.append($("<p/>").html("<b>End: </b>" + info.event.end.toLocaleString()));
            }
            $("#myModal #pDetails").empty().html($description);
            $("#myModal").modal();
        }

    });
    calendar.render();
});