$(function() {

  // sample calendar events data

  var calendarEl = document.getElementById('fullcalendar');

  // initialize the calendar
  var calendar = new FullCalendar.Calendar(calendarEl, {
    headerToolbar: {
      left: "prev,today,next",
      center: 'title',
      right: 'dayGridMonth,listMonth'
    },
    editable: false,
    droppable: true, // this allows things to be dropped onto the calendar
    fixedWeekCount: true,
    // height: 300,
    initialView: 'dayGridMonth',
    timeZone: 'UTC+7',
    hiddenDays:[],
    navLinks: 'true',
    // weekNumbers: true,
    // weekNumberFormat: {
    //   week:'numeric',
    // },
    dayMaxEvents: 2,
    events: [],
      eventSources: [{
        url: window.location.origin+'/AJAX/Callendar',
          backgroundColor: 'rgba(1,104,114,.55)',
        borderColor: '#0168fa'
      }],
/*      eventSources: [calendarEvents],
*/    drop: function(info) {
        // remove the element from the "Draggable Events" list
        // info.draggedEl.parentNode.removeChild(info.draggedEl);
    },
    eventClick: function(info) {
      var eventObj = info.event;
      console.log(info);
      $('#modalTitle1').html(eventObj.title);
      $('#modalBody1').html(eventObj._def.extendedProps.description);
      $('#eventUrl').attr('href',eventObj.url);
        $('#exampleModalCenter').modal("show");
    },
    dateClick: function(info) {
      $("#createEventModal").modal("show");
      console.log(info);
    },
  });

  calendar.render();


});