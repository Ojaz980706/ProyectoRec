angular.module('MedicalAppointmetsApp')
    .controller('Citas', function ($scope, $http) {
        $scope.numeroEmpleado = null;
        $scope.Usuario = [];
        $scope.date = new Date();
        $scope.Paciente = {};
        $scope.Pacientes = {};
        $scope.EspecialidadId = 0;
        $scope.Motivos = {};
        $scope.Medicos = {};
        $scope.PacienteRequired = true;
        $scope.minFechaCita = moment().format('YYYY-MM-DDT00:00');

        $scope.buscarInformacionEmpleado = function () {
            $('#progressBusquedaEmpleado').show();
            //console.log($scope.numeroEmpleado);
            if ($scope.numeroEmpleado !== null) {
                $http.post(mainUrl + "Citas/GetInformacionEmpleadoPorID", {
                    ID: $scope.numeroEmpleado
                }).then(function (response) {
                    //console.log(response);
                    if (response.data === "") {
                        $scope.numeroEmpleado = null;
                        notify('circle', '¡Error!', 'Usuario no encontrado.', 'top-right', 2500, 'danger', null, null);

                        $scope.Usuario = [];
                        $('[ng-model=numeroEmpleado]').focus();
                    }
                    else {
                        $scope.Usuario = response.data;
                        notify('flip', '', 'Usuario encontrado correctamente', 'top-right', 2500, 'success', null, null);
                    }
                    $('#progressBusquedaEmpleado').hide();
                    //console.log($scope.Usuario);
                },
                    function (err) {
                        notify('flip', '¡Error!', err.statusText, 'top-right', 0, 'danger', null, null);
                        $scope.numeroEmpleado = null;
                        $scope.Usuario = [];
                        console.log(err);
                        //$('[ng-model=numeroEmpleado]').focus();

                    });
            }
            $('#progressBusquedaEmpleado').hide();
        };

        $scope.CrearSelect = function () {
            $scope.GetPacientes();
            if ($scope.Pacientes.length === 0) {
                $('[ng-model="DDLPacientes"]').find('option').text('No se encontraron resultados');
                $('[ng-model="DDLPacientes"]').select2();
            }
            else {
                $('[ng-model="DDLPacientes"]').select2();
            }
        };

        $scope.GetPacientes = function () {
            if ($scope.numeroEmpleado !== null) {
                $http.post(mainUrl + "Citas/GetPacientesDelEmpleado",
                    {
                        id: $scope.numeroEmpleado
                    }).then(function (response) {
                        $scope.Paciente = {};
                        $scope.Pacientes = response.data;
                    });
            }
        };

        $scope.GetInformacionDelPaciente = function () {
            //console.log($scope.DDLPacientes);
            if ($scope.numeroEmpleado !== null && $scope.DDLPacientes !== null && $scope.DDLPacientes !== undefined) {
                $http.post(mainUrl + "Citas/GetInformacionPaciente",
                    {
                        id: $scope.DDLPacientes
                    }).then(function (response) {
                        //console.log(response);
                        $scope.Paciente = JSON.parse(response.data);
                    });
            }
            else {
                $scope.Paciente = {};
            }
        };

        $scope.UpdatePaciente = function () {
            $scope.GetInformacionDelPaciente();
        };

        $scope.GetEspecialidades = function () {
            $http.get(mainUrl + "Citas/GetEspecialidades")
                .then(function (response) {
                    //console.log(response);
                    if (response.data !== null) {
                        $scope.Especialidades = JSON.parse(response.data);
                    }
                });
        };


        $scope.GetMotivos = function () {
            $http.get(mainUrl + "Citas/GetMotivos").then(function (response) {
                //console.log(response);
                if (response.data !== null) {
                    $scope.Motivos = JSON.parse(response.data);
                }
            });
        };

        $scope.GetDuracionEspecialidad = function (id) {
            $http.post(mainUrl + "Citas/GetDuracionPorMotivoId",
                {
                    MotivoId: id
                }).then(function (response) {
                    console.log(response);
                    $scope.Duracion = response.data + " minutos aprox.";
                });
        };

        $scope.GetMotivos();

        $scope.CargarMedicos = function () {
            for (var i = 0; i < $scope.Motivos.length; i++) {
                if ($scope.DDLMotivos === $scope.Motivos[i].MotivoId) {
                    var motivo = $scope.Motivos[i];
                    console.log(motivo);
                    // Especificar la duracion de las citas.
                    $scope.GetDuracionEspecialidad(motivo.MotivoId);
                }
            }
            $scope.GetMedicosPorMotivoId();
        };
        $scope.GetMedicosPorMotivoId = function () {
            if ($scope.DDLMotivos !== null) {
                $http.post(mainUrl + "Citas/GetMedicosPorMotivoId/",
                    {
                        id: $scope.DDLMotivos
                    })
                    .then(function (response) {
                        //console.log(response);
                        if (response.data !== null) {
                            //console.log(response.data);
                            $scope.Medicos = JSON.parse(response.data);
                        }
                        else {
                            $('[ng-model="DDLMedicos"]').find('option').text('No se encontraron resultados');
                            $('[ng-model="DDLMedicos"]').select2();
                        }
                    });
            }
            else {
                $scope.Medicos = {};
            }
        };

        $scope.ValidarEsPersonal = function () {
            //console.log($scope.EsPersonal);
            if ($scope.EsPersonal) {
                $('[ng-model="DDLPacientes"]').attr('disabled', true).parent('div').attr('class', 'form-group form-group-default disabled');
                $('[ng-model="DDLPacientes"]').attr('required', false);
                $scope.PacienteRequired = false;
                $('[ng-model="btnSyncPacientes"]').hide();

            } else {
                $('[ng-model="DDLPacientes"]').attr('disabled', false).parent('div').attr('class', 'form-group form-group-default');
                $('[ng-model="DDLPacientes"]').attr('required', true);
                $scope.PacienteRequired = true;
                $('[ng-model="btnSyncPacientes"]').show();
            }
        };


        $scope.sendForm = function (e)
        {
            console.log(e);
            e.preventDefault();
            $http.post(mainUrl + "Citas/ValidarCitaAjax/",
                {
                    numeroempleado: $scope.numeroEmpleado,
                    motivo: $scope.DDLMotivos,
                    medico: $scope.DDLMedicos,
                    fecha: $scope.FechaCita
                })
                .then(function (response)
                {
                    console.log(response);
                    if (response.data) {
                        e.currentTarget.submit();
                    }
                    
                }, function (err) {
                    notify('flip', '¡Error!', err.statusText, 'top-right', 0, 'danger', null, null);
                    console.log(err);

                });
        };

    });