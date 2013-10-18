using System;
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
            CheckAnswerButton.Click += CheckAnswerButton_Click;

            var questionsPath = ConfigurationManager.AppSettings["questionsPath"];
            questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(questionsPath));
            this.random = new Random();

            this.currentQuestion = questions.ElementAt(random.Next(0, this.questions.Count));

            if(this.currentQuestion != null) {
                this.QuestionPrompt.Text = this.currentQuestion.QuestionText;
            }
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button was clicked");
        }

        
    }
}
