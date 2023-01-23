using BookRating.App.Controllers;
using BookRating.App.Models;
using BookRating.App.Models.Entities;
using BookRating.App.Services;
using Moq;

namespace BookRating.App.Test.Controllers;

public class BookRatingControllerTest
{
    private Mock<ICosmosDbService> _cosmosDbService;
    private BookRatingController _underTest; 

    [SetUp]
    public void Setup()
    {
        _cosmosDbService = new Mock<ICosmosDbService>();
        _underTest = new BookRatingController(_cosmosDbService.Object);
    }
    
    [Test]
    public async Task Index_Should_DelegateToCosmosDbService()
    {
        //given
        
        //when
        await _underTest.Index();
        
        //then
        _cosmosDbService.Verify(x => x.GetItemsAsync(It.IsAny<string>()), 
            Times.Once);
    }
    
    [Test]
    public async Task CreateAsync_Should_DelegateToCosmosDbService()
    {
        //given
        var viewModel = new CreateBookRatingViewModel
        {
            Name = "name",
            Description = "desc",
            Rating = 2
        };
        
        //when
        await _underTest.CreateAsync(viewModel);
        
        //then
        _cosmosDbService.Verify(x => x.AddItemAsync(It.IsAny<BookRatingEntity>()), 
            Times.Once);
    }
    
    [Test]
    public async Task EditAsync_Should_DelegateToCosmosDbService()
    {
        //given
        var viewModel = new BookRatingViewModel
        {
            Name = "name",
            Description = "desc",
            Rating = 2
        };
        
        //when
        await _underTest.EditAsync(viewModel);
        
        //then
        _cosmosDbService.Verify(x => x.UpdateItemAsync(It.IsAny<BookRatingEntity>()), 
            Times.Once);
    }
}