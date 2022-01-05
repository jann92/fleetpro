var app = angular.module("Home", ['ngStorage', 'angular-loading-bar', 'ngAnimate', 'ui.bootstrap']);

app.run(function ($rootScope, $location, $localStorage) {
    if ($localStorage.access_token == null || $localStorage.access_token == undefined) {

        localStorage.removeItem('ngStorage-access_token');
        localStorage.removeItem('ngStorage-moduleRoles');
     

        window.location = "login";
    }


});
