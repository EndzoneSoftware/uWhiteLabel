angular.module("umbraco").controller("uWhiteLabel.DashboardController", 
	function ($scope, $http, uWhiteLabelResource, notificationsService) {

	    $scope.mode = "default";

	    uWhiteLabelResource.getIFrameUrl().then(function (response) {
	        if (response.data.HasIframe) {
	            $scope.Url = response.data.Url;
	            $scope.mode = "iframe";
	        }
		});


	}
);