using AutoFixture;
using BookRating.App.Models;
using BookRating.App.Models.Entities;
using BookRating.App.Services;
using Microsoft.Azure.Cosmos;
using Moq;

namespace BookRating.App.Test.Services;

public class CosmosDbServiceTest
{
    private Mock<Container> _container;
    private Mock<CosmosClient> _cosmosClient;
    private const string DATABASE_NAME = nameof(DATABASE_NAME);
    private const string CONTAINER_NAME = nameof(CONTAINER_NAME);
    private CosmosDbService _underTest;
    

    [SetUp]
    public void Setup()
    {
        _container = new Mock<Container>();
        _cosmosClient = new Mock<CosmosClient>();
        _cosmosClient
            .Setup(x => x.GetContainer(DATABASE_NAME, CONTAINER_NAME))
            .Returns(_container.Object);
        _underTest = new CosmosDbService(_cosmosClient.Object, DATABASE_NAME, CONTAINER_NAME);
    }

    [Test]
    public async Task AddItem_Should_CreateItem()
    {
        //given
        var entity = BookRatingEntity.FromNewViewModel(new CreateBookRatingViewModel());
        
        //when
        await _underTest.AddItemAsync(entity);
        
        //then
        _container.Verify(x => x.CreateItemAsync(entity, 
            It.IsAny<PartitionKey>(),
            It.IsAny<ItemRequestOptions>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteItem_Should_DeleteItem()
    {
        //given
        var id = Guid.NewGuid().ToString();

        //when

        await _underTest.DeleteItemAsync(id);

        //then
        _container.Verify(x => x.DeleteItemAsync<BookRatingEntity>(id, 
            It.IsAny<PartitionKey>(),
            It.IsAny<ItemRequestOptions>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public async Task UpdateItem_Should_UpsertItem()
    {
        //given
        var entity = BookRatingEntity.FromNewViewModel(new CreateBookRatingViewModel());

        //when

        await _underTest.UpdateItemAsync(entity);

        //then
        _container.Verify(x => x.UpsertItemAsync(entity, 
            It.IsAny<PartitionKey>(),
            It.IsAny<ItemRequestOptions>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}