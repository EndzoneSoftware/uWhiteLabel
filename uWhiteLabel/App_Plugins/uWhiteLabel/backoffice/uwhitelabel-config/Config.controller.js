'use strict';
(function () {
    //create the controller
    function uWhiteLabelWelcomeScreenController($scope, $routeParams, $http, uWhiteLabelResource, notificationsService, navigationService) {

        navigationService.syncTree({ tree: 'uwhitelabel-config', path: ["-1", "2222"], forceReload: false });

        //set a property on the scope equal to the current route id
        $scope.id = $routeParams.id;

        var vm = this;

        vm.saveButtonState = "init";

        $scope.content = { tabs: [{ id: 1, label: "Help" }, { id: 2, label: "Iframe" }, { id: 3, label: "Html" }] };

        uWhiteLabelResource.getIFrameUrl().then(function (response) {
            $scope.url = response.data.Url;
        });
        uWhiteLabelResource.getHtml().then(function (response) {
            $scope.html = response.data.Html;
        });

        $scope.SaveIframe = function (url) {
            vm.saveButtonState = "busy";
            var saveUrl = (url.$modelValue) ? url.$modelValue : "";
            uWhiteLabelResource.saveIFrameUrl(saveUrl).then(function (response) {
                notificationsService.success("Success", "iFrame URL has been saved");
                vm.saveButtonState = "success";
            }, function (response) {
                notificationsService.error("Error", "iFrame URL is not valid");
                vm.saveButtonState = "error";
            });
        }
        $scope.SaveHtml = function (html) {
            vm.saveHtmlButtonState = "busy";
            var saveHtml = (html.$modelValue) ? html.$modelValue : "";
            uWhiteLabelResource.saveHtml(saveHtml).then(function (response) {
                notificationsService.success("Success", "Your custom HTML has been saved");
                vm.saveHtmlButtonState = "success";
            }, function (response) {
                notificationsService.error("Error", "Unable to save your HTML");
                vm.saveHtmlButtonState = "error";
            });
        }

    };
    //register the controller
    angular.module("umbraco").controller('uWhiteLabel.Config.WelcomeScreenController', uWhiteLabelWelcomeScreenController);

})();