namespace Web.Dtos.Sales.Service
{
    public class OutputServicesResultDto
    {
        public List<OutputServiceDto> Array { get; set; }
        public string Message { get; set; }
        public OutputPermissionDto Permission { get; set; }
    }
}
