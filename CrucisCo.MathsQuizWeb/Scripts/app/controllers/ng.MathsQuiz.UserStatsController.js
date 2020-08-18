MathsQuizApp.controller('UserStatsController', ["$scope", "$http", function ($scope, $http) {
    var userStatsApi = "api/UserStatsApi/";
    $scope.errorCondition = false;
    $scope.users = [];
    $scope.selectedUser = null;
    $scope.hasUserStats = false;
    $scope.defaultStartDate = moment().startOf('day').subtract(1, 'months').toDate();
    $scope.defaultEndDate = moment().endOf('day').toDate();
    $scope.userStats = new Stats();

    $scope.startDate = $scope.defaultStartDate;
    $scope.endDate = $scope.defaultEndDate;
    $scope.hasUserStats = function () { return $scope.userStats && $scope.userStats.summary && $scope.userStats.multiplicationResults; };

    $http.post(userStatsApi + "GetUsers")
        .success(function (response) {
            $scope.users = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response.message;
        });

    

    $scope.getUserStats = function () {
        var statsSearch = new UserStatsSearch();
        statsSearch.user = $scope.selectedUser;
        statsSearch.fromDate = $scope.startDate;
        statsSearch.toDate = $scope.endDate;

        $scope.getUserResultsSummary(statsSearch);
        $scope.getMultiplicationResults(statsSearch);
        $scope.getDecimalResults(statsSearch);
    }

    $scope.getUserResultsSummary = function (statsSearch) {
        $http.post(userStatsApi + "GetUserResultsSummary", statsSearch)
        .success(function (response) {
            $scope.userStats.summary = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response.message;
        });
    };


    $scope.getMultiplicationResults = function (statsSearch) {
        $http.post(userStatsApi + 'GetMultiplicationQuizResults', statsSearch)
        .success(function (response) {
            // format date and as a new property 
            $(response).each(function (index, item) {
                item.quizDateFormatted = moment(item.quizDate).format('lll');
            });

            $scope.userStats.multiplicationResults = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response.message;
        });
    };

    $scope.getDecimalResults = function (statsSearch) {
        $http.post(userStatsApi + 'GetDecimalQuizResults', statsSearch)
        .success(function (response) {
            // format date and as a new property 
            $(response).each(function (index, item) {
                item.quizDateFormatted = moment(item.quizDate).format('lll');
            });

            $scope.userStats.decimalResults = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response.message;
        });
    };
}]);