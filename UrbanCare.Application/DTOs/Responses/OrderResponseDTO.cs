namespace UrbanCare.Application.DTOs.Responses
{
    public record OrderResponseDTO(
        int Id,
        ResidentResponseDTO Resident,
        string Description,
        OrderCategoryResponseDTO OrderCategory,
        BuildingResponseDTO Building,
        ApartmentResponseDTO? Apartment,
        PriorityResponseDTO Priority,
        string ContactPhone,
        string ContactEmail,
        OrderStatusResponseDTO OrderStatus,
        EmployeeDataResponseDTO? Dispatcher,
        DateTime CreatedAt,
        DateTime? ChangedAt,
        DateTime? AcceptedAt,
        DateTime? CompletedAt,
        List<OrderExecutorResponseDTO>? OrderExecutors,
        List<OrderMaterialResponseDTO>? OrderMaterials);
}
