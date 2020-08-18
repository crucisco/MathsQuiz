CREATE PROCEDURE [dbo].[GetSummaryStats]	
	@quizUser varchar(50), @fromDate datetime, @toDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @minMultiplicationAnswers int = 25
	declare @multiplicationQuizzesOverMinAnswers int
	declare @minDecimalAnswers int = 10
	declare @decimalQuizzesOverMinAnswers int

	select @multiplicationQuizzesOverMinAnswers = count(mqr.Id) 
	from MultiplicationQuizResult mqr 
	where mqr.QuizUser = @quizUser 
	and mqr.Correct > @minMultiplicationAnswers 
	and mqr.QuizDate between @fromDate and @toDate

	select @decimalQuizzesOverMinAnswers = count(dqr.Id) 
	from DecimalQuizResult dqr 
	where dqr.QuizUser = @quizUser 
	and dqr.Correct > @minDecimalAnswers 
	and dqr.QuizDate between @fromDate and @toDate

	select CONCAT('Multiplication (min ', @minMultiplicationAnswers, ' correct/quiz)') as QuizType, count(mqr.Id) as QuizzesTaken, @multiplicationQuizzesOverMinAnswers as QuizzesOverMinAnswers, AVG(mqr.Answered) as AvgAnswersPerQuiz, AVG(mqr.Correct) as AvgCorrectPerQuiz, SUM(mqr.Answered) as TotalQuestionsAnswered, SUM(mqr.Correct) as TotalCorrect 
	from MultiplicationQuizResult mqr
	where mqr.QuizUser = @quizUser  
	and mqr.QuizDate between @fromDate and @toDate
	UNION
	select CONCAT('Decimal (min ', @minDecimalAnswers, ' correct/quiz)') as QuizType, count(dqr.Id) as QuizzesTaken, @decimalQuizzesOverMinAnswers as QuizzesOverMinAnswers, AVG(dqr.Answered) as AvgAnswersPerQuiz, AVG(dqr.Correct) as AvgCorrectPerQuiz, SUM(dqr.Answered) as TotalQuestionsAnswered, SUM(dqr.Correct) as TotalCorrect 
	from DecimalQuizResult dqr
	where dqr.QuizUser = @quizUser  
	and dqr.QuizDate between @fromDate and @toDate

END
GO

