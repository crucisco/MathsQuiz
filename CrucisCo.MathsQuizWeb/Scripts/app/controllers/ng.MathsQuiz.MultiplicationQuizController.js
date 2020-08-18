function MultiplicationQuizController(MultiplicationQuizTime, callFocus, $scope, $http) {
    var multiplicationApi = "api/MultiplicationApi/";
    var maxQuizTime = MultiplicationQuizTime;
    $scope.tables = [];
    $scope.quizRunning = false;
    $scope.selectedTables = [];
    $scope.errorCondition = false;
    $scope.quizStatus = 'info';
    $scope.maxQuizTime = maxQuizTime;
    $scope.quizTime = maxQuizTime;
    $scope.results = [];

    $http.post(multiplicationApi + "GetTables", null)
        .success(function (response) {
            $scope.tables = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });

    $scope.reset = function () {
        $scope.quizRunning = false;
        $scope.selectedTables = [];
    };

    $scope.toggleAllTables = function () {
        if ($scope.selectedTables.length > 0) {
            $scope.selectedTables = [];
        } else {
            for (item in $scope.tables) {
                $scope.selectedTables.push(item);
            }
        }
    };

    $scope.toggleTableSelection = function (table) {
        var tableIndex = $scope.selectedTables.indexOf(table);

        // if the item exists, remove it from the array
        if (tableIndex > -1) {
            $scope.selectedTables.splice(tableIndex, 1); // remove the table
        } else {
            $scope.selectedTables.push(table);
        }
    };

    $scope.setQuizMultiple = function () {
        $http.post(multiplicationApi + 'SetQuizMultiple', $scope.selectedTables)
        .success(function (response) {
            $scope.quizRunning = response;
            $scope.getProblem();
            $scope.startTimer();
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });
    };

    $scope.getProblem = function () {
        $http.post(multiplicationApi + "GetProblem")
        .success(function (response) {
            $scope.problem = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });
    };

    $scope.setAnswer = function () {
        $http.post(multiplicationApi + "SetAnswer", $scope.problem)
        .success(function (response) {
            $scope.getProblem();
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });

        callFocus("txtAnswer");
    };

    $scope.startTimer = function () {
        $scope.quizTime = maxQuizTime;
        $scope.quizStatus = 'success';
        var wTimer = window.setInterval(function () {
            $scope.quizTime = $scope.quizTime - 1;
            if ($scope.quizTime < (maxQuizTime * 2 / 3)) {
                $scope.quizStatus = 'warning';
            }
            if ($scope.quizTime < (maxQuizTime * 1 / 3)) {
                $scope.quizStatus = 'danger';
            }
            if ($scope.quizTime === 0) {
                window.clearInterval(wTimer);
                $scope.quizRunning = false;
                $scope.getResults();
            }
            $scope.$apply($scope.quizTime);
            $scope.$apply($scope.quizStatus);
        }, 1000);
    };

    $scope.getResults = function () {
        $http.post(multiplicationApi + 'GetResults', null)
        .success(function (response) {
            $scope.results = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });
    };

    $scope.checkKeyAndSetAnswer = function () {
        if (event.which === 13) {
            $scope.setAnswer();
        }
    };
};

MultiplicationQuizController.$inject = ['MultiplicationQuizTime', 'callFocus', '$scope', '$http'];

MathsQuizApp.controller('MultiplicationQuizController', MultiplicationQuizController);
