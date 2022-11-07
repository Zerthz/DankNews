using System.Collections.Generic;
using DurableFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace DurableFunction
{
    // Had problems inputting a tuple to the activity with untyped, so sending in one of these. The string should be changed to memes
    public record AssembleInput(List<News> NewsList, List<Meme> MemeList);

    public class AssembleDataActivity
    {

        // TODO : Remove comments and add data from memes when we know what we're getting.
        [Function(nameof(AssembleMemeNews))]
        public static System.Collections.Generic.List<DurableFunction.Models.MemeNewsModel> AssembleMemeNews(
            [ActivityTrigger] DurableFunction.AssembleInput input,
             FunctionContext context)
        {

            var news = input.NewsList;
            // Vet inte hur många memes det är vi kommer få? är det typ 100+ eller typ 3?
            var memes = input.MemeList;
            int counter = 0;
            List<MemeNewsModel> output = new List<MemeNewsModel>();
            foreach (var item in news)
            {
                // Create a new model here with the wanted data from the inputs
                var data = new MemeNewsModel()
                {
                    NewsTitle = item.Title,
                    NewsAbstract = item.Abstract,
                    NewsSection = item.Section,
                    NewsSubsection = item.Subsection,
                    NewsDatePublished = item.PublishedDate,
                    NewsByLine = item.Byline,
                    NewsURL = item.Url,

                    // Kan typ ha en counter och ta en meme från det? måste bara gardera mot range overflow
                    MemeURL = memes[counter].Url,
                    MemeAlt = memes[counter].Name
                };

                // inte testat den här koden men det borde gardera mot overflows, bara loopar över memesen om och om igen. funkar för nu
                counter++;
                if (memes.Count < counter)
                    counter = 0;
                output.Add(data);
            }

            return output;
        }
    }
}