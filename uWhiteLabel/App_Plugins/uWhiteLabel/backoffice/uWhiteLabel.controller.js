angular.module("umbraco").controller("uWhiteLabel.DashboardController", 
	function ($scope, $http, notificationsService) {

		$http.get("backoffice/uWhiteLabel/Dashboard/iFrameData").then(function (response) {
			$scope.Url = response.data.Url;
		}, function (response) {
			$scope.Url = "error";
			notificationsService.error("Error", "uWhiteLabel dashboard URL not configured (or incorrect) in web.config");
		});


	}
);