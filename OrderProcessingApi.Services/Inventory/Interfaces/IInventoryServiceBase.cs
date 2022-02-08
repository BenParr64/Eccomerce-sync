﻿using OrderProcessingApi.Domain;
using OrderProcessingApi.Domain.Integrations;

namespace OrderProcessingApi.Services.Inventory.Interfaces;

public interface IInventoryServiceBase
{
     void AddInventoryItems(List<Product> inventoryItems, Integration profile);
}