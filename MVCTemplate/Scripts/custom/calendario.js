/* ============================================================
 * Calendar
 * This is a Demo App that was created using Pages Calendar Plugin
 * We have demonstrated a few function that are useful in creating
 * a custom calendar. Please refer docs for more information
 * ============================================================ */

var inactivityTime = function () {
    var t;
    window.onload = resetTimer;
    // DOM Events
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(t);
        t = setTimeout(Calendar.rebuild, 30000);
    }
};

(function ($) {
    'use strict';
    $(document).ready(function () {
        var selectedEvent;
        $.ajax({
            type: "GET",
            url: mainUrl + "Calendario/getEvents",
            data: { MedicoId: getUrlParameter('MedicoId') },
            success: function (data) {
                $('#myCalendar').pagescalendar({
                    events: JSON.parse(data),
                    slotDuration: 15,
                    onViewRenderComplete: function (range) {
                        var start = range.start.format();
                        var end = range.end.format();
                        if ($("body").hasClass('pending')) {
                            return;
                        }
                        $('body').addClass('pending');

                        $("body").removeClass('pending');

                        $.ajax({
                            url: mainUrl + 'Medicos/GetInfo/' + getUrlParameter('MedicoId'),
                            type: 'GET',
                            async: true,
                            success: function (data) {
                                var medico = JSON.parse(data);
                                if (medico.Especialidad.NombreEspecialidad.toLowerCase().indexOf("2do turno") == -1) {
                                    $('#weekGrid').children('div:lt(6)').toggle();
                                    $('#time-slots').children('div:lt(6)').toggle()
                                    $('#time-slots').children('div').slice(-4).toggle();
                                    $('#weekGrid').children('div').slice(-4).toggle();
                                }
                                else {
                                    $('#lnkHorasInactivas').text('Ocultar horas inactivas')
                                }
                                
                            },
                            error: function (error) {
                                console.log(error);
                            }
                        });

                        
                    },
                    onEventClick: function (event) {
                        //if (!event.readOnly) {
                        //Open Pages Custom Quick View
                        if (!$('#calendar-event').hasClass('open'))
                            $('#calendar-event').addClass('open');

                        //console.log(event);
                        $.ajax({
                            type: "GET",
                            url: mainUrl + "Calendario/GetEventoDetalle",
                            data: "id=" + event.other.folio,
                            success: function (data) {
                                event.other = JSON.parse(data);
                                selectedEvent = event;
                                setEventDetailsToForm(selectedEvent);
                            },
                            error: function (ajaxContext) {
                                $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                                $("body").removeClass('pending');
                            }
                        });


                        //}

                    },
                    onEventDragComplete: function (event) {
                        $.ajax({
                            type: "GET",
                            url: mainUrl + "Calendario/GetEventoDetalle",
                            data: "id=" + event.other.folio,
                            success: function (data) {
                                event.other = JSON.parse(data);
                                selectedEvent = event;
                                setEventDetailsToForm(selectedEvent);
                            },
                            error: function (ajaxContext) {
                                $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                                $("body").removeClass('pending');
                            }
                        });

                    },
                    onEventResizeComplete: function (event) {
                        $.ajax({
                            type: "GET",
                            url: mainUrl + "Calendario/GetEventoDetalle",
                            data: "id=" + event.other.folio,
                            success: function (data) {
                                event.other = JSON.parse(data);
                                selectedEvent = event;
                                setEventDetailsToForm(selectedEvent);
                            },
                            error: function (ajaxContext) {
                                $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                                $("body").removeClass('pending');
                            }
                        });
                    },
                    //onTimeSlotDblClick: function (timeSlot) {
                    //    $('#calendar-event').removeClass('open');
                    //    //Adding a new Event on Slot Double Click
                    //    var newEvent = {
                    //        title: 'my new event',
                    //        class: 'bg-success-lighter',
                    //        start: timeSlot.date,
                    //        end: moment(timeSlot.date).add(1, 'hour').format(),
                    //        allDay: false,
                    //        other: {
                    //            //You can have your custom list of attributes here
                    //            note: 'test'
                    //        }
                    //    };
                    //    selectedEvent = newEvent;
                    //    $('#myCalendar').pagescalendar('addEvent', newEvent);
                    //    setEventDetailsToForm(selectedEvent);
                    //}
                });




            },
            error: function (ajaxContext) {
                $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                $("body").removeClass('pending');
            }
        });





        // Some Other Public Methods That can be Use are below \
        //console.log($('body').pagescalendar('getEvents'))
        //get the value of a property
        //console.log($('body').pagescalendar('getDate','MMMM'));

        function setEventDetailsToForm(event) {
            //console.log(event);

            //Limpiar los textos
            $('#eventIndex').val();
            $('#txtEventName').val();
            $('#Folio').val();
            $('#txtEspecialidad').val();
            $('#lblNombre').val();
            $('#lblNoEmpleado').val();
            $('#lblDivision').val();
            $('#lblTurno').val();
            $('#lblComentarios').val();
            $('#ltBitacora').text('');
            $('#eventSave').attr('hidden', false);
            $('#eventAssisted').attr('hidden', false);
            $('#eventUnassisted').attr('hidden', false);
            $('#eventCanceled').attr('hidden', false);
            $('input[type=radio]:not(:checked)').parent('div').attr('hidden', false);
            $('#lblBaja').attr('hidden', true);

            //Show Event date
            //Display labels
            $('#event-date').html(moment(event.start).format('D MMMM YYYY'));
            $('#lblfromTime').html(moment(event.start).format('h:mm A'));
            $('#lbltoTime').html(moment(event.end).format('h:mm A'));

            //Cargar la información
            $('#eventIndex').val(event.index);
            $('#txtEventName').val(event.title);
            $('#txtFolio').val(event.other.folio);
            $('#txtEspecialidad').val(event.other.especialidad);
            $('#lblNombre').html(event.other.nombre);
            $('#lblNoEmpleado').html(event.other.numeroempleado);
            $('#lblPacienteNombre').html(event.other.paciente.InformacionPersonal.NombreApellido);
            $('#lblPacienteParentesco').html(event.other.paciente.Parentesco.Definicion);
            $('#lblMotivo').html(event.other.motivo.MotivoEspecialidad);
            $('#lblDivision').html(event.other.division);
            $('#lblTurno').html(event.other.turno).attr('title', event.other.turno);
            $('#lblComentarios').html(event.other.comentarios);
            $('#lblEstadoActual').html(event.other.bitacora[event.other.bitacora.length - 1].Estado.Detalle);
            $('#lnkReporte').attr("href", mainUrl + "Citas/CitaConfirmedReport/" + event.other.folio);
            $('#lnkBorrar').attr("href", mainUrl + "Citas/Delete/" + event.other.folio);
            $('#lnkTratamientos').attr("href", mainUrl + "Tratamientos/TratamientoCita/" + event.other.folio);
            if (event.other.TieneEntrevista) {
                $('#lnkHistorial').css("display", "block");
                //$('#lnkHistorial').attr("href", mainUrl + "entrevistas/HistorialFamiliar/" + event.other.paciente.PacienteId);
                $('#lnkHistorial').attr("href", mainUrl + "Entrevistas/Create/" + event.other.folio);

                $('#lnkNotas').parent().css("display", "block");
                $('#lnkNotas').attr("href", mainUrl + "notas/create/?PacienteId=" + event.other.paciente.PacienteId + "&CitaId=" + event.other.folio);
            }
            else
            {
                $('#lnkHistorial').css("display", "block");
                $('#lnkHistorial').attr("href", mainUrl + "Entrevistas/Create/" + event.other.folio);
                $('#lnkNotas').parent().css("display", "none");
            }
            //Cargar foto del empleado
            var promise = $.ajax({
                url: mainUrl + 'Usuarios/ImagenDelEmpleado/' + event.other.numeroempleado,
                type: 'GET',
                async: true,
                success: function (data) {
                },
                error: function (error) {
                    console.log('ERROR');
                    console.log(error);
                }
            });
            promise.done(function (data) {
                $('#imgEmpleado').attr('src', 'data:image/gif;base64,' + data);
                //modalInitial();
            });

            var EstadoId = event.other.bitacora[event.other.bitacora.length - 1].EstadoId;

            for (var i = 0; i < event.other.bitacora.length; i++) {
                $('#ltBitacora').append('<li class="p-l-5 p-t-5 font-montserrat"> ' + event.other.bitacora[i].Estado.Detalle + ' <span class="small-text hint-text"> ' + moment(event.other.bitacora[i].FechaModificado).format("YYYY/MM/DD hh:mm A") + ' </span> </li>')
            }

            if (event.readOnly || EstadoId >= 3) {
                $('#eventSave').attr('hidden', true);
                $('#eventAssisted').attr('hidden', true);
                $('#eventUnassisted').attr('hidden', true);
                $('#eventCanceled').attr('hidden', true);
            }
            //console.log(EstadoId);
            if (EstadoId === 7) {
                $('[data-index=' + event.index + ']').addClass('bg-danger');
                $('#lblBaja').attr('hidden', false);
            }
            selectedEvent = event;
        }
        $('#eventSave').on('click', function () {
            console.log(selectedEvent);

            if ($("body").hasClass('pending')) {
                return;
            }
            $('body').addClass('pending');

            $.ajax({
                type: "POST",
                url: mainUrl + "Calendario/UpdateEvent",
                data: {
                    'jsonEvento': JSON.stringify(selectedEvent),
                    'estado': null
                },
                success: function (result) {
                    console.log(result);
                    if (result.success) {
                        if (result.notify) {
                            notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icono, result.where);
                            selectedEvent = JSON.parse(result.evento);
                            $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
                            $("body").removeClass('pending');
                        }
                    }
                    else {
                        $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                        $("body").removeClass('pending');
                        return;
                    }
                },
                error: function (ajaxContext) {
                    $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                    $("body").removeClass('pending');
                }
            });

            //$('#myCalendar').pagescalendar('updateEvent', selectedEvent);
            //window.location = window.location;
            $('#myCalendar').pagescalendar('rebuild');
            $('#calendar-event').removeClass('open');
        });
        $('#eventAssisted').on('click', function () {
            console.log(selectedEvent);

            if ($("body").hasClass('pending')) {
                return;
            }
            $('body').addClass('pending');

            $.ajax({
                type: "POST",
                url: mainUrl + "Calendario/UpdateEvent",
                data: {
                    'jsonEvento': JSON.stringify(selectedEvent),
                    'estado': 3
                },
                success: function (result) {
                    console.log(result);
                    if (result.success) {
                        if (result.notify) {
                            notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icono, result.where);
                            selectedEvent = JSON.parse(result.evento);
                            $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
                            $("body").removeClass('pending');
                        }
                    }
                    else {
                        $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                        $("body").removeClass('pending');
                        return;
                    }
                },
                error: function (ajaxContext) {
                    $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                    $("body").removeClass('pending');
                }
            });

            $('#myCalendar').pagescalendar('rebuild');
            $('#calendar-event').removeClass('open');
        });
        $('#eventUnassisted').on('click', function () {
            console.log(selectedEvent);

            if ($("body").hasClass('pending')) {
                return;
            }
            $('body').addClass('pending');

            $.ajax({
                type: "POST",
                url: mainUrl + "Calendario/UpdateEvent",
                data: {
                    'jsonEvento': JSON.stringify(selectedEvent),
                    'estado': 4
                },
                success: function (result) {
                    console.log(result);
                    if (result.success) {
                        if (result.notify) {
                            notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icono, result.where);
                            selectedEvent = JSON.parse(result.evento);
                            $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
                            $("body").removeClass('pending');
                        }
                    }
                    else {
                        $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                        $("body").removeClass('pending');
                        return;
                    }
                },
                error: function (ajaxContext) {
                    $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                    $("body").removeClass('pending');
                }
            });

            $('#myCalendar').pagescalendar('rebuild');
            $('#calendar-event').removeClass('open');
        });
        $('#eventCanceled').on('click', function () {
            console.log(selectedEvent);

            if ($("body").hasClass('pending')) {
                return;
            }
            $('body').addClass('pending');

            $.ajax({
                type: "POST",
                url: mainUrl + "Calendario/UpdateEvent",
                data: {
                    'jsonEvento': JSON.stringify(selectedEvent),
                    'estado': 5
                },
                success: function (result) {
                    console.log(result);
                    if (result.success) {
                        if (result.notify) {
                            notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icono, result.where);
                            selectedEvent = JSON.parse(result.evento);
                            $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
                            $("body").removeClass('pending');
                        }
                    }
                    else {
                        $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                        $("body").removeClass('pending');
                        return;
                    }
                },
                error: function (ajaxContext) {
                    $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
                    $("body").removeClass('pending');
                }
            });

            $('#myCalendar').pagescalendar('rebuild');
            $('#calendar-event').removeClass('open');
        });
        $('#eventDelete').on('click', function () {
            $('#myCalendar').pagescalendar('removeEvent', $('#eventIndex').val());
            $('#calendar-event').removeClass('open');
        });
    });
})(window.jQuery);