Chart.defaults.global.colours = ["#2d4296", "#0f7411", "#DCDCDC", "#FDB45C", "#F7464A", "#949FB1", "#4D5360"];

MathsQuizApp.controller('MultiplicationQuizResultsController', ["$scope", "$http", function ($scope, $http) {
    var multiplicationApi = "api/MultiplicationApi/";

    $scope.getQuizResultsFromDb = function () {
        $http.post(multiplicationApi + 'GetQuizResultsFromDb', null)
        .success(function (response) {
            $scope.labels = getDatesFromResults(response);
            $scope.series = ['Answered', 'Correct'];

            $scope.data = [
              getAnsweredCountFromResults(response),
              getCorrectCountFromResults(response)
            ];
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });
    };

    $scope.getQuizResultsFromDb();

    getDatesFromResults = function (results) {
        var dates = [];
        $.each(results, function (index, item) {
            dates.push(moment(item.quizDate).format('lll'));
        });
        return dates;
    };

    getAnsweredCountFromResults = function (results) {
        var dates = [];
        $.each(results, function (index, item) {
            dates.push(item.answered);
        });
        return dates;
    };

    getCorrectCountFromResults = function (results) {
        var dates = [];
        $.each(results, function (index, item) {
            dates.push(item.correct);
        });
        return dates;
    };
}]);