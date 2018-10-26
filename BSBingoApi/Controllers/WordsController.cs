using System;
using System.Linq;
using System.Threading.Tasks;
using BSBingoApi.Model;
using BSBingoApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BSBingoApi.Controllers
{
    [Route("/[controller]")]
    [Authorize]
    [ApiController]
    public class WordsController : Controller
    {
        private readonly IBSWordService _bswordService;
        private readonly PagingOptions _defaultPagingOptions;

        public WordsController(IBSWordService bSWordService,
                               IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            _bswordService = bSWordService;
            _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }

        [HttpGet(Name = nameof(GetAllWords))]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<BSWord>>> GetAllWords(
            [FromQuery] PagingOptions paginOptions,
            [FromQuery] SortOptions<BSWord, BSWordEntity> sortOptions,
            [FromQuery] SearchOptions<BSWord, BSWordEntity> searchOptions)
        {
            paginOptions.Offset = paginOptions.Offset ?? _defaultPagingOptions.Offset;
            paginOptions.Limit = paginOptions.Limit ?? _defaultPagingOptions.Limit;

            var words = await _bswordService.GetWordsAsync(
                paginOptions,
                sortOptions,
                searchOptions);

            var collection = PagedCollection<BSWord>.Create(
                Link.ToCollection(nameof(GetAllWords)),
                words.Items.ToArray(),
                words.TotalSize,
                paginOptions);


            return collection;
        }

        [HttpGet("{wordId}", Name = nameof(GetWordById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<BSWord>> GetWordById(Guid wordId)
        {
            var bsword = await _bswordService.GetBSWordAsync(wordId);
            if (bsword == null)
            {
                return NotFound();
            }


            return bsword;
        }

        [HttpGet("languages", Name = nameof(GetAvailableLanguages))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<String>>> GetAvailableLanguages(
            [FromQuery] PagingOptions pagingOptions
        )
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var languages = await _bswordService.GetAvailableLanguagesAsync(pagingOptions);
            if (languages == null)
            {
                return NotFound();
            }

            var result = PagedCollection<string>.Create(
                Link.ToCollection(nameof(GetAvailableLanguages)),
                                  languages.Items.ToArray(),
                                  languages.TotalSize,
                                  pagingOptions

            );

            return result;
        }



        /// <summary>
        /// Gets the available categories.
        /// </summary>
        /// <returns>The available categories.</returns>
        /// <param name="pagingOptions">Paging options.</param>
        /// <param name="searchOptions">Search options.</param>
        [HttpGet("categories", Name = nameof(GetAvailableCategories))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<string>>> GetAvailableCategories(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SearchOptions<BSWord, BSWordEntity> searchOptions
        )
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var categories = await _bswordService.GetAvailableCategoriesAsync(
                pagingOptions,
                searchOptions
            );

            return PagedCollection<string>.Create(
                Link.ToCollection(nameof(GetAvailableCategories)),
                categories.Items.ToArray(),
                categories.TotalSize,
                pagingOptions
            );
        }
    }
}
