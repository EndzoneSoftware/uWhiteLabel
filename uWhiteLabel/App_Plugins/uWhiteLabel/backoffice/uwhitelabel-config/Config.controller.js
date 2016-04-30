﻿'use strict';
(function () {
    //create the controller
    function uWhiteLabelWelcomeScreenController($scope, $routeParams, $http, uWhiteLabelResource, notificationsService, navigationService) {

        navigationService.syncTree({ tree: 'uwhitelabel-config', path: ["-1", "2222"], forceReload: false });

        //set a property on the scope equal to the current route id
        $scope.id = $routeParams.id;

        var vm = this;


        $scope.content = { tabs: [{ id: 1, label: "Help" }, { id: 2, label: "Iframe" }, { id: 3, label: "Html" }] };

        uWhiteLabelResource.getIFrameUrl().then(function (response) {
            vm.url = response.data.Url;
        });
        uWhiteLabelResource.getHtml(true).then(function (response) {
            vm.html = response.data.Html;
        });

        vm.saveButtonState = "init";
        $scope.SaveIframe = function (saveUrl) {
            vm.saveButtonState = "busy";
            uWhiteLabelResource.saveIFrameUrl(saveUrl).then(function (response) {
                notificationsService.success("Success", "iFrame URL has been saved");
                vm.saveButtonState = "success";
            }, function (response) {
                notificationsService.error("Error", "iFrame URL is not valid");
                vm.saveButtonState = "error";
            });
        }

        vm.saveHtmlButtonState = "init";
        $scope.SaveHtml = function (saveHtml) {
            vm.saveHtmlButtonState = "busy";
            uWhiteLabelResource.saveHtml(saveHtml).then(function (response) {
                notificationsService.success("Success", "Your custom HTML has been saved");
                vm.saveHtmlButtonState = "success";
            }, function (response) {
                notificationsService.error("Error", "Unable to save your HTML");
                vm.saveHtmlButtonState = "error";
            });
        }
        $scope.GetDefaultHtml = function () {
            vm.resetHtmlButtonState = "busy";
            uWhiteLabelResource.getDefaultHtml().then(function (response) {
                vm.html = response.data.Html;
                vm.resetHtmlButtonState = "success";
            }, function (response) {
                notificationsService.error("Error", "Unable to get reset HTML");
                vm.resetHtmlButtonState = "error";
            });
            return $scope.html;
        }

    };
    //register the controller
    angular.module("umbraco").controller('uWhiteLabel.Config.WelcomeScreenController', uWhiteLabelWelcomeScreenController);

})();