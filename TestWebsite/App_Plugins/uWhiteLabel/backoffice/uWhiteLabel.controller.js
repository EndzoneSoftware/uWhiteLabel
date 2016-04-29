angular.module("umbraco").controller("uWhiteLabel.DashboardController", 
	function ($scope, $http, uWhiteLabelResource, notificationsService) {

	    uWhiteLabelResource.getIFrameUrl().then(function (response) {
			$scope.Url = response.data.Url;
		}, function (response) {
			$scope.Url = "error";
			notificationsService.error("Error", "uWhiteLabel dashboard URL not configured (or incorrect) in web.config");
		});


	}
);