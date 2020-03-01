using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludotheque.Models;
using Microsoft.EntityFrameworkCore;

namespace Ludotheque
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }
        /// <summary>
        /// used to enable or disable Previous
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        /// <summary>
        ///  used to enable or disable Next paging buttons
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static async Task<PaginatedList<Game>> CreateAsync(GamesIndexData gamesAllData, int pageIndex, int pageSize)
        {
            var source = gamesAllData.Games.AsQueryable().AsNoTracking();
            return await PaginatedList<Game>.CreateAsync(source, pageIndex, pageSize);
        }
    }
}
