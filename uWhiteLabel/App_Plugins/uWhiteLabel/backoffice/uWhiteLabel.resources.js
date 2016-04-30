angular.module("umbraco.resources")
 .factory("uWhiteLabelResource", function ($http) {
     return {
         getIFrameUrl: function () {
             return $http.get("backoffice/uWhiteLabel/Dashboard/iFrameData");
         },
         getHtml: function () {
             return $http.get("backoffice/uWhiteLabel/Dashboard/GetHtml");
         },
         saveIFrameUrl: function (url) {
             return $http.get("backoffice/uWhiteLabel/Dashboard/SaveiFrameData?url=" + url);
         },
         saveHtml: function (html) {
             return $http.post("backoffice/uWhiteLabel/Dashboard/SaveHtml", JSON.stringify(html));
         }
     };
 });