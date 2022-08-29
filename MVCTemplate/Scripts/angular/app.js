/*global angular */
var mainUrl;
/**
 *
 * @type {angular.Module}
 */
angular.module('MedicalAppointmetsApp', [])
    .config(function () {
        'use strict';
    })
    .filter('ageFilter', function () {
        function calculateAge(birthday) { // birthday is a date
            if (birthday !== null) {
                var ageDifMs = Date.now() - new Date(birthday).getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch
                return Math.abs(ageDate.getUTCFullYear() - 1970);
            }
        }
        function monthDiff(d1, d2) {
            if (d1 < d2) {
                var months = d2.getMonth() - d1.getMonth();
                return months <= 0 ? 0 : months;
            }
            return 0;
        }
        return function (birthdate) {
            if (birthdate !== null) {
                var age = calculateAge(birthdate);
                if (age === 0)
                    return monthDiff(birthdate, new Date()) + ' meses';
                return age + " años";
            }
            
        };
    });