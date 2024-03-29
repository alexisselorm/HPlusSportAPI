﻿namespace HPlusSport.API.Models;

public class ProductQueryParameters : QueryParameters
{
    public string? Name { get; set; } = string.Empty;
    public string? Sku { get; set; } = string.Empty;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string SearchTerm { get; set; } = string.Empty;

}