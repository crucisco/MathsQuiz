MathsQuizApp.filter('correctResults', function () {
    return function (items) {
        var filtered = [];
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.isCorrect) {
                filtered.push(item);
            }
        }
        return filtered;
    };
});

MathsQuizApp.filter('toQuizDatetimeArray', function () {
    return function (items) {
        var dates = [];
        $.each(items, function (index, item) {
            dates.push(item.quizDate);
        });
        return dates;
    }
});

MathsQuizApp.filter('toPowBase10', function () {
    return function(index){
        return Math.pow(10, index);
    }
})