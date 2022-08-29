angular.module('MedicalAppointmetsApp')
    .controller('Perfil', function ($scope, $http)
    {
        $scope.Password = {};
        $scope.UpdatePassword = function () {
            $http.post(mainUrl + "Perfil/ActualizarPassword", {
                username: $scope.Password.Username,
                pwd_actual: $scope.Password.PwdActual,
                pwd_new: $scope.Password.PwdNew,
                pwd_confirm: $scope.Password.PwdConfirm
            }).then(function (response) {
                //console.log(response);
                var result = response.data;
                if (result.success) {
                    notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icono, result.where);
                    $scope.Password.PwdActual = null;
                    $scope.Password.PwdNew = null;
                    $scope.Password.PwdConfirm = null;
                }
                else {
                    notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icono, result.where);
                }
            },
                function (err) {
                    notify('flip', '¡Error!', err.statusText, 'top-right', 0, 'danger', null, null);
                    $scope.Password = {};
                    console.log(err);
                });
        }
    });