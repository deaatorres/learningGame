﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Models;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        private List<Question> questions;
        private Question currentQuestion;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();
            CheckAnswerButton.Click += (o, e) => CheckAnswer();
            AnswerBox.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    CheckAnswer();
                }
            };

            var questionsPath = ConfigurationManager.AppSettings["questionsPath"];
            questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(questionsPath));
            this.random = new Random();

            this.currentQuestion = this.GetRandomQuestion(this.questions);

            if(this.currentQuestion != null) {
                this.QuestionPrompt.Text = this.currentQuestion.QuestionText;
            }
        }

        private void CheckAnswer()
        {
            var enteredText = AnswerBox.Text;
            if (currentQuestion.Answer == enteredText)
            {
                var availableQuestions = this.questions.Where(question => question != this.currentQuestion);
                this.currentQuestion = this.GetRandomQuestion(availableQuestions);
                this.QuestionPrompt.Text = this.currentQuestion.QuestionText;

                MessageBox.Show("You got it right!!");
                AnswerBox.Clear();
            }
            else
            {
                MessageBox.Show("Sorry, you are incorrect :(");
                AnswerBox.Clear();
            }
        }

        private Question GetRandomQuestion(IEnumerable<Question> questions)
        {
            return questions.ElementAt(random.Next(0, questions.Count()));
        }
    }
}
