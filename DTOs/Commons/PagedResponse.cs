using System;
using System.Collections.Generic;

namespace mentor.DTOs.Commons
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public List<LinkDTO> Links { get; set; } = new();

        public PagedResponse(IEnumerable<T> data, int totalItems, int pageNumber, int pageSize)
        {
            Data = data;
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
