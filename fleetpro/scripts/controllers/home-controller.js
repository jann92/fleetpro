app.controller('HomeController', ['$scope', '$localStorage', '$interval','$window','LoginFactory','$rootScope', function ($scope, $localStorage, $interval,$window, LoginFactory, $rootScope) {
    $scope.username = $localStorage.username;
    $scope.parentID = $localStorage.parentID;
    $scope.userID = $localStorage.userID;

    //app windows
    var win_TRP, win_TRA, win_RPT, win_ISP, win_MTP, win_CAM;


    if ($localStorage.moduleRoles == undefined) {

        LoginFactory.getRoleList(function (res) {

            $localStorage.moduleRoles = res;

            setRoles();


        }, function (err) {

        });

    } else {
        setRoles();
    }


    function setRoles() {
        $localStorage.moduleRoles.forEach(function (data) {
            if (data.Module === 'Asset') {
                $scope.Asset_Role = data;
            } if (data.Module === 'Landmarks') {
                $scope.Landmarks_Role = data;
            } if (data.Module === 'Zones') {
                $scope.Zone_Role = data;
            } if (data.Module === 'Delivery') {
                $scope.Delivery_Role = data;
            } if (data.Module === 'Driver') {
                $scope.Driver_Role = data;
            } if (data.Module === 'Reports') {
                $scope.Reports_Role = data;
            } if (data.Module === 'User Settings') {
                $scope.UserSettings_Role = data;
            } if (data.Module === 'Sensors') {
                $scope.Sensors_Role = data;
            } if (data.Module === 'Camera') {
                $scope.Camera_Role = data;
            } if (data.Module === 'Weather') {
                $scope.Weather_Role = data;
            } if (data.Module === 'Asset Assignment') {
                $scope.AssetAssignment_Role = data;
            } if (data.Module === 'Zone Assignment') {
                $scope.ZoneAssignment_Role = data;
            } if (data.Module === 'Asset Filter') {
                $scope.AssetFilter_Role = data;
            } if (data.Module === 'Asset Tracer') {
                $scope.AssetTracer_Role = data;
            } if (data.Module === 'Nearest Asset') {
                $scope.NearestAsset_Role = data;
            } if (data.Module === 'Measure Route') {
                $scope.MeasureRoute_Role = data;
            } if (data.Module === 'Driver Assignment') {
                $scope.DriverAssignment_Role = data;
            } if (data.Module === 'Route') {
                $scope.Route_Role.Read = true;
            } if (data.Module === 'InsightPro') {
                $scope.InsightPro_Role = data;
            } if (data.Module === 'MaintenancePro') {
                $scope.MaintenancePro_Role = data;
            } if (data.Module === 'FleetAdmin') {
                $scope.FleetAdmin_Role = data;
            } if (data.Module === 'BKB') {
                $scope.ServiceRequest_Role = data;
            } if (data.Module == 'CamPro') {
                $scope.CamPro_Role = data;
            } if (data.Module == 'DeliveryPro') {
                $scope.DP_Role = data;
            } if (data.Module == 'SensePro') {
                $scope.SensePro_Role = data;
            } if (data.Module == 'DHL') {
                $scope.DHL_Role = data;
            } if (data.Module == 'Metro Retail') {
                $scope.MetroRetail_Role = data;
            }

        });

    }


    $scope.selectItem = function (item) {
        switch (item) {
            case 'fleetpro':
                //$window.location.href = 'http://' + window.location.host + '/trackpro/track/monitor/';
                win_TRP = window.open('http://' + window.location.host + '/trackpro/track/monitor/', '_blank');
                //localStorage.setItem("win_TRP", win_TRP);
                //console.log(localStorage.getItem('win_TRP'))
                
                break;
            case 'reports':
                //$window.location.href = 'http://' + window.location.host + '/reportpro/';
                win_RPT = window.open('http://' + window.location.host + '/reportpro/', '_blank');
                break;
            case 'maintenance':
                //$window.location.href = 'http://' + window.location.host + '/maintenancepro/';
                win_MTP = window.open('http://' + window.location.host + '/maintenancepro/', '_blank');
                break;
            case 'insights':
                //$window.location.href = 'http://' + window.location.host + '/insightpro/';
                win_ISP = window.open('http://' + window.location.host + '/insightpro/', '_blank');
                break;
            case 'administration':
                //$window.location.href = 'http://' + window.location.host + '/fleetadmin/';
                win_TRA = window.open('http://' + window.location.host + '/fleetadmin/', '_blank');
                break;
            case 'bkb':
                //$window.location.href = 'http://' + window.location.host + '/bkb/';
                window.open('http://' + window.location.host + '/bkb/', '_blank');
                break;
            case 'campro':
                //$window.location.href = 'http://' + window.location.host + '/campro';
                win_CAM = window.open('http://' + window.location.host + '/campro', '_blank');
                break;
            case 'dhl':
                //$window.location.href = 'http://' + window.location.host + '/dhl';
                window.open('http://' + window.location.host + '/fleetprodhl', '_blank');
                break;
            case 'dp':
                //$window.location.href = 'http://' + window.location.host + '/deliverypro';
                //window.open('http://' + window.location.host + '/deliverypro', '_blank');
                window.open('http://login.philgps.com/deliveryprobeta', '_blank');
                break;
            case 'sensepro':
                //$window.location.href = 'http://' + window.location.host + '/sensepro';
                window.open('http://' + window.location.host + '/sensepro', '_blank');
                break;
            case 'metroretail':
                window.open('http://deliverypro.philgps.com/metroretail/');
                break;
            default:
        }
    }


    $scope.logout = function () {
        // $.getJSON("https://jsonip.com?callback=?", function (ip) {


        LoginFactory.getClientIP(function(ip){

            var param = { IPAdd: ip.ip };

            LoginFactory.logout(param, function (data) {

                //fleetpro
                localStorage.removeItem('ngStorage-access_token');
                localStorage.removeItem('ngStorage-expires_in');
                localStorage.removeItem('ngStorage-serverCode');
                localStorage.removeItem('ngStorage-defaultCoordinates');
                localStorage.removeItem('ngStorage-imagesURL');
                localStorage.removeItem('ngStorage-roles');
                localStorage.removeItem('ngStorage-moduleRoles');
                localStorage.removeItem('ngStorage-userEmail');

                //trakpro
                localStorage.removeItem('ngStorage-access_token_TRP');
                localStorage.removeItem('ngStorage-expires_in_TRP');
                localStorage.removeItem('ngStorage-serverCode_TRP');
                localStorage.removeItem('ngStorage-defaultCoordinates_TRP');
                localStorage.removeItem('ngStorage-moduleRoles');
                localStorage.removeItem('ngStorage-username_TRP');

                //reportpro
                localStorage.removeItem('currentUser_RPT');

                //fleetadmin
                localStorage.removeItem('ngStorage-access_token_TRA');
                localStorage.removeItem('ngStorage-expires_in_TRA');
                localStorage.removeItem('ngStorage-serverCode_TRA');
                localStorage.removeItem('ngStorage-defaultCoordinates_TRA');
                localStorage.removeItem('ngStorage-moduleRoles_TRA');
                localStorage.removeItem('ngStorage-username_TRA');

                //maintenance
                localStorage.removeItem('currentUser_MTP');

                //insightpro
                localStorage.removeItem('currentUser_ISP');

                //campro
                localStorage.removeItem('currentUser_CAM');

                //reload windows app
                //$scope.reloadApps();
           

                location.href = "login/";

            }, function (err) {

                //fleetpro
                localStorage.removeItem('ngStorage-access_token');
                localStorage.removeItem('ngStorage-expires_in');
                localStorage.removeItem('ngStorage-serverCode');
                localStorage.removeItem('ngStorage-defaultCoordinates');
                localStorage.removeItem('ngStorage-imagesURL');
                localStorage.removeItem('ngStorage-roles');
                localStorage.removeItem('ngStorage-moduleRoles');
                localStorage.removeItem('ngStorage-userEmail');

                //trakpro
                localStorage.removeItem('ngStorage-access_token_TRP');
                localStorage.removeItem('ngStorage-expires_in_TRP');
                localStorage.removeItem('ngStorage-serverCode_TRP');
                localStorage.removeItem('ngStorage-defaultCoordinates_TRP');
                localStorage.removeItem('ngStorage-moduleRoles');
                localStorage.removeItem('ngStorage-username_TRP');

                //reportpro
                localStorage.removeItem('currentUser_RPT');

                //fleetadmin
                localStorage.removeItem('ngStorage-access_toke_TRA');
                localStorage.removeItem('ngStorage-expires_in_TRA');
                localStorage.removeItem('ngStorage-serverCode_TRA');
                localStorage.removeItem('ngStorage-defaultCoordinates_TRA');
                localStorage.removeItem('ngStorage-moduleRoles_TRA');
                localStorage.removeItem('ngStorage-username_TRA');

                //maintenance
                localStorage.removeItem('currentUser_MTP');

                //insightpro
                localStorage.removeItem('currentUser_ISP');

                //campro
                localStorage.removeItem('currentUser_CAM');

                location.href = "login/";
            });
        }, function (error) {

        });

      
    }

    $scope.reloadApps = function(){
        if (win_TRP != null) {
            win_TRP.location.reload();
        }
        if (win_TRA != null) {
            win_TRA.location.reload();
        }
        if (win_RPT != null) {
            win_RPT.location.reload();
        }
        if (win_MTP != null) {
            win_MTP.location.reload();
        }
        if (win_ISP != null) {
            win_ISP.location.reload();
        }
        if (win_CAM != null) {
            win_CAM.location.reload();
        }
    }

    $rootScope.$on("reloadApps", function (event, result) {
        $scope.reloadApps();
    });


}]);