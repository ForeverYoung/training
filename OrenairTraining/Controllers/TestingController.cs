using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    public class TestingController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /Testing/

        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            var tests = db.testconfig.Where(c => c.deleted == false).ToList();
            return View(tests);
        }

        //
        // GET: /StartTest/

        [Authorize]
        public ActionResult StartTest(int config_id)
        {
            Session["configId"] = config_id;
            Session["questions"] = FillTestFromConfig(config_id);
            Session["mQuestions"] = new List<MQuestion>();
            Session["sessionData"] = new session() {datetime = DateTime.Now, 
                                                    ipaddress = HttpContext.Request.UserHostAddress, 
                                                    deleted = false, 
                                                    testconfig_id = config_id,
                                                    user_id = db.user.First(u => u.user_name == User.Identity.Name).user_id
            };
                  
            var model = db.testconfig.Find(config_id);
            return View(model);
        }

        /// <summary>
        /// Рендеринг страницы тестирования
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderQuestionPage()
        {
            int i=0;
            //"June 7, 2087 15:03:25"
            var months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
            var date = DateTime.Now.Add(TimeSpan.Parse(db.testconfig.Find(Session["configId"]).time.ToString()));
            var countdownTime = months[date.Month - 1] + " " + date.Day + ", " + date.Year + " " + date.Hour + ":" + date.Minute + ":" + date.Second;
            ViewBag.CountdownTime = countdownTime;
            //time formatting ending
            foreach (var qnum in Session["questions"] as List<int>)
            {
                var q = db.question.Find(qnum);
                (Session["mQuestions"] as List<MQuestion>).Add(new MQuestion() { 
                            Text = q.text,
                            Variants = q.answer.Split(new char[] { '|' }).ToList(),
                            CorrectAnswers = "",
                            IndexInTest = i,
                            GlobalId = q.question_id,
                            UserAnswers = "",
                            IsAnswered = false
                        });
                foreach (var variant in (Session["mQuestions"] as List<MQuestion>)[i].Variants)
                    (Session["mQuestions"] as List<MQuestion>)[i].CorrectAnswers += (variant.StartsWith("@") ? variant + "|" : "");
                i++;
            }
            if (i > 0)
            {
                ViewBag.Variants = (Session["mQuestions"] as List<MQuestion>)[0].Variants;
                return View((Session["mQuestions"] as List<MQuestion>)[0]);
            }
            return PartialView();
        }

        /// <summary>
        /// Рендеринг частичного представления, содержащего текст вопроса и варианты ответов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AnswerOnQuestion(MQuestion mq, string[] answers)
        {
            int newIndex = mq.IndexInTest + 1;
            foreach (var a in answers)
            {
                (Session["mQuestions"] as List<MQuestion>)[mq.IndexInTest].UserAnswers += 
                    (Session["mQuestions"] as List<MQuestion>)[mq.IndexInTest].Variants[Convert.ToInt32(a)] + "|";
            }
            if (newIndex < (Session["mQuestions"] as List<MQuestion>).Count)
            {
                ViewBag.Variants = (Session["mQuestions"] as List<MQuestion>)[newIndex].Variants;
                return PartialView("QuestionContent", (Session["mQuestions"] as List<MQuestion>)[newIndex]);
            }
            else
                return PartialView("TheEnd");
        }

        public ActionResult GoTo(int questionIndex)
        {
            ViewBag.Variants = (Session["mQuestions"] as List<MQuestion>)[questionIndex].Variants;
            return PartialView("QuestionContent", (Session["mQuestions"] as List<MQuestion>)[questionIndex]);
        }

        /// <summary>
        /// Генерирует набор вопросов в соответствии с конфигурацией для каждой сессии
        /// </summary>
        /// <param name="config_id">id конфигурации</param>
        /// <returns>Готовый набор вопросов</returns>
        List<int> FillTestFromConfig(int config_id)
        {
            List<int> allQuestionsInTest = new List<int>();
            testconfig chosenTestConfig = db.testconfig.Find(config_id);
            var listOfPairThemeAndQuestions = My_Classes.Parser.ParseTestConfig(chosenTestConfig.themes, chosenTestConfig.questions);
            Random r = new Random();
            for (int i = 0; i < listOfPairThemeAndQuestions.Count; i++)
            {
                int theme = Convert.ToInt32(listOfPairThemeAndQuestions[i]);
                i++;
                string[] questionSet = listOfPairThemeAndQuestions[i].Split(new char[] { '-' });

                var allQuestionsOfTheme = db.question.Where(q => q.container_id == theme && q.deleted == false).ToList();

                // Перемешиваем вопросы данного типа 'j' и темы 'theme' в случайном порядке
                // и выбираем кол-во 'questionSet[j - 1]' для данной сессии
                for (int j = 1; j < questionSet.Length + 1; j++)
                {
                    var allQuestionsOfTypeInTheme = allQuestionsOfTheme.Where(q => q.type_id == j).OrderBy(x => r.Next()).Take(Convert.ToInt32(questionSet[j - 1])).ToList();
                    foreach (var q in allQuestionsOfTypeInTheme)
                    {
                        allQuestionsInTest.Add(q.question_id);
                    }
                }
            }
            return allQuestionsInTest;
        }
    }
}
