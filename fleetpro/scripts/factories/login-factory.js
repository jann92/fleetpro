'use strict';
app.factory('LoginFactory', ['$http', '$localStorage', '$filter', function ($http, $localStorage, $filter) {
    var baseUrl = "../";
    var accountSensorLabelsUrl = "../" + "Account/User/SensorLabels";
    var _serverList = "../" + "Server/List/trp";
    var utilitiesAPIMailSendUrl = "http://login.philgps.com/UtilitiesAPI/Mail/Send";
    var accountLogoutURL = "Account/Logout";
    var getAccountParentID = "../Account/Get/ParentID";

    var headers = function (contentType) {
        return {
            'content-type': contentType,
            'accept': 'application/json'
        };
    }

    return {
        login: function (loginUrl, formData, success, error) {
            
            $.ajax({
                method: 'POST',
                url: loginUrl,
                data: formData,
                header: headers('application/x-www-form-url-encoded')
            }).success(success).error(error)

        },
        getServerList: function () {
            return $http.get(_serverList);
        },
        getRolesList: function (success, error) {
            var _getroles = "../Account/Module/List";
            var headerToken = headers('application/json');
            headerToken.authorization = 'Bearer ' + $localStorage.access_token;
            $http.get(_getroles, { headers: headerToken }).success(success).error(error);
        },
        getAccountUserSensorLabels: function (success, error) {
            var headerToken = headers('application/json');
            headerToken.authorization = 'Bearer ' + $localStorage.access_token;
            $http.get(accountSensorLabelsUrl, { headers: headerToken }).success(success).error(error);
        },
        getUserEmail: function (success, error) {

            var getUserEmailUrl = "../" + "Account/User/Email";

            var headerToken = headers('application/json');
            headerToken.authorization = 'Bearer ' + $localStorage.access_token;
            $http.get(getUserEmailUrl, { headers: headerToken }).success(success).error(error);
        },
        utilitiesAPIMailSend: function (param, success, error) {
            var headerToken = headers('application/json');
            $http.post(utilitiesAPIMailSendUrl, param, { headers: headerToken }).success(success).error(error);
        },
        logout: function(param , success,error){
            var headerToken = headers('application/json');
            headerToken.authorization = 'Bearer ' + $localStorage.access_token;
            $http.post(accountLogoutURL, param, { headers: headerToken }).success(success).error(error);
        },
        getClientIP: function (success, error) {
            var headerToken = headers('application/json');
            //headerToken.authorization = 'Bearer ' + $localStorage.access_token;
            $http.get("https://api.ipify.org/?format=json", headerToken).success(success).error(error);
        },
        getParentID: function(success, error){
            var headerToken = headers('application/json');
            headerToken.authorization = 'Bearer ' + $localStorage.access_token;
            $http.get(getAccountParentID, { headers: headerToken }).success(success).error(error);

        },
        createAlert: function (title, message) {
            $('#alert_placeholder').show('slide');
            message = (message == null ? "Please contact your administrator for more information." : message);
            $('#alert_placeholder').html('<div class="alert alert-custom"><div class="dismiss alert-custom-header"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button><span class="font-15px">' + title + '</span></div><div class="alert-custom-body"><span class="font-11px">' + message + '</span></div></div>')
            setTimeout(function () {
                $('#alert_placeholder').hide('slide');
            }, 8000);
        },

    }
}]);