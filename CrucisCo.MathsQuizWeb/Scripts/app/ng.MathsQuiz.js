var MathsQuizApp = angular.module('MathsQuizApp', ['ngAnimate', 'ui.bootstrap', 'chart.js', 'ui.slider']);

// focus for elements from controllers
MathsQuizApp.factory('callFocus', ['$timeout', '$window', function ($timeout, $window) {
    return function (id) {
        // timeout makes sure that is invoked after any other event has been triggered.
        // e.g. click events that need to run before the focus or
        // inputs elements that are in a disabled state but are enabled when those events
        // are triggered.
        $timeout(function () {
            var element = $window.document.getElementById(id);
            if (element)
                element.focus();
        });
    };
}]);

// focus via declaration (attribute)
MathsQuizApp.directive('eventFocus', ['callFocus', function (callFocus) {
    return function (scope, elem, attr) {
        elem.on(attr.eventFocus, function () {
            callFocus(attr.eventFocusId);
        });

        // Removes bound events in the element itself
        // when the scope is destroyed
        scope.$on('$destroy', function () {
            element.off(attr.eventFocus);
        });
    };
}]);