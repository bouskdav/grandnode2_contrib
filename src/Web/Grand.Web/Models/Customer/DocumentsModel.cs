﻿using Grand.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Grand.Web.Models.Customer
{
    public class DocumentsModel : BaseModel
    {
        public DocumentsModel()
        {
            DocumentList = new List<Document>();
            PagingContext = new DocumentPagingModel();
        }

        public List<Document> DocumentList { get; set; }
        public DocumentPagingModel PagingContext { get; set; }
        public string CustomerId { get; set; }
    }

    public class Document: BaseEntityModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DownloadId { get; set; }
        public string Status { get; set; }
        public string DocumentType { get; set; }
        public string Link { get; set; }
        public double Amount { get; set; }
        public double OutstandAmount { get; set; }
        public int Quantity { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DueDate { get; set; }

    }
}