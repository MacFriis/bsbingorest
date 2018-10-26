using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BSBingoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BSBingoApi.Services
{
    public class DefaultBSWordService : IBSWordService
    {
        private readonly BSBingoApiDbContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultBSWordService(BSBingoApiDbContext context,
                                    IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        /// <summary>
        /// Gets the available categories async.
        /// </summary>
        /// <returns>The available categories async.</returns>
        /// <param name="pagingOptions">Paging options.</param>
        /// <param name="searchOptions">Search options.</param>
        public async Task<PagedResults<string>> GetAvailableCategoriesAsync(
           PagingOptions pagingOptions,
            SearchOptions<BSWord, BSWordEntity> searchOptions)
        {
            IQueryable<BSWordEntity> query = _context.BSWords.Where(x => x.Category != null);

            query = searchOptions.Apply(query);

            var items = await query
                .Select(x => x.Category)
                .Distinct()
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ToArrayAsync();

            var size = items.Count();

            return new PagedResults<string>
            {
                Items = items,
                TotalSize = size
            };


        }

        /// <summary>
        /// Gets the available languages async.
        /// </summary>
        /// <returns>The available languages async.</returns>
        public async Task<PagedResults<string>> GetAvailableLanguagesAsync(
            PagingOptions pagingOptions
        )
        {
            var languages = await _context.BSWords
                                          .Select(x => x.Language)
                                          .Distinct()
                                          .Skip(pagingOptions.Offset.Value)
                                          .Take(pagingOptions.Limit.Value)
                                          .ToArrayAsync();

            var size = languages.Count();
            return new PagedResults<string>
            {
                Items = languages,
                TotalSize = size
            };
        }

        /// <summary>
        /// Gets the BSW ord async.
        /// </summary>
        /// <returns>The BSW ord async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<BSWord> GetBSWordAsync(Guid id)
        {
            var entity = await _context.BSWords
                                       .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<BSWord>(entity);
        }

        /// <summary>
        /// Gets the words async.
        /// </summary>
        /// <returns>The words async.</returns>
        /// <param name="pagingOptions">Paging options.</param>
        /// <param name="sortOptions">Sort options.</param>
        /// <param name="searchOptions">Search options.</param>
        public async Task<PagedResults<BSWord>> GetWordsAsync(
            PagingOptions pagingOptions,
            SortOptions<BSWord, BSWordEntity> sortOptions,
            SearchOptions<BSWord, BSWordEntity> searchOptions)
        {
            IQueryable<BSWordEntity> query = _context.BSWords;
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<BSWord>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<BSWord>
            {
                Items = items,
                TotalSize = size
            };
        }
    }
}
