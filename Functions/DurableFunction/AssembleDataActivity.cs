using System.Collections.Generic;
using DurableFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace DurableFunction
{

    // We input a tuple, because I think you can only have 1 input to this, so we then immediatly break it out of the tuple
    // because frick tuples, all my homies hate tuples

    // The 2nd tuple value should go from string to meme model

    public class AssembleDataActivity
    {

        // TODO : Remove comments and add data from memes when we know what we're getting.
        [Function(nameof(AssembleMemeNews))]
        public static System.Collections.Generic.List<DurableFunction.Models.MemeNewsModel> AssembleMemeNews(
            [ActivityTrigger] DurableFunction.AssembleInput input,
             FunctionContext context)
        {
            // break out of tuple
            var news = input.NewsList;
            // Vet inte hur många memes det är vi kommer få? är det typ 100+ eller typ 3?
            var memes = input.MemeList;
            // int counter = 0;
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
                    //MemeURL = memes[counter]

                };

                // inte testat den här koden men det borde gardera mot overflows, bara loopar över memesen om och om igen. funkar för nu
                // counter++;
                // if (memes.Count < counter)
                //  counter = 0;
                output.Add(data);
            }

            return output;
        }
    }
}