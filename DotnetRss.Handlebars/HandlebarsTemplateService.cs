// <copyright file="HandlebarsTemplateService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;
using HandlebarsDotNet;

namespace DotnetRss.Core
{
    /// <summary>
    /// Handlebars Template Service.
    /// </summary>
    public class HandlebarsTemplateService : ITemplateService
    {
        private HandlebarsTemplate<object, object> feedItemTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlebarsTemplateService"/> class.
        /// </summary>
        public HandlebarsTemplateService()
        {
            this.feedItemTemplate = Handlebars.Compile(HandlebarsTemplateService.GetResourceFileContentAsString("Templates.feeditem.html.hbs"));
        }

        /// <inheritdoc/>
        public async Task<string> RenderFeedItemAsync(FeedItem item)
        {
            if (item.Link is null)
            {
                throw new ArgumentNullException(nameof(item.Link));
            }

            SmartReader.Article article = await SmartReader.Reader.ParseArticleAsync(item.Link);
            item.Html = article.Content;
            return this.feedItemTemplate.Invoke(item);
        }

        private static string GetResourceFileContentAsString(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            if (assembly is null)
            {
                return string.Empty;
            }

            var resourceName = "DotnetRss.Handlebars." + fileName;

            string? resource = null;
            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream is null)
                {
                    return string.Empty;
                }

                using StreamReader reader = new StreamReader(stream);
                resource = reader.ReadToEnd();
            }

            return resource ?? string.Empty;
        }
    }
}
