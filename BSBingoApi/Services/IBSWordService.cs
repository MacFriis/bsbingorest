using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BSBingoApi.Model;

namespace BSBingoApi.Services
{
    public interface IBSWordService
    {
        Task<PagedResults<BSWord>> GetWordsAsync(
            PagingOptions pagingOptions,
            SortOptions<BSWord, BSWordEntity> sortOptions,
            SearchOptions<BSWord, BSWordEntity> searchOptions);

        Task<BSWord> GetBSWordAsync(Guid id);


        Task<PagedResults<string>> GetAvailableLanguagesAsync(
            PagingOptions pagingOptions
        );

        Task<PagedResults<string>> GetAvailableCategoriesAsync(
            PagingOptions pagingOptions,
            SearchOptions<BSWord, BSWordEntity> searchOptions
        );
    }
}
