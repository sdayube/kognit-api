﻿using Kognit.API.Application.Parameters;

namespace Kognit.API.Application.Wrappers
{
    public class PaginatedResponse<T> : Response<T>
    {
        public virtual int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }

        public PaginatedResponse(T data, int pageNumber, int pageSize, RecordsCount recordsCount)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.RecordsFiltered = recordsCount.RecordsFiltered;
            this.RecordsTotal = recordsCount.RecordsTotal;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}