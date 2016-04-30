'use strict';
(function () {
    //create the controller
    function uWhiteLabelWelcomeScreenController($scope, $routeParams, $http, uWhiteLabelResource, notificationsService) {
        //set a property on the scope equal to the current route id
        $scope.id = $routeParams.id;

        $scope.content = { tabs: [{ id: 1, label: "Help" }, { id: 2, label: "Iframe" }, { id: 3, label: "Basic" }] };

        uWhiteLabelResource.getIFrameUrl().then(function (response) {
            $scope.url = response.data.Url;
        });

        $scope.SaveIframe = function (url) {
            uWhiteLabelResource.saveIFrameUrl(url.$modelValue).then(function (response) {
                             notificationsService.success("Success","iFrame URL has been saved");
            }, function (response) {
                notificationsService.error("Error", "iFrame URL is not valid");
            });
        }

    };
    //register the controller
    angular.module("umbraco").controller('uWhiteLabel.Config.WelcomeScreenController', uWhiteLabelWelcomeScreenController);

})();