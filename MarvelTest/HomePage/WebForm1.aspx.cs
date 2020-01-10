using MarvelTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarvelTest.Source
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HomeController homeController = new HomeController();

            List<Personagem> personagens = homeController.Index(1);
            Comic comic = homeController.Index(2);

            Titulo.InnerHtml = comic.Titulo;
            imagem.Src = comic.UrlImagem;
            descricao.InnerHtml = comic.Descricao;
            rights.InnerHtml = comic.Attribution;


            foreach (var item in personagens)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl();
                div.Attributes.Add("class", "col-md-12");
                div.InnerHtml =
                "<div class=\"col-xl-4 text-center mb-5\" style=\"margin: auto\">" +
                    "<img class=\"rounded-circle\" src=\"" + item.UrlImagem + "\" alt=\"Generic placeholder image\" style=\"width: 180px; height: 180px\" runat=\"server\">" +
                    "<h4>" + item.Nome + "</h4>" +
                    "<span runat= \"server\" style=\"font-size: 125%\">" + item.Descricao + "</ span >" +
                "</div>";
                chars.Controls.Add(div);
                
            }

            
        }
    }
}