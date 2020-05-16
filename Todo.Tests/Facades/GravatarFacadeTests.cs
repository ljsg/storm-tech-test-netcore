using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Facades;
using Todo.Models;
using Todo.Tests.TestDoubles;
using Xunit;

namespace Todo.Tests.Facades
{
    public class GravatarFacadeTests
    {
        private readonly Dictionary<string, HttpResponseMessage> cannedAnswers;
        private readonly GravatarFacade gravatarFacade;

        public GravatarFacadeTests()
        {
            cannedAnswers = new Dictionary<string, HttpResponseMessage>()
            {
                {
                    "/forbiddenEmailHash.json", new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Forbidden
                    }
                },
                {
                    "/liamEmailHash.json", new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("{\"entry\":[{\"id\":\"123995739\",\"hash\":\"c7e9c2b2fe54e4215a93ab7745fba5f4\",\"requestHash\":\"c7e9c2b2fe54e4215a93ab7745fba5f4\",\"profileUrl\":\"http://gravatar.com/liamgove\",\"preferredUsername\":\"liamgove\",\"thumbnailUrl\":\"https://secure.gravatar.com/avatar/c7e9c2b2fe54e4215a93ab7745fba5f4\",\"photos\":[{\"value\":\"https://secure.gravatar.com/avatar/c7e9c2b2fe54e4215a93ab7745fba5f4\",\"type\":\"thumbnail\"}],\"name\":{\"givenName\":\"Liam\",\"familyName\":\"Gove\"},\"displayName\":\"Liam\",\"urls\":[]}]}")
                    }
                }
            };
            var messageHandler = new StubHttpMessageHandler(cannedAnswers);
            var httpClient = new HttpClient(messageHandler);
            gravatarFacade = new GravatarFacade(httpClient);
        }

        [Fact]
        public async Task Return_null_when_forbidden_response_is_received()
        {
            UserProfile result = await gravatarFacade.GetProfile("forbiddenEmailHash");
            Assert.Null(result);
        }

        [Fact]
        public async Task Return_user_profile_when_succesfully_retrieved()
        {
            UserProfile result = await gravatarFacade.GetProfile("liamEmailHash");
            Assert.Equal("Liam", result.DisplayName);
            Assert.Equal("https://secure.gravatar.com/avatar/c7e9c2b2fe54e4215a93ab7745fba5f4", result.PictureUrl);
        }
    }
}


