namespace WIS.Domain.Exceptions;

public class InventoryNotCreatedException(string code)
    : ApplicationException($"Entity not created yet:  SkuNumber {code}");