using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// Creates 'li' HTML tags with contextual styling based on the actual request.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">Text to be displayed.</param>
        /// <param name="actionName">Action to point to.</param>
        /// <param name="controllerName">Controller to point to.</param>
        /// <returns>Returns a HTML 'li' string with the given context.</returns>
        //public static MvcHtmlString MenuLink(this System.Web.Mvc.HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        //{
        //    // Get the Action and the Controller for the actual request.
        //    var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
        //    var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

        //    // Creates the tag.
        //    var li = new TagBuilder("li");

        //    // Creates a <a> inside the <li> tag, pointing to the given action and controller.
        //    li.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString();

        //    // Checks if the user's actual page is the point page at the <li>. If it is, put an 'active' css class to it for contextual displaying.
        //    // Turns all strings to lower casing to evade casing issues not setting the 'active' css correctly.
        //    if (controllerName.ToLower() == currentController.ToLower() && actionName.ToLower() == currentAction.ToLower())
        //        li.AddCssClass("active");

        //    return new MvcHtmlString(li.ToString());
        //}

        //Overload para a adicionar um ícone à esquerda.
        /// <summary>
        /// Creates 'li' HTML tags with contextual styling based on the actual request, with an glyphicon to the left.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">Text to be displayed.</param>
        /// <param name="actionName">Action to point to.</param>
        /// <param name="controllerName">Controller to point to.</param>
        /// <param name="iconClass">The glyphicon icon class. Can be without the 'glyphicon' and 'glyphicon-' values. Refeer to the actual version or Bootstrap in this project for the complete list of available Glyphicon at http://getbootstrap.com/components/#glyphicons</param>
        /// <returns>Returns a HTML 'li' string with the given context.</returns>
        //public static MvcHtmlString MenuLink(this System.Web.Mvc.HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, string iconClass)
        //{
        //    // Get the Action and the Controller for the actual request.
        //    var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
        //    var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

        //    // Creates the icon 'span' tag.
        //    var icon = new TagBuilder("span");

        //    // Make sure it adds the icon if the user inserts all the glyphicon classes or only the name of the glyphicon.
        //    icon.AddCssClass("fa fa-" + iconClass.Replace("fa fa-", ""));

        //    // Creates the 'a' tag.
        //    var link = new TagBuilder("a")
        //    {
        //        InnerHtml = icon + "&nbsp;&nbsp;" + linkText // Adds two spaces between the icon and the text.
        //    };

        //    // Adds the link according to the given action and controllers parameters.
        //    link.Attributes.Add("href", "/" + controllerName + "/" + actionName + "/" + routeValues);

        //    // Creates the 'li' tag.
        //    var li = new TagBuilder("li")
        //    {
        //        InnerHtml = link.ToString()
        //    };

        //    // Checks if the user's actual page is the point page at the <li>. If it is, put an 'active' css class to it for contextual displaying.
        //    // Turns all strings to lower casing to evade casing issues not setting the 'active' css correctly.
        //    if (controllerName.ToLower() == currentController.ToLower() && actionName.ToLower() == currentAction.ToLower())
        //        li.AddCssClass("active");

        //    return new MvcHtmlString(li.ToString());
        //}
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, string iconClass, object htmlAttributes)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var currentArea = htmlHelper.ViewContext.RouteData.DataTokens["area"];
            // Build the icon and display text elements
            StringBuilder innerHtml = new StringBuilder();

            TagBuilder icon = new TagBuilder("i");
            icon.AddCssClass(iconClass);
            innerHtml.Append(icon.ToString());

            TagBuilder span = new TagBuilder("span");
            span.InnerHtml = " "+linkText;
            innerHtml.Append(span.ToString());
            // Build the link
            TagBuilder link = new TagBuilder("a");
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.Action(actionName, controllerName, routeValues);
            link.MergeAttribute("href", url);
            link.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            link.InnerHtml = innerHtml.ToString();
            // Build the li element
            TagBuilder li = new TagBuilder("li");
            li.InnerHtml = link.ToString();
            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase) && String.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase))
            {
                li.AddCssClass("active");
            }
            // Return the html
            return new MvcHtmlString(li.ToString());
        }

    }
}