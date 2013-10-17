using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrenairTraining.Models
{
    public class MQuestion
    {
        public int IndexInTest { get; set; }

        public int GlobalId { get; set; }

        [Display(Name="Текст вопроса")]
        public string Text { get; set; }

        public List<string> Variants { get; set; }

        public string CorrectAnswers { get; set; }

        public string UserAnswers { get; set; }

        public Nullable<bool> IsAnswered { get; set; }
    }
}