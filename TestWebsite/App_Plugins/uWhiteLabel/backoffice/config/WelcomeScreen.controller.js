'use strict';
alert(1);
(function () {
    alert(2);
    //create the controller
    function uWhiteLabelWelcomeScreenController($scope, $routeParams, $http) {
        //set a property on the scope equal to the current route id
        $scope.id = $routeParams.id;
        $scope.test = "mystring";
        $scope.importOutput = "";


        $scope.runImport = function (importID) {
            console.log("I have called our REST API");

            // here is where we call our Import REST API
            // $scope.importOutput = "Run Successfully!"

            $http.get('http://localhost:57601/App_Plugins/UmbExtend/jsontest.html').
            success(function (data) {
                $scope.importOutput = data.content;
            });
        }

    };
    //register the controller
    angular.module("umbraco").controller('uWhiteLabel.Config.WelcomeScreenController', uWhiteLabelWelcomeScreenController);

})();