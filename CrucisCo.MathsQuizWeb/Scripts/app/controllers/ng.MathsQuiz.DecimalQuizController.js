function DecimalQuizController(DecimalsQuizTime, callFocus, $scope, $http) {
    var decimalApi = "api/DecimalApi/";
    var maxQuizTime = DecimalsQuizTime;
    $scope.operatorIndices = [];
    $scope.operatorMinIndex;
    $scope.operatorMaxIndex;
    $scope.quizRunning = false;
    $scope.selectedOperators = [];
    $scope.errorCondition = false;
    $scope.quizStatus = 'info';
    $scope.maxQuizTime = maxQuizTime;
    $scope.quizTime = maxQuizTime;
    $scope.results = [];

    $http.post(decimalApi + "GetDecimalOperators", null)
        .success(function (response) {
            $scope.operatorIndices = response;
            $scope.operatorMinIndex = getOperatorMinIndex();
            $scope.operatorMaxIndex = getOperatorMaxIndex();
            $scope.resetSelectedOperators();
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });

    $scope.reset = function () {
        $scope.quizRunning = false;
        $scope.resetSelectedOperators();
    };

    $scope.toggleAllOperators = function () {
        if ($scope.selectedOperators.length > 0) {
            $scope.selectedOperators = [];
        } else {
            for (item in $scope.operatorIndices) {
                $scope.selectedOperators.push(item);
            }
        }
    };

    $scope.resetSelectedOperators = function () {
        $scope.selectedOperators[0] = $scope.operatorMinIndex + 1;
        $scope.selectedOperators[1] = $scope.operatorMaxIndex - 1;
    };

    $scope.setQuizOperators = function () {
        $http.post(decimalApi + 'SetQuizOperators', $scope.selectedOperators)
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
        $http.post(decimalApi + "GetProblem")
        .success(function (response) {
            $scope.problem = response;
        })
        .error(function (response) {
            $scope.errorCondition = true;
            $scope.errorMessage = response;
        });
    };

    $scope.setAnswer = function () {
        // set the operator index to a string to avoid the problem with binding to enum on server
        $scope.problem.operatorIndex = $scope.problem.operatorIndex.toString();
        $scope.problem.problemOperation = $scope.problem.problemOperation.toString();
        $http.post(decimalApi + "SetAnswer", $scope.problem)
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
        $http.post(decimalApi + 'GetResults', null)
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


    getOperatorMinIndex = function () {
        var min = 0;
        for(op in $scope.operatorIndices){
            if ($scope.operatorIndices.hasOwnProperty(op)) {
                var val = parseInt($scope.operatorIndices[op]);
                if (min > val) {
                    min = val;
                }
            }
        }

        return min;
    }

    getOperatorMaxIndex = function () {
        var max = 0;
        for (op in $scope.operatorIndices) {
            if ($scope.operatorIndices.hasOwnProperty(op)) {
                var val = parseInt($scope.operatorIndices[op]);
                if (max < val) {
                    max = val;
                }
            }
        }

        return max;
    }
};

DecimalQuizController.$inject = ['DecimalsQuizTime', 'callFocus', '$scope', '$http'];

MathsQuizApp.controller('DecimalQuizController', DecimalQuizController);
