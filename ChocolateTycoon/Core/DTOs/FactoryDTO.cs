namespace ChocolateTycoon.Core.DTOs
{
    public class FactoryDto
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public byte Level { get; private set; }

        public ProductionUnitDto ProductionUnitDTO { get; set; }
        public StorageUnitDto StorageUnitDTO { get; set; }

        public SupplierDto Supplier { get; set; }
    }
}