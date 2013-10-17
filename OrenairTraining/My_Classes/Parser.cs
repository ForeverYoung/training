using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrenairTraining.My_Classes
{
    public class Parser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns>List<string> состоит из последовательности id темы - вопросы</returns>
        public static List<string> ParseTestConfig(string themes, string questions)
        {
            
            string[] themeIds;
            string[] questions_count;

            themeIds = themes.Split(new char[]{ '|' });
            questions_count = questions.Split(new char[] {'|'});

            List<string> list = new List<string>();

            for (int i = 0; i < themeIds.Count() - 1; i++)
            {
                list.Add(themeIds[i]);
                list.Add(questions_count[i]);
            }

            return list;
        }
    }
}