angular.module("umbraco.resources")
 .factory("uWhiteLabelResource", function ($http) {
     return {
         getIFrameUrl: function () {
             return $http.get("backoffice/uWhiteLabel/Dashboard/iFrameData");
         },
         saveIFrameUrl: function (url) {
             return $http.get("backoffice/uWhiteLabel/Dashboard/SaveiFrameData?url="+url);
         }
     };
 });