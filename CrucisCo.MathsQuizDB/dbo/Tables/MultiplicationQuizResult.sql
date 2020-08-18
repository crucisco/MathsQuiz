
CREATE TABLE [dbo].[MultiplicationQuizResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuizUser] [varchar](150) NOT NULL,
	[QuizDate] [datetime] NOT NULL,
	[TimesTable] VARCHAR(50) NULL,
	[Answered] [int] NOT NULL,
	[Correct] [int] NOT NULL,
 CONSTRAINT [PK_MultiplicationQuizResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
