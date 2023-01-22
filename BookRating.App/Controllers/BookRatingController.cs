using BookRating.App.Models;
using BookRating.App.Models.Entities;
using BookRating.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRating.App.Controllers;

public class BookRatingController : Controller
{
    private readonly ICosmosDbService _cosmosDbService;

    public BookRatingController(ICosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    [ActionName("Index")]
    public async Task<IActionResult> Index()
    {
        return View((await _cosmosDbService.GetItemsAsync("SELECT * FROM c"))
            .Select(entity => entity.ToViewModel()).ToList());
    }

    [ActionName("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync([Bind("Name,Description,Rating")] CreateBookRatingViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            await _cosmosDbService.AddItemAsync(BookRatingEntity.FromNewViewModel(viewModel));
            return RedirectToAction("Index");
        }

        return View(viewModel);
    }

    [HttpPost]
    [ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync([Bind("Id,Name,Description,Rating")] BookRatingViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            await _cosmosDbService.UpdateItemAsync(BookRatingEntity.FromViewModel(viewModel));
            return RedirectToAction("Index");
        }

        return View(viewModel);
    }

    [ActionName("Edit")]
    public async Task<ActionResult> EditAsync(string id) => id != null
        ? await _cosmosDbService.GetItemAsync(id) is { } entity
            ? View(entity.ToViewModel())
            : NotFound()
        : BadRequest();

    [ActionName("Delete")]
    public async Task<ActionResult> DeleteAsync(string id) => id != null
        ? await _cosmosDbService.GetItemAsync(id) is { } entity
            ? View(entity.ToViewModel())
            : NotFound()
        : BadRequest();

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
    {
        await _cosmosDbService.DeleteItemAsync(id);
        return RedirectToAction("Index");
    }

    [ActionName("Details")]
    public async Task<ActionResult> DetailsAsync(string id)
    {
        return View((await _cosmosDbService.GetItemAsync(id)).ToViewModel());
    }
}