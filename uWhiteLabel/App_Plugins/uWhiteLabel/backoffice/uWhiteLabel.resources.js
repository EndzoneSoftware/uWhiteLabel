angular.module("umbraco.resources")
 .factory("uWhiteLabelResource", function ($http) {
     return {
         getIFrameUrl: function () {
             return $http.get("backoffice/uWhiteLabel/Dashboard/iFrameData");
         },
         getHtml: function (useDefault) {
             return $http.get("backoffice/uWhiteLabel/Dashboard/GetHtml?useDefault=" + (useDefault ? "true" : "false"));
         },
         saveIFrameUrl: function (url) {
             return $http.get("backoffice/uWhiteLabel/Dashboard/SaveiFrameData?url=" + url);
         },
         saveHtml: function (html) {
             return $http.post("backoffice/uWhiteLabel/Dashboard/SaveHtml", JSON.stringify(html));
         }
     };
 });