using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using SocionicTeamBuilder.Mobile.Views;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(TestingId), nameof(TestingId))]
    public class TestPassingViewModel: BaseViewModel
    {
        private int testingId;

        private int questionNumber;
        private int questionTotalCount;
        private string questionTitle;

        private string questionText;

        private Question question;
        private readonly Dictionary<string, int> scores;

        public TestPassingViewModel()
        {
            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();
            scores = new Dictionary<string, int>();

            AnswerTapped = new Command<Answer>(OnAnswerSelectedAsync);
            ExitCommand = new Command(OnExitSelectedAsync);
        }

        public ObservableCollection<Answer> Answers { get; }
        public ObservableCollection<Question> Questions { get; }
        public Command<Answer> AnswerTapped { get; }
        public Command ExitCommand { get; }

        public int TestingId
        {
            get => testingId;
            set
            {
                testingId = value;
                LoadTestingAsync();
            }
        }

        public Question Question
        {
            get => question;
            set
            {
                question = value;
                QuestionNumber = value.Number;
                QuestionText = value.Text;
                QuestionTitle = QuestionNumber + "/" + QuestionTotalCount;
            }
        }

        public int QuestionNumber
        {
            get => questionNumber;
            set => SetProperty(ref questionNumber, value);
        }

        public int QuestionTotalCount
        {
            get => questionTotalCount;
            set => SetProperty(ref questionTotalCount, value);
        }

        public string QuestionTitle
        {
            get => questionTitle;
            set => SetProperty(ref questionTitle, value);
        }

        public string QuestionText
        {
            get => questionText;
            set => SetProperty(ref questionText, value);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnExitSelectedAsync(object obj)
        {
            string title = TranslateExtension.GetValue("ExitTestTitle");
            string msg = TranslateExtension.GetValue("ExitTestMsg");
            string yes = TranslateExtension.GetValue("YesBtn");
            string no = TranslateExtension.GetValue("NoBtn");

            bool answer = await Application.Current.MainPage.DisplayAlert(title, msg, yes, no);
            if (answer)
            {
                await Shell.Current.GoToAsync($"///{nameof(SocionicTypePage)}");
            }
        }

        private void OnAnswerSelectedAsync(Answer answer)
        {
            GetScore(answer.Score);
            ExecuteLoadNextQuestion();
        }

        private void GetScore(int score)
        {
            string dichotomy = Question.DichotomyAbbreveation;
            if (scores.ContainsKey(dichotomy))
            {
                scores[dichotomy] += score;
            }
            else
            {
                scores.Add(dichotomy, score);
            }

            Debug.WriteLine(Question.Number + ": " + score);
        }

        private async void LoadTestingAsync()
        {
            IsBusy = true;

            try
            {
                await LoadQuestionsAsync();
                await LoadAnswersAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadQuestionsAsync()
        {
            Questions.Clear();
            var questions = await TestingService.GetQuestions(TestingId);
            foreach (var q in questions)
            {
                Questions.Add(q);
            }

            QuestionTotalCount = Questions.Count();
            Question = Questions.FirstOrDefault();
        }

        private async Task LoadAnswersAsync()
        {
            Answers.Clear();
            var answers = await TestingService.GetAnswers(TestingId, QuestionNumber);
            foreach (var a in answers)
            {
                Answers.Add(a);
            }
        }

        private async void ExecuteLoadNextQuestion()
        {
            IsBusy = true;

            try
            {
                ++QuestionNumber;
                var question = Questions.Where(q => q.Number == QuestionNumber).SingleOrDefault();

                if (question == null)
                {
                    FinishTesting();
                    return;
                }

                Question = question;
                await LoadAnswersAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void FinishTesting()
        {
            Debug.WriteLine("Finish testing");

            string title = TranslateExtension.GetValue("TestResTitle");
            string msg = TranslateExtension.GetValue("TestResMsg");
            string ok = TranslateExtension.GetValue("DetailsBtn");

            string socionicType = await TestingService.GetResult(TestingId, App.EmployeeId, scores);
            await Application.Current.MainPage.DisplayAlert(title, msg + socionicType, ok);

            await Shell.Current.GoToAsync($"/{nameof(SocionicTypePage)}");
        }
    }
}