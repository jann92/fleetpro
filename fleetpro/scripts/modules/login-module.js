var app = angular.module("Login", ['ngStorage', 'angular-loading-bar', 'ngAnimate', 'ui.bootstrap']);
app.run(function ($rootScope, $location, $localStorage, LoginFactory) {
    $rootScope.$on("$locationChangeStart", function (event, next, current) {
        if ($localStorage.access_token != null) {
    
            window.location = "../";
          
        } else {
      
        }
    });
});

//Forgot Password Modal
//app.directive('forgotPasswordModal', function () {
//    return {
//        restrict: "EAC",
//        templateUrl: '../views/modal/modal-forgotpassword.html',
//    }
//});