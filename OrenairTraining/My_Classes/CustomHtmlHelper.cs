using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrenairTraining.Models;
using System.Web.Mvc.Ajax;

namespace OrenairTraining.My_Classes
{
    public static class CustomHtmlHelper
    {
        /// <summary>
        /// Рендеринг дерева данных
        /// </summary>
        /// <param name="html"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderTreeView(this HtmlHelper html, AjaxHelper ajax, List<Node> list, string controller, string mode)
        {
            string html_code = "";

            TagBuilder a_edit = new TagBuilder("a"), a_create = new TagBuilder("a"), a_delete = new TagBuilder("a");

            foreach (var item in list)
            {               
                if (!item.Deleted)
                {
                    TagBuilder li = new TagBuilder("li");

                    if (mode == "Edit")
                    {
                        a_edit = new TagBuilder("a");
                        a_edit.Attributes.Add("href", "/Container/Edit/" + item.Id);
                        a_edit.SetInnerText("\t Р ");
                        a_create = new TagBuilder("a");
                        a_create.Attributes.Add("href", "/Container/Create/" + item.Id);
                        a_create.SetInnerText(" С ");
                        a_delete = new TagBuilder("a");
                        a_delete.Attributes.Add("href", "/Container/Delete/" + item.Id);
                        a_delete.SetInnerText(" У ");                        
                    }

                    switch (item.TypeId)
                    {
                        case 1:
                            li.AddCssClass("closed");
                            TagBuilder span = new TagBuilder("span");
                            span.AddCssClass("folder");
                            span.SetInnerText(item.Name);
                            if (mode == "Edit")
                            {
                                span.InnerHtml += a_edit.ToString();
                                span.InnerHtml += a_create.ToString();
                                span.InnerHtml += a_delete.ToString();
                            }
                            li.InnerHtml += span;
                            break;
                        case 2:
                            li.AddCssClass("closed");
                            TagBuilder spaan = new TagBuilder("span");
                            spaan.AddCssClass("file");
                            spaan.SetInnerText(item.Name);
                            if (mode == "Edit")
                            {
                                spaan.InnerHtml += a_edit.ToString();
                                spaan.InnerHtml += a_create.ToString();
                                spaan.InnerHtml += a_delete.ToString();
                            }
                            li.InnerHtml += spaan;                            
                            TagBuilder ul = new TagBuilder("ul");
                            ul.InnerHtml += "<li>" + AjaxExtensions.ActionLink(ajax, "Материалы", "RenderMaterialsInRightSide", controller, new { id = item.Id }, new AjaxOptions { UpdateTargetId = "data" }, htmlAttributes: new { @class = "file" }) + "</li>";
                            ul.InnerHtml += "<li>" + AjaxExtensions.ActionLink(ajax, "Вопросы", "RenderQuestionsInRightSide", controller, new { id = item.Id }, new AjaxOptions { UpdateTargetId = "data" }, htmlAttributes: new { @class = "file" }) + "</li>";
                            li.InnerHtml += ul.ToString();
                            break;
                        default:
                            break;
                    }
                    
                    if (item.Childrens.Count > 0)
                    {
                        TagBuilder ul = new TagBuilder("ul");
                        ul.InnerHtml += RenderTreeView(html, ajax, item.Childrens, controller, mode);
                        li.InnerHtml += ul.ToString();
                    }

                    html_code += li.ToString();
                }
            }            
            return new MvcHtmlString(html_code);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="answerscount"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderAnswer(this HtmlHelper html, int answerscount)
        {
            string html_code = "";
            for (int i = 0; i < answerscount; i++)
            {
                
            }
            return new MvcHtmlString(html_code);
        }
    }
}