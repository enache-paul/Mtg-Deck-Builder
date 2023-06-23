using Xunit;
using mtg.Models;
using mtg.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using mtg_lib.Data;
using X.PagedList;

namespace mtg.Test
{
    public class SavedDecksControllerTest
    {
        [Fact]
        public void LoadDecksReturnsResult()
        {
            var logger = new LoggerFactory().CreateLogger<SavedDecksController>();
            var controller = new SavedDecksController(logger)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = controller.LoadDecks() as ViewResult;

            Assert.NotNull(result);
            Assert.Null(result.ViewName);
        }
        

        [Fact]
        public void FilterCards()
        {
            var logger = new LoggerFactory().CreateLogger<SavedDecksController>();
            var controller = new SavedDecksController(logger)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            var name = "Test";
            var page = 1;
            var pageSize = 10;
            var cardsCounted = new List<CardsCounted>();
            var deckId = 2;
            
            var result = controller.FilterCardsBy(name, page, pageSize, cardsCounted, deckId);

            Assert.NotNull(result);
            Assert.True(result is IPagedList<CardsCounted>);
        }
    }
}