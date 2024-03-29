﻿using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;

namespace DddEf.Domain.Aggregates.SalesOrder.Entities;

public sealed class SalesOrderItemSecond
{
    private Guid DetId { get; set; }
    private SalesOrderId Id { get; }
    public int RowNumber { get; private set; }
    public ItemId ItemId { get; private set; }
    public double? Qty { get; private set; }
    public double? Price { get; private set; }
    public double? Total { get; private set; }
    public string LineStatus { get; private set; }
    public IReadOnlyList<SalesOrderItemSecondBin> Bins => (_bins.OrderBy(p => p.RowNumber)).ToList().AsReadOnly();

    private readonly List<SalesOrderItemSecondBin> _bins = new();


#pragma warning disable CS8618
    private SalesOrderItemSecond()
    {

    }
#pragma warning disable CS8618 

    public SalesOrderItemSecond(
                        int rowNumber,
                        ItemId itemId,
                       double qty,
                       double price,
                        List<SalesOrderItemSecondBin> bins
       )
    {
        DetId = Guid.NewGuid();
        RowNumber = rowNumber;
        ItemId = itemId;
        Qty = qty;
        Price = price;
        Total = qty * price;
        LineStatus = "Open";
        _bins = bins;
    }
    public static SalesOrderItemSecond Create(
                        int rowNumber,
                        ItemId itemId,
                       double qty,
                       double price,
                       List<SalesOrderItemSecondBin> bins
                       )
    {
        return new(rowNumber, itemId, qty, price, bins);
    }

    public void Close()
    {
        LineStatus = "Closed";
    }
}

